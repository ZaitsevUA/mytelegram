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
internal sealed class UnpinAllMessagesHandler(ICommandBus commandBus, IHandlerHelper handlerHelper)
    : BaseObjectHandler<MyTelegram.Schema.Messages.LayerN.RequestUnpinAllMessages,
            IObject>,
        Messages.IUnpinAllMessagesHandler
{
    protected override async Task<IObject> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.LayerN.RequestUnpinAllMessages obj)
    {
        const uint unpinAllMessagesConstructorId = 0xee22b9a8;
        if (handlerHelper.TryGetHandler(unpinAllMessagesConstructorId, out var handler))
        {
            var response = await handler.HandleAsync(input, new MyTelegram.Schema.Messages.RequestUnpinAllMessages
            {
                Peer = obj.Peer
            });

            return response!;
        }

        return null!;
    }
}
