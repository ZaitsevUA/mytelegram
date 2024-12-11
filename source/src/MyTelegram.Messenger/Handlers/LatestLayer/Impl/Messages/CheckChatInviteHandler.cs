// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Check the validity of a chat invite link and get basic info about it
/// <para>Possible errors</para>
/// Code Type Description
/// 406 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 500 CHAT_MEMBERS_CHANNEL
/// 400 INVITE_HASH_EMPTY The invite hash is empty.
/// 406 INVITE_HASH_EXPIRED The invite link has expired.
/// 400 INVITE_HASH_INVALID The invite hash is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.checkChatInvite" />
///</summary>
internal sealed class CheckChatInviteHandler(
    IQueryProcessor queryProcessor,
    IChannelAppService channelAppService,
    IPhotoAppService photoAppService,
    ILayeredService<IChatConverter> layeredChatService,
    ILayeredService<IPhotoConverter> layeredPhotoService)
    : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestCheckChatInvite, MyTelegram.Schema.IChatInvite>,
        Messages.ICheckChatInviteHandler
{
    protected override async Task<IChatInvite> HandleCoreAsync(IRequestInput input,
        RequestCheckChatInvite obj)
    {
        if (string.IsNullOrEmpty(obj.Hash))
        {
            RpcErrors.RpcErrors400.InviteHashEmpty.ThrowRpcError();
        }

        var chatInviteReadModel = await queryProcessor.ProcessAsync(new GetChatInviteByLinkQuery(obj.Hash));
        if (chatInviteReadModel == null)
        {
            RpcErrors.RpcErrors400.InviteHashInvalid.ThrowRpcError();
        }

        if (chatInviteReadModel!.ExpireDate > 0)
        {
            if (chatInviteReadModel.ExpireDate.Value < CurrentDate)
            {
                RpcErrors.RpcErrors400.InviteHashInvalid.ThrowRpcError();
            }
        }

        var channelReadModel = await channelAppService.GetAsync(chatInviteReadModel.PeerId);
        if (channelReadModel == null)
        {
            RpcErrors.RpcErrors400.ChannelIdInvalid.ThrowRpcError();
        }
        var channelMemberReadModel =
            await queryProcessor.ProcessAsync(new GetChannelMemberByUserIdQuery(channelReadModel!.ChannelId,
                input.UserId));
        var chatPhoto = await photoAppService.GetAsync(channelReadModel.PhotoId);
        if (channelMemberReadModel != null)
        {
            return new TChatInviteAlready
            {
                Chat = layeredChatService.GetConverter(input.Layer).ToChannel(input.UserId, channelReadModel,
                    chatPhoto, channelMemberReadModel, false)
            };
        }

        return new TChatInvite
        {
            About = channelReadModel.About,
            Broadcast = channelReadModel.Broadcast,
            Channel = true,
            Megagroup = channelReadModel.MegaGroup,
            ParticipantsCount = channelReadModel.ParticipantsCount ?? 0,
            Photo = layeredPhotoService.GetConverter(input.Layer).ToPhoto(chatPhoto),
            RequestNeeded = chatInviteReadModel.RequestNeeded,
            Title = channelReadModel.Title,
        };
    }
}
