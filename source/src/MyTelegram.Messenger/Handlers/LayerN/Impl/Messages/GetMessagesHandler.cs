﻿// ReSharper disable All

namespace MyTelegram.Handlers.Messages.LayerN;

///<summary>
/// Returns the list of messages by their IDs.
/// See <a href="https://corefork.telegram.org/method/messages.getMessages" />
///</summary>
internal sealed class GetMessagesHandler(
    IHandlerHelper handlerHelper)
    : ForwardRequestToNewHandler<
            MyTelegram.Schema.Messages.LayerN.RequestGetMessages,
            MyTelegram.Schema.Messages.RequestGetMessages>(handlerHelper),
        Messages.LayerN.IGetMessagesHandler
{
    protected override Schema.Messages.RequestGetMessages GetNewData(IRequestInput request, Schema.Messages.LayerN.RequestGetMessages obj)
    {
        return new Schema.Messages.RequestGetMessages
        {
            Id = new(obj.Id.Select(p => new TInputMessageID
            {
                Id = p
            }))
        };
    }
}
