namespace MyTelegram.Domain.Events.Dialog;

public class ReadInboxMaxIdUpdatedEvent : RequestAggregateEvent2<DialogAggregate, DialogId>
{
    public long OwnerUserId { get; }
    public long ToPeerId { get; }
    public int ReadInboxMaxId { get; }
    public long SenderUserId { get; }
    public int SenderMessageId { get; }

    public ReadInboxMaxIdUpdatedEvent(RequestInfo requestInfo, long ownerUserId, long toPeerId, int readInboxMaxId, long senderUserId,
        int senderMessageId) : base(requestInfo)
    {
        OwnerUserId = ownerUserId;
        ToPeerId = toPeerId;
        ReadInboxMaxId = readInboxMaxId;
        SenderUserId = senderUserId;
        SenderMessageId = senderMessageId;
    }
}