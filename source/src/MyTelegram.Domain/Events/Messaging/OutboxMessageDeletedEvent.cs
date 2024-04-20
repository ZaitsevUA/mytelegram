namespace MyTelegram.Domain.Events.Messaging;

public class OutboxMessageDeletedEvent(
    RequestInfo requestInfo,
    long ownerPeerId,
    int messageId,
    IReadOnlyCollection<InboxItem>? inboxItems)
    : AggregateEvent<MessageAggregate, MessageId>, IHasRequestInfo
{
    public IReadOnlyCollection<InboxItem>? InboxItems { get; } = inboxItems;
    public int MessageId { get; } = messageId;
    public RequestInfo RequestInfo { get; } = requestInfo;
    public long OwnerPeerId { get; } = ownerPeerId;
}
