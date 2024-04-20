using MyTelegram.Domain.Aggregates.Temp;
using MyTelegram.Domain.Events.Temp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTelegram.Domain.Sagas;
[JsonConverter(typeof(SystemTextJsonSingleValueObjectConverter<UpdateMessageReplySagaId>))]
public class UpdateMessageReplySagaId(string value) : Identity<UpdateMessageReplySagaId>(value), ISagaId;

public class
    UpdateMessageReplySagaLocator : DefaultSagaLocator<UpdateMessageReplySaga, UpdateMessageReplySagaId>
{
    protected override UpdateMessageReplySagaId CreateSagaId(string requestId)
    {
        return new UpdateMessageReplySagaId(requestId);
    }
}

public class UpdateMessageReplySagaState : AggregateState<UpdateMessageReplySaga, UpdateMessageReplySagaId,
    UpdateMessageReplySagaState>,
    IApply<UpdateMessageReplySagaStartedEvent>,
    IApply<DiscussionGroupUpdatedSagaEvent>
{
    public RequestInfo RequestInfo { get; private set; } = default!;

    public void Apply(UpdateMessageReplySagaStartedEvent aggregateEvent)
    {
        RequestInfo = aggregateEvent.RequestInfo;
    }

    public void Apply(DiscussionGroupUpdatedSagaEvent aggregateEvent)
    {
    }
}

public class UpdateMessageReplySagaStartedEvent(RequestInfo requestInfo) : AggregateEvent<UpdateMessageReplySaga, UpdateMessageReplySagaId>
{
    public RequestInfo RequestInfo { get; } = requestInfo;
}

public class UpdateMessageReplySaga :
    MyInMemoryAggregateSaga<UpdateMessageReplySaga,
        UpdateMessageReplySagaId, UpdateMessageReplySagaLocator>
{
    private readonly UpdateMessageReplySagaState _state = new();

    public UpdateMessageReplySaga(UpdateMessageReplySagaId id, IEventStore eventStore, IIdGenerator idGenerator) : base(id, eventStore)
    {
        Register(_state);
    }

    
}
