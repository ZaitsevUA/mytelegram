namespace MyTelegram.Domain.Sagas.States;

public class ReadChannelHistorySagaState : AggregateState<ReadChannelHistorySaga, ReadChannelHistorySagaId,
        ReadChannelHistorySagaState>,
    IApply<ReadChannelHistoryStartedEvent>,
    IApply<ReadChannelHistoryCompletedEvent>
{
    public RequestInfo RequestInfo { get; set; } = null!;
    public long ChannelId { get; private set; }

    public void Apply(ReadChannelHistoryStartedEvent aggregateEvent)
    {
        RequestInfo = aggregateEvent.RequestInfo;
        ChannelId = aggregateEvent.ChannelId;
    }

    public void Apply(ReadChannelHistoryCompletedEvent aggregateEvent)
    {
    }
}
