namespace MyTelegram.Domain.Aggregates.Pts;

public class IncrementTempPtsCommand(TempPtsId aggregateId, long ownerPeerId, int newPts, long permAuthKeyId = 0)
    : Command<TempPtsAggregate, TempPtsId, IExecutionResult>(aggregateId)
{
    public long OwnerPeerId { get; } = ownerPeerId;
    public int NewPts { get; } = newPts;
    public long PermAuthKeyId { get; } = permAuthKeyId;
}