namespace MyTelegram.Domain.Events.Dialog;

public class DialogUpdatedEvent(long ownerUserId, Peer toPeer, int topMessageId, int pts)
    : AggregateEvent<DialogAggregate, DialogId>
{
    public long OwnerUserId { get; } = ownerUserId;
    public Peer ToPeer { get; } = toPeer;
    public int TopMessageId { get; } = topMessageId;
    public int Pts { get; } = pts;
}