﻿// ReSharper disable All

using EventFlow.Queries;

namespace MyTelegram.Handlers.Channels;

///<summary>
/// Change or remove the username of a supergroup/channel
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNELS_ADMIN_PUBLIC_TOO_MUCH You're admin of too many public channels, make some channels private to change the username of this channel.
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 403 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 400 CHAT_NOT_MODIFIED No changes were made to chat information because the new information you passed is identical to the current information.
/// 403 CHAT_WRITE_FORBIDDEN You can't write in this chat.
/// 400 USERNAME_INVALID The provided username is not valid.
/// 400 USERNAME_NOT_MODIFIED The username was not modified.
/// 400 USERNAME_OCCUPIED The provided username is already occupied.
/// 400 USERNAME_PURCHASE_AVAILABLE The specified username can be purchased on <a href="https://fragment.com/">https://fragment.com</a>.
/// See <a href="https://corefork.telegram.org/method/channels.updateUsername" />
///</summary>
internal sealed class UpdateUsernameHandler(
    ICommandBus commandBus,
    IQueryProcessor queryProcessor,
    IAccessHashHelper accessHashHelper)
    : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestUpdateUsername, IBool>,
        Channels.IUpdateUsernameHandler
{
    protected override async Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.RequestUpdateUsername obj)
    {
        if (obj.Channel is TInputChannel inputChannel)
        {
            await accessHashHelper.CheckAccessHashAsync(inputChannel.ChannelId, inputChannel.AccessHash);

            var oldUserName = await queryProcessor.ProcessAsync(new GetChannelUserNameByChannelIdQuery(inputChannel.ChannelId));
            if (string.Equals(obj.Username, oldUserName))
            {
                RpcErrors.RpcErrors400.UsernameNotModified.ThrowRpcError();
            }

            var command = new SetUserNameCommand(UserNameId.Create(obj.Username),
                input.ToRequestInfo(),
                inputChannel.ChannelId.ToChannelPeer(),
                obj.Username,
                oldUserName
                );
            await commandBus.PublishAsync(command, default);

            return new TBoolTrue();
        }

        throw new NotImplementedException();
    }
}
