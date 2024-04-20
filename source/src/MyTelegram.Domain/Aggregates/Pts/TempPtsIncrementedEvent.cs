namespace MyTelegram.Domain.Aggregates.Pts;

public class TempPtsIncrementedEvent(long ownerPeerId, int newPts, long permAuthKeyId, int date)
    : AggregateEvent<TempPtsAggregate, TempPtsId>
{
    public long OwnerPeerId { get; } = ownerPeerId;
    public int NewPts { get; } = newPts;
    public long PermAuthKeyId { get; } = permAuthKeyId;
    public int Date { get; } = date;
}