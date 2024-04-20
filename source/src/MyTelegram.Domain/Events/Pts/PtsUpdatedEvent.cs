namespace MyTelegram.Domain.Events.Pts;

public class PtsUpdatedEvent(
    long peerId,
    long permAuthKeyId,
    int newPts,
    int date,
    long globalSeqNo,
    int changedUnreadCount)
    : AggregateEvent<PtsAggregate, PtsId>
{
    //bool incrementUnreadCount
    //IncrementUnreadCount = incrementUnreadCount;

    public int NewPts { get; } = newPts;
    public int Date { get; } = date;
    public long GlobalSeqNo { get; } = globalSeqNo;

    public int ChangedUnreadCount { get; } = changedUnreadCount;
    //public bool IncrementUnreadCount { get; }

    public long PeerId { get; } = peerId;
    public long PermAuthKeyId { get; } = permAuthKeyId;
}