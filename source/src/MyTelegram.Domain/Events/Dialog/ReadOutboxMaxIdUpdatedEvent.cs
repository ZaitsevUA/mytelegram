namespace MyTelegram.Domain.Events.Dialog;

public class ReadOutboxMaxIdUpdatedEvent : RequestAggregateEvent2<DialogAggregate, DialogId>
{
    public long OwnerUserId { get; }
    public long ToPeerId { get; }
    public int ReadOutboxMaxId { get; }
    public ReadOutboxMaxIdUpdatedEvent(RequestInfo requestInfo, long ownerUserId, long toPeerId, int readOutboxMaxId) : base(requestInfo)
    {
        OwnerUserId = ownerUserId;
        ToPeerId = toPeerId;
        ReadOutboxMaxId = readOutboxMaxId;
    }
}