namespace MyTelegram.Domain.Commands.RpcResult;

public class CreateRpcResultCommand : RequestCommand2<RpcResultAggregate, RpcResultId, IExecutionResult>
{
    public CreateRpcResultCommand(RpcResultId aggregateId,
        RequestInfo requestInfo,
        byte[] rpcData) : base(aggregateId, requestInfo)
    {
        RpcData = rpcData;
    }

    public byte[] RpcData { get; }
}
