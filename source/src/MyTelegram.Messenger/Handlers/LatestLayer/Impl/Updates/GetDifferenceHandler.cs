// ReSharper disable All

using MyTelegram.Schema.Updates;

namespace MyTelegram.Handlers.Updates;

///<summary>
/// Get new <a href="https://corefork.telegram.org/api/updates">updates</a>.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CDN_METHOD_INVALID You can't call this method in a CDN DC.
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 403 CHAT_WRITE_FORBIDDEN You can't write in this chat.
/// 400 DATE_EMPTY Date empty.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 400 PERSISTENT_TIMESTAMP_EMPTY Persistent timestamp empty.
/// 400 PERSISTENT_TIMESTAMP_INVALID Persistent timestamp invalid.
/// 500 RANDOM_ID_DUPLICATE You provided a random ID that was already used.
/// 400 USERNAME_INVALID The provided username is not valid.
/// 400 USER_NOT_PARTICIPANT You're not a member of this supergroup/channel.
/// See <a href="https://corefork.telegram.org/method/updates.getDifference" />
///</summary>
internal sealed class GetDifferenceHandler : RpcResultObjectHandler<MyTelegram.Schema.Updates.RequestGetDifference, MyTelegram.Schema.Updates.IDifference>,
    Updates.IGetDifferenceHandler
{
    private readonly IAckCacheService _ackCacheService;
    private readonly ILogger<GetDifferenceHandler> _logger;
    private readonly IMessageAppService _messageAppService;
    private readonly IPtsHelper _ptsHelper;
    private readonly IQueryProcessor _queryProcessor;
    private readonly ILayeredService<IDifferenceConverter> _layeredService;
    private readonly IPhotoAppService _photoAppService;

    public GetDifferenceHandler(IMessageAppService messageAppService,
        IPtsHelper ptsHelper,
        IQueryProcessor queryProcessor,
        ILogger<GetDifferenceHandler> logger,
        IAckCacheService ackCacheService,
        ILayeredService<IDifferenceConverter> layeredService, IPhotoAppService photoAppService)
    {
        _messageAppService = messageAppService;
        _ptsHelper = ptsHelper;
        _queryProcessor = queryProcessor;
        _logger = logger;
        _ackCacheService = ackCacheService;
        _layeredService = layeredService;
        _photoAppService = photoAppService;
    }

    protected override async Task<MyTelegram.Schema.Updates.IDifference> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Updates.RequestGetDifference obj)
    {
        var userId = input.UserId;
        if (userId == 0)
        {
            return new TDifferenceEmpty
            {
                Date = CurrentDate
            };
        }

        var cachedPts = _ptsHelper.GetCachedPts(userId);
        var ptsReadModel = await _queryProcessor.ProcessAsync(new GetPtsByPeerIdQuery(userId));
        var pts = Math.Max(cachedPts, ptsReadModel?.Pts ?? 0);

        var ptsForAuthKeyIdReadModel =
            await _queryProcessor.ProcessAsync(new GetPtsByPermAuthKeyIdQuery(userId, input.PermAuthKeyId), default)
         ;
        var ptsForAuthKeyId = ptsForAuthKeyIdReadModel?.Pts ?? 0;
        var diff = pts - ptsForAuthKeyId;
        if (diff == 0)
        {
            diff = pts - obj.Pts;
        }

        var globalSeqNo = ptsForAuthKeyIdReadModel?.GlobalSeqNo ?? 0;
        IReadOnlyCollection<IUpdatesReadModel> userUpdates = [];
        var joinedChannelIdList = await _queryProcessor
            .ProcessAsync(new GetChannelIdListByMemberUserIdQuery(input.UserId));

        var limit = obj.PtsTotalLimit ?? MyTelegramServerDomainConsts.DefaultPtsTotalLimit;
        limit = Math.Min(limit, MyTelegramServerDomainConsts.DefaultPtsTotalLimit);

        var updatesReadModels =
            await _queryProcessor.ProcessAsync(new GetUpdatesQuery(input.UserId, input.UserId, obj.Pts, obj.Date, limit));
        var messageIds = updatesReadModels.Where(p => p.UpdatesType == UpdatesType.NewMessages)
                .Select(p => p.MessageId ?? 0)
                .ToList()
            ;

        // all channel updates
        var channelUpdatesReadModels = await _queryProcessor.ProcessAsync(
            new GetChannelUpdatesByGlobalSeqNoQuery(joinedChannelIdList.ToList(), globalSeqNo,
                limit));

        if (channelUpdatesReadModels.Any(p => p.OnlySendToUserId.HasValue))
        {
            var tempChannelReadModels = channelUpdatesReadModels.ToList();
            tempChannelReadModels.RemoveAll(p => p.OnlySendToUserId.HasValue && p.OnlySendToUserId != input.UserId);
            channelUpdatesReadModels = tempChannelReadModels;
        }

        var users = updatesReadModels.SelectMany(p => p.Users ?? new List<long>(0)).ToList();
        var chats = updatesReadModels.SelectMany(p => p.Chats ?? new List<long>(0)).ToList();
        users.AddRange(channelUpdatesReadModels.SelectMany(p => p.Users ?? new List<long>(0)).ToList());
        chats.AddRange(channelUpdatesReadModels.SelectMany(p => p.Chats ?? new List<long>(0)).ToList());
        chats.AddRange(channelUpdatesReadModels.Select(p => p.OwnerPeerId));

        var dto = await _messageAppService
            .GetChannelDifferenceAsync(new GetDifferenceInput(input.UserId, input.UserId,
                obj.Pts,
                limit, messageIds, users, chats));

        var allUpdateList = updatesReadModels.Where(p => p.UpdatesType == UpdatesType.Updates)
            .SelectMany(p => p.Updates ?? new List<IUpdate>()).ToList();
        allUpdateList.AddRange(channelUpdatesReadModels.Where(p => p.UpdatesType == UpdatesType.Updates).SelectMany(p => p.Updates ?? []));
        allUpdateList.AddRange(userUpdates.SelectMany(p => p.Updates ?? []));

        IReadOnlyCollection<IEncryptedMessageReadModel> encryptedMessages = [];

        var maxPts = 0;

        if (updatesReadModels.Count > 0 || channelUpdatesReadModels.Count > 0 || userUpdates.Count > 0)
        {
            maxPts = updatesReadModels.Count > 0 ? updatesReadModels.Max(p => p.Pts) : obj.Pts;
            var channelMaxGlobalSeqNo =
                channelUpdatesReadModels.Count > 0 ? channelUpdatesReadModels.Max(p => p.GlobalSeqNo) : 0; //updatesReadModels.Max(p => p.GlobalSeqNo);
            var userGlobalSeqNo = userUpdates.Count > 0 ? userUpdates.Max(p => p.GlobalSeqNo) : 0;

            var maxGlobalSeqNo = Math.Max(channelMaxGlobalSeqNo, userGlobalSeqNo);

            await _ackCacheService.AddRpcPtsToCacheAsync(input.ReqMsgId,
                maxPts,
                maxGlobalSeqNo,
                new Peer(PeerType.User, input.UserId),
                true
                );
        }
        dto.MessageList = dto.MessageList.OrderBy(p => p.MessageId).ToList();

        return _layeredService.GetConverter(input.Layer).ToDifference(dto, ptsReadModel, cachedPts, limit, allUpdateList, new List<IChat>(), encryptedMessages);
    }
}
