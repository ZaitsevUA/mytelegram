namespace MyTelegram.ReadModel.Impl;

public class RpcResultReadModel : IRpcResultReadModel,
    IAmReadModelFor<RpcResultAggregate, RpcResultId, RpcResultCreatedEvent>
{
    public virtual long? Version { get; set; }

    public Task ApplyAsync(IReadModelContext context,
        IDomainEvent<RpcResultAggregate, RpcResultId, RpcResultCreatedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        Id = domainEvent.AggregateIdentity.Value;
        UserId = domainEvent.AggregateEvent.RequestInfo.UserId;
        ReqMsgId = domainEvent.AggregateEvent.RequestInfo.ReqMsgId;
        //PeerId = domainEvent.AggregateEvent.PeerId;
        //SourceId = domainEvent.AggregateEvent.SourceId;
        RpcData = domainEvent.AggregateEvent.RpcData;
        Date = domainEvent.AggregateEvent.Date;

        return Task.CompletedTask;
    }

    //public virtual long PeerId { get; private set; }
    //public virtual string SourceId { get; private set; } = null!;
    public int Date { get; private set; }

    public virtual string Id { get; private set; } = null!;
    public virtual long ReqMsgId { get; private set; }
    public virtual byte[] RpcData { get; private set; } = null!;
    public virtual long UserId { get; private set; }
}