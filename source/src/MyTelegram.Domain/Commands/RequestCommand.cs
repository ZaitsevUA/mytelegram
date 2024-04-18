namespace MyTelegram.Domain.Commands;

public abstract class RequestCommand<TAggregate, TIdentity, TExecutionResult>(
    TIdentity aggregateId,
    long reqMsgId) :
    DistinctCommand<TAggregate, TIdentity, TExecutionResult>(aggregateId),
    IHasRequestMessageId
    where TExecutionResult : IExecutionResult
    where TIdentity : IIdentity
    where TAggregate : IAggregateRoot<TIdentity>
{
    //protected RequestCommand(TIdentity aggregateId, long reqMsgId) : base(aggregateId)
    //{
    //    ReqMsgId = reqMsgId;
    //}

    public long ReqMsgId { get; } = reqMsgId;

    protected override IEnumerable<byte[]> GetSourceIdComponents()
    {
        yield return BitConverter.GetBytes(ReqMsgId);
    }
}
