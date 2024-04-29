// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Make a user admin in a <a href="https://corefork.telegram.org/api/channel#basic-groups">basic group</a>.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHAT_ID_INVALID The provided chat id is invalid.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// 400 USER_ID_INVALID The provided user ID is invalid.
/// 400 USER_NOT_PARTICIPANT You're not a member of this supergroup/channel.
/// See <a href="https://corefork.telegram.org/method/messages.editChatAdmin" />
///</summary>
internal sealed class EditChatAdminHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestEditChatAdmin, IBool>,
    Messages.IEditChatAdminHandler
{
    private readonly ICommandBus _commandBus;
    private readonly IPeerHelper _peerHelper;

    public EditChatAdminHandler(ICommandBus commandBus, IPeerHelper peerHelper)
    {
        _commandBus = commandBus;
        _peerHelper = peerHelper;
    }

    protected override async Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestEditChatAdmin obj)
    {
        var peer = _peerHelper.GetPeer(obj.UserId);
        var isBot = _peerHelper.IsBotUser(peer.PeerId);
        var command = new EditChatAdminCommand(ChatId.Create(obj.ChatId), input.ToRequestInfo(),
            input.UserId,
            false,
            peer.PeerId,
            isBot,
            new ChatAdminRights(true, true, true, true, true, true, true, false, true, true, true, true, true, true, true),
            string.Empty,
            CurrentDate
        );

        await _commandBus.PublishAsync(command, default);

        return new TBoolTrue();
    }
}
