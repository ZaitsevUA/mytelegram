namespace MyTelegram.Domain.Aggregates.RpcResult;

public class RpcResultState : AggregateState<RpcResultAggregate, RpcResultId, RpcResultState>,
    IApply<RpcResultCreatedEvent>
{
    public RequestInfo RequestInfo { get; private set; } = default!;
    public byte[] RpcData { get; private set; } = default!;

    public void Apply(RpcResultCreatedEvent aggregateEvent)
    {
        RequestInfo = aggregateEvent.RequestInfo;
        RpcData = aggregateEvent.RpcData;
    }
}
