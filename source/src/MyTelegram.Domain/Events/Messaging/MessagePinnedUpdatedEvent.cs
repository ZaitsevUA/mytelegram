namespace MyTelegram.Domain.Events.Messaging;

public class MessagePinnedUpdatedEvent(
    RequestInfo requestInfo,
    long ownerPeerId,
    int messageId,
    bool pinned,
    Peer toPeer) : AggregateEvent<MessageAggregate, MessageId>, IHasRequestInfo
{
    public long OwnerPeerId { get; } = ownerPeerId;
    public int MessageId { get; } = messageId;
    public bool Pinned { get; } = pinned;
    public Peer ToPeer { get; } = toPeer;
    public RequestInfo RequestInfo { get; } = requestInfo;
}