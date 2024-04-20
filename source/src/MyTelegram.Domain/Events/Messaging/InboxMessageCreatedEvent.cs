namespace MyTelegram.Domain.Events.Messaging;

public class InboxMessageCreatedEvent(
    RequestInfo requestInfo,
    MessageItem inboxMessageItem,
    int senderMessageId)
    : RequestAggregateEvent2<MessageAggregate, MessageId>(requestInfo)
{
    public MessageItem InboxMessageItem { get; } = inboxMessageItem;
    public int SenderMessageId { get; } = senderMessageId;
}