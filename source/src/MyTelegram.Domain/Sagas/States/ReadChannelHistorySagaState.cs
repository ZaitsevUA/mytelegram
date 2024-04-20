namespace MyTelegram.Domain.Sagas.States;

public class ReadChannelHistorySagaState : AggregateState<ReadChannelHistorySaga, ReadChannelHistorySagaId,
        ReadChannelHistorySagaState>,
    IApply<ReadChannelHistoryStartedEvent>,
    IApply<ReadChannelHistoryCompletedEvent>
{
    public RequestInfo RequestInfo { get; set; } = null!;
    public long ChannelId { get; private set; }
    //public long ReaderUserId { get; private set; }

    //public bool NeedWa
    //public int? TopMsgId { get; private set; }

    public void Apply(ReadChannelHistoryStartedEvent aggregateEvent)
    {
        RequestInfo = aggregateEvent.RequestInfo;
        ChannelId = aggregateEvent.ChannelId;
    }

    //public void LoadSnapshot(ReadChannelHistorySagaSnapshot snapshot)
    //{
    //    ReqMsgId = snapshot.ReqMsgId;
    //    ReaderUid = snapshot.ReaderUid;
    //    ChannelId = snapshot.ChannelId;
    //    CorrelationId = snapshot.CorrelationId;
    //}
    public void Apply(ReadChannelHistoryCompletedEvent aggregateEvent)
    {
        //throw new NotImplementedException();
    }
}
