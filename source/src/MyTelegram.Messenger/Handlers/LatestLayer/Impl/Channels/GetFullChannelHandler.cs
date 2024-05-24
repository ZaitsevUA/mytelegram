// ReSharper disable All

namespace MyTelegram.Handlers.Channels;

///<summary>
/// Get full info about a <a href="https://corefork.telegram.org/api/channel#supergroups">supergroup</a>, <a href="https://corefork.telegram.org/api/channel#gigagroups">gigagroup</a> or <a href="https://corefork.telegram.org/api/channel#channels">channel</a>
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 406 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 403 CHANNEL_PUBLIC_GROUP_NA channel/supergroup not available.
/// 400 CHAT_NOT_MODIFIED No changes were made to chat information because the new information you passed is identical to the current information.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// See <a href="https://corefork.telegram.org/method/channels.getFullChannel" />
///</summary>
internal sealed class GetFullChannelHandler(
    IQueryProcessor queryProcessor,
    ILayeredService<IChatConverter> layeredService,
    IAccessHashHelper accessHashHelper,
    IPhotoAppService photoAppService,
    ILogger<GetFullChannelHandler> logger,
    IOptions<MyTelegramMessengerServerOptions> options,
    IChatInviteLinkHelper chatInviteLinkHelper)
    : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestGetFullChannel, MyTelegram.Schema.Messages.IChatFull>,
        Channels.IGetFullChannelHandler
{
    protected override async Task<MyTelegram.Schema.Messages.IChatFull> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.RequestGetFullChannel obj)
    {
        if (obj.Channel is TInputChannel inputChannel)
        {
            await accessHashHelper.CheckAccessHashAsync(inputChannel.ChannelId, inputChannel.AccessHash);

            var channel = await queryProcessor.ProcessAsync(new GetChannelByIdQuery(inputChannel.ChannelId));
            channel.ThrowExceptionIfChannelDeleted();

            var channelFull = await queryProcessor.ProcessAsync(new GetChannelFullByIdQuery(inputChannel.ChannelId));

            var dialogReadModel = await queryProcessor.ProcessAsync(
                new GetDialogByIdQuery(DialogId.Create(input.UserId, PeerType.Channel, inputChannel.ChannelId)));
            if (dialogReadModel == null)
            {
                logger.LogWarning("Dialog not exists,userId={UserId},ToPeer={ToPeer}", input.UserId, new Peer(PeerType.Channel, inputChannel.ChannelId));
            }
            else
            {
                channelFull!.ReadInboxMaxId = dialogReadModel.ReadInboxMaxId;
                channelFull.ReadOutboxMaxId = dialogReadModel.ReadOutboxMaxId;
                //channelFull.UnreadCount = dialogReadModel.UnreadCount;
                var maxId = new[]{dialogReadModel.ReadInboxMaxId, dialogReadModel.ReadOutboxMaxId,
                    dialogReadModel.ChannelHistoryMinId}.Max();
                channelFull.UnreadCount = channel!.TopMessageId - maxId;
            }

            // Console.WriteLine($"# GetFullChannel:{input.UserId} {channelFull.ChannelId} TopMessageId:{channel.TopMessageId}  ReadInboxMaxId:{channelFull.ReadInboxMaxId} ReadOutboxMaxId:{channelFull.ReadOutboxMaxId} UnreadCount:{channelFull.UnreadCount} Pts:{channel.Pts}");

            var channelMember = await queryProcessor
                .ProcessAsync(new GetChannelMemberByUserIdQuery(inputChannel.ChannelId, input.UserId));
            var migratedFromChatReadModel = channelFull!.MigratedFromChatId == null ? null :
                await queryProcessor.ProcessAsync(new GetChatByChatIdQuery(channelFull.MigratedFromChatId.Value));

            var peerNotifySettings = await queryProcessor
                .ProcessAsync(
                    new GetPeerNotifySettingsByIdQuery(PeerNotifySettingsId.Create(input.UserId,
                        PeerType.Channel,
                        inputChannel.ChannelId)));
            //var chatPhoto = _layeredPhotoService.GetConverter(input.Layer).ToChatPhoto(channel!.Photo);
            var photoReadModel = await photoAppService.GetPhotoAsync(channel!.PhotoId);
            IChatInviteReadModel? chatInviteReadModel = null;
            if (channel.AdminList.Any(p => p.UserId == input.UserId))
            {
                chatInviteReadModel =
                    await queryProcessor.ProcessAsync(new GetPermanentChatInviteQuery(inputChannel.ChannelId));
                if (chatInviteReadModel != null)
                {
                    //chatInviteReadModel.Link = $"{_options.Value.JoinChatDomain}/+{chatInviteReadModel.Link}";
                    chatInviteReadModel.Link =
                        chatInviteLinkHelper.GetFullLink(options.Value.JoinChatDomain, chatInviteReadModel.Link);
                }
            }

            var r = layeredService.GetConverter(input.Layer).ToChatFull(
                input.UserId,
                channel!,
                photoReadModel,
                channelFull!,
                channelMember,
                peerNotifySettings,
                migratedFromChatReadModel,
                chatInviteReadModel
                );

            if (channelFull!.LinkedChatId.HasValue)
            {
                var linkedChannelReadModel = await queryProcessor.ProcessAsync(new GetChannelByIdQuery(channelFull.LinkedChatId.Value));
                if (linkedChannelReadModel != null)
                {
                    var linkedChannelPhotoReadModel = await photoAppService.GetPhotoAsync(linkedChannelReadModel.PhotoId);
                    var linkedChannel = layeredService.GetConverter(input.Layer).ToChannel(input.UserId,
                        linkedChannelReadModel, linkedChannelPhotoReadModel, null, false);

                    r.Chats.Add(linkedChannel);
                }
            }

            //_logger.LogInformation("GetFullChannel:{UserId},Data={@Data}", input.UserId, r);

            return r;
        }

        throw new NotImplementedException();
    }
}
