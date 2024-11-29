// ReSharper disable All

namespace MyTelegram.Handlers.Channels.LayerN;

///<summary>
/// Get info about a <a href="https://corefork.telegram.org/api/channel">channel/supergroup</a> participant
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 406 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 403 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 400 PARTICIPANT_ID_INVALID The specified participant ID is invalid.
/// 400 USER_ID_INVALID The provided user ID is invalid.
/// 400 USER_NOT_PARTICIPANT You're not a member of this supergroup/channel.
/// See <a href="https://corefork.telegram.org/method/channels.getParticipant" />
///</summary>
internal sealed class GetParticipantHandler(
    IQueryProcessor queryProcessor,
    IUserAppService userAppService,
    IPeerHelper peerHelper,
    ILayeredService<IChatConverter> layeredService,
    IAccessHashHelper accessHashHelper,
    ILayeredService<IUserConverter> layeredUserService,
    ILayeredService<IPhotoConverter> layeredPhotoService,
    IChannelAppService channelAppService,
    IPhotoAppService photoAppService,
    IPrivacyAppService privacyAppService)
    : RpcResultObjectHandler<MyTelegram.Schema.Channels.LayerN.RequestGetParticipant,
            MyTelegram.Schema.Channels.IChannelParticipant>,
        Channels.LayerN.IGetParticipantHandler
{
    private readonly ILayeredService<IPhotoConverter> _layeredPhotoService = layeredPhotoService;

    protected override async Task<MyTelegram.Schema.Channels.IChannelParticipant> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.LayerN.RequestGetParticipant obj)
    {
        var peer = peerHelper.GetPeer(obj.UserId, input.UserId);
        if (obj.Channel is TInputChannel inputChannel)
        {
            await accessHashHelper.CheckAccessHashAsync(inputChannel.ChannelId, inputChannel.AccessHash);

            var channelMemberReadModel = await queryProcessor
                    .ProcessAsync(new GetChannelMemberByUserIdQuery(inputChannel.ChannelId, peer.PeerId), default)
                ;

            if (channelMemberReadModel == null)
            {
                RpcErrors.RpcErrors400.UserNotParticipant.ThrowRpcError();
            }

            var userReadModel = await userAppService.GetAsync(channelMemberReadModel!.UserId);

            if (userReadModel == null)
            {
                RpcErrors.RpcErrors400.UserIdInvalid.ThrowRpcError();
            }

            var channelReadModel = await channelAppService.GetAsync(inputChannel.ChannelId);

            var privacies = await privacyAppService.GetPrivacyListAsync(userReadModel!.UserId);
            var photos = await photoAppService.GetPhotosAsync(userReadModel);
            var user = layeredUserService.GetConverter(input.Layer)
                .ToUser(input.UserId, userReadModel, photos, privacies: privacies);
            var chatPhoto = await photoAppService.GetAsync(channelReadModel!.PhotoId);

            return layeredService.GetConverter(input.Layer).ToChannelParticipantLayerN(
                input.UserId,
                channelReadModel,
                chatPhoto,
                channelMemberReadModel,
                user);
        }

        throw new NotImplementedException();
    }
}
