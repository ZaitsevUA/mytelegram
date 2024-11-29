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
internal sealed class GetSendAsHandler(
    IQueryProcessor queryProcessor,
    IUserAppService userAppService,
    ILayeredService<IChatConverter> layeredChatService,
    ILayeredService<IUserConverter> layeredUserService,
    ILayeredService<ISendAsPeerConverter> layeredSendAsPeerService,
    IAccessHashHelper accessHashHelper,
    IPhotoAppService photoAppService)
    : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestGetSendAs, MyTelegram.Schema.Channels.ISendAsPeers>,
        Channels.IGetSendAsHandler
{
    protected override async Task<MyTelegram.Schema.Channels.ISendAsPeers> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.RequestGetSendAs obj)
    {
        if (obj.Peer is TInputPeerChannel inputPeerChannel)
        {
            await accessHashHelper.CheckAccessHashAsync(inputPeerChannel.ChannelId, inputPeerChannel.AccessHash);
            var channelReadModels = await queryProcessor.ProcessAsync(new GetSendAsQuery(inputPeerChannel.ChannelId));
            if (channelReadModels.Any(p => p.CreatorId != input.UserId))
            {
                var userReadModel = await userAppService.GetAsync(input.UserId);
                var userPhotoReadModels = await photoAppService.GetPhotosAsync(userReadModel);
                var user = layeredUserService.GetConverter(input.Layer)
                    .ToUser(input.UserId, userReadModel!, userPhotoReadModels);

                return new TSendAsPeers
                {
                    Chats = [],
                    Peers = new TVector<ISendAsPeer>([new TSendAsPeer
                    {
                        Peer=new TPeerUser
                        {
                            UserId=input.UserId,
                        }
                    }]),
                    Users = new TVector<IUser>([user])
                };
            }

            var photoReadModels = await photoAppService.GetPhotosAsync(channelReadModels);
            var channels = layeredChatService.GetConverter(input.Layer).ToChannelList(input.UserId, channelReadModels,
                photoReadModels, Array.Empty<long>(), Array.Empty<IChannelMemberReadModel>(), false);

            return layeredSendAsPeerService.GetConverter(input.Layer).ToSendAsPeers(channels);
        }

        throw new NotImplementedException();
    }
}
