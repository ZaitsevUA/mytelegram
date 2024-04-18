namespace MyTelegram.Domain.Events.Dialog;

public class InboxMessageReceivedEvent(
    RequestInfo requestInfo,
    int messageId,
    long ownerPeerId,
    Peer toPeer)
    : RequestAggregateEvent2<DialogAggregate, DialogId>(requestInfo)
{
    public int MessageId { get; } = messageId;
    public long OwnerPeerId { get; } = ownerPeerId;
    public Peer ToPeer { get; } = toPeer;
}