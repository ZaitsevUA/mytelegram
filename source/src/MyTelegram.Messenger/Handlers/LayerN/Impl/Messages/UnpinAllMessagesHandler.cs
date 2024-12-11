// ReSharper disable All

namespace MyTelegram.Handlers.Messages.LayerN;

///<summary>
/// <a href="https://corefork.telegram.org/api/pin">Unpin</a> all pinned messages
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 400 CHAT_NOT_MODIFIED No changes were made to chat information because the new information you passed is identical to the current information.
/// See <a href="https://corefork.telegram.org/method/messages.unpinAllMessages" />
///</summary>
internal sealed class UnpinAllMessagesHandler(IHandlerHelper handlerHelper)
    : ForwardRequestToNewHandler<MyTelegram.Schema.Messages.LayerN.RequestUnpinAllMessages,
            MyTelegram.Schema.Messages.RequestUnpinAllMessages,
            MyTelegram.Schema.Messages.IAffectedHistory>(handlerHelper),
        Messages.IUnpinAllMessagesHandler
{
    protected override RequestUnpinAllMessages GetNewData(IRequestInput request, Schema.Messages.LayerN.RequestUnpinAllMessages obj)
    {
        return new RequestUnpinAllMessages
        {
            Peer = obj.Peer
        };
    }
}
