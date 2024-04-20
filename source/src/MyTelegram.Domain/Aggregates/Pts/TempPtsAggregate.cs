namespace MyTelegram.Domain.Aggregates.Pts;

public class TempPtsAggregate(TempPtsId id) : AggregateRoot<TempPtsAggregate, TempPtsId>(id), INotSaveAggregateEvents,
    IApply<TempPtsIncrementedEvent>
{
    public void IncrementPts(long ownerPeerId, int newPts, long permAuthKeyId)
    {
        Emit(new TempPtsIncrementedEvent(ownerPeerId, newPts, permAuthKeyId, DateTime.UtcNow.ToTimestamp()));
    }

    public void Apply(TempPtsIncrementedEvent aggregateEvent)
    {
        //throw new NotImplementedException();
    }
}