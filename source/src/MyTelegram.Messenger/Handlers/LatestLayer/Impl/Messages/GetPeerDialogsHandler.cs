// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Get dialog info of specified peers
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 406 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.getPeerDialogs" />
///</summary>
internal sealed class GetPeerDialogsHandler(
    IDialogAppService dialogAppService,
    IPeerHelper peerHelper,
    IPtsHelper ptsHelper,
    ILayeredService<IDialogConverter> layeredService,
    IAccessHashHelper accessHashHelper,
    ILogger<GetPeerDialogsHandler> logger,
    IQueryProcessor queryProcessor,
    IPhotoAppService photoAppService,
    ILayeredService<IUserConverter> layeredUserService,
    IPrivacyAppService privacyAppService)
    : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetPeerDialogs, MyTelegram.Schema.Messages.IPeerDialogs>,
        Messages.IGetPeerDialogsHandler
{
    private readonly IPeerHelper _peerHelper = peerHelper;
    private readonly IAccessHashHelper _accessHashHelper = accessHashHelper;
    private readonly ILogger<GetPeerDialogsHandler> _logger = logger;
    private readonly IQueryProcessor _queryProcessor = queryProcessor;

    protected override async Task<IPeerDialogs> HandleCoreAsync(IRequestInput input,
        RequestGetPeerDialogs obj)
    {
        var userId = input.UserId; // await GetUidAsync(input);
        var peerList = new List<Peer>();

        foreach (var inputDialogPeer in obj.Peers)
        {
            switch (inputDialogPeer)
            {
                case TInputDialogPeer dialogPeer when dialogPeer.Peer is TInputPeerSelf || dialogPeer.Peer is TInputPeerEmpty:
                    continue;
                case TInputDialogPeer dialogPeer:
                {
                    var peer = peerHelper.GetPeer(dialogPeer.Peer, userId);
                    if (peerHelper.IsEncryptedDialogPeer(peer.PeerId))
                    {
                        continue;
                    }

                    await accessHashHelper.CheckAccessHashAsync(dialogPeer.Peer);

                    //if (peer.PeerId != userId)
                    //{
                    //    peerList.Add(peer);
                    //}

                    peerList.Add(peer);
                    break;
                }
                case TInputDialogPeerFolder inputDialogPeerFolder:
                {
                    var peers = await queryProcessor.ProcessAsync(
                        new GetDialogsByFolderIdQuery(userId, inputDialogPeerFolder.FolderId));
                    peerList.AddRange(peers);
                }
                    break;
            }
        }

        var limit = peerList.Count == 0 ? 10 : peerList.Count;
        var output = await dialogAppService
            .GetDialogsAsync(new GetDialogInput
            {
                OwnerId = userId,
                Limit = limit,
                PeerIdList = peerList.Select(p => p.PeerId).ToList()
            });
        var pts = await _queryProcessor.ProcessAsync(new GetPtsByPeerIdQuery(input.UserId));
        var cachedPts = ptsHelper.GetCachedPts(input.UserId);

        output.PtsReadModel = pts;
        output.CachedPts = cachedPts;
        var r = layeredService.GetConverter(input.Layer).ToPeerDialogs(output);

        foreach (var dialog in r.Dialogs)
        {
            switch (dialog)
            {
                case Schema.TDialog d:
                    var m = output.MessageList.FirstOrDefault(p => p.MessageId == d.TopMessage);
                    break;
                case TDialogFolder dialogFolder:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(dialog));
            }
        }

        if (r.Dialogs.Count == 0)
        {
            var userIds = peerList.Where(p => p.PeerType == PeerType.User).Select(p => p.PeerId).Distinct().ToList();
            var userReadModels = await _queryProcessor.ProcessAsync(new GetUsersByUidListQuery(userIds));
            var photoReadModels = await photoAppService.GetPhotosAsync(userReadModels);
            var contactReadModels = await _queryProcessor.ProcessAsync(new GetContactListQuery(input.UserId, userIds));
            var privacyReadModels = await privacyAppService.GetPrivacyListAsync(userIds);
            var users = layeredUserService.GetConverter(input.Layer)
                .ToUserList(input.UserId, userReadModels, photoReadModels, contactReadModels, privacyReadModels);
            r.Dialogs = new TVector<IDialog>(peerList.Select(p => new TDialog
            {
                Peer = p.ToPeer(),
                NotifySettings = new TPeerNotifySettings(),
            }));
            if (r.Users == null)
            {
                r.Users = new();
            }
            foreach (var user in users)
            {
                r.Users.Add(user);
            }
        }
        return r;
    }
}
