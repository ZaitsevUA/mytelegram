// ReSharper disable All

namespace MyTelegram.Handlers.Channels;

///<summary>
/// Obtains a list of peers that can be used to send messages in a specific group
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 CHAT_ID_INVALID The provided chat id is invalid.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// See <a href="https://corefork.telegram.org/method/channels.getSendAs" />
///</summary>
internal sealed class GetSendAsHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestGetSendAs, MyTelegram.Schema.Channels.ISendAsPeers>,
    Channels.IGetSendAsHandler
{
    private readonly IQueryProcessor _queryProcessor;
    private readonly ILayeredService<IChatConverter> _layeredChatService;
    private readonly ILayeredService<IUserConverter> _layeredUserService;
    private readonly ILayeredService<ISendAsPeerConverter> _layeredSendAsPeerService;
    private readonly IAccessHashHelper _accessHashHelper;
    private readonly IPhotoAppService _photoAppService;
    public GetSendAsHandler(
        IQueryProcessor queryProcessor,
        ILayeredService<IChatConverter> layeredChatService,
        ILayeredService<IUserConverter> layeredUserService,
        ILayeredService<ISendAsPeerConverter> layeredSendAsPeerService,
        IAccessHashHelper accessHashHelper, IPhotoAppService photoAppService)
    {
        _queryProcessor = queryProcessor;
        _layeredChatService = layeredChatService;
        _layeredUserService = layeredUserService;
        _layeredSendAsPeerService = layeredSendAsPeerService;
        _accessHashHelper = accessHashHelper;
        _photoAppService = photoAppService;
    }

    protected override async Task<MyTelegram.Schema.Channels.ISendAsPeers> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.RequestGetSendAs obj)
    {
        if (obj.Peer is TInputPeerChannel inputPeerChannel)
        {
            await _accessHashHelper.CheckAccessHashAsync(inputPeerChannel.ChannelId, inputPeerChannel.AccessHash);
            var channelReadModels = await _queryProcessor.ProcessAsync(new GetSendAsQuery(inputPeerChannel.ChannelId));
            if (channelReadModels.Any(p => p.CreatorId != input.UserId))
            {
                return _layeredSendAsPeerService.GetConverter(input.Layer).ToSendAsPeers(Array.Empty<IChat>());
            }

            var photoReadModels = await _photoAppService.GetPhotosAsync(channelReadModels);
            var channels = _layeredChatService.GetConverter(input.Layer).ToChannelList(input.UserId, channelReadModels,
                photoReadModels, Array.Empty<long>(), Array.Empty<IChannelMemberReadModel>(), false);

            return _layeredSendAsPeerService.GetConverter(input.Layer).ToSendAsPeers(channels);
        }

        throw new NotImplementedException();
    }
}
