namespace MyTelegram.Domain.Events.RpcResult;

public class RpcResultCreatedEvent : RequestAggregateEvent2<RpcResultAggregate, RpcResultId>
{
    public RpcResultCreatedEvent(RequestInfo requestInfo,
        byte[] rpcData, int date) : base(requestInfo)
    {
        RpcData = rpcData;
        Date = date;
    }

    public byte[] RpcData { get; }
    public int Date { get; }
}
