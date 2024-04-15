namespace MyTelegram.Domain.Events.Messaging;

public class OutboxMessageDeletedEvent : AggregateEvent<MessageAggregate, MessageId>, IHasRequestInfo
{
    public OutboxMessageDeletedEvent(
        RequestInfo requestInfo,
        long ownerPeerId,
        int messageId,
        IReadOnlyCollection<InboxItem>? inboxItems)
    {
        RequestInfo = requestInfo;
        OwnerPeerId = ownerPeerId;
        MessageId = messageId;
        InboxItems = inboxItems;
    }

    public IReadOnlyCollection<InboxItem>? InboxItems { get; }
    public int MessageId { get; }
    public RequestInfo RequestInfo { get; }
    public long OwnerPeerId { get; }
}
