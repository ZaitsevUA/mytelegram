using MyTelegram.Domain.Aggregates.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace MyTelegram.Domain.Sagas;


[JsonConverter(typeof(SystemTextJsonSingleValueObjectConverter<PinForwardedChannelMessageSagaId>))]
public class PinForwardedChannelMessageSagaId : Identity<PinForwardedChannelMessageSagaId>, ISagaId
{
    public PinForwardedChannelMessageSagaId(string value) : base(value)
    {
    }
}

public class
    PinChannelMessagePtsIncrementedEvent(long channelId, int messageId, int pts) : AggregateEvent<PinForwardedChannelMessageSaga,
        PinForwardedChannelMessageSagaId>
{
    public long ChannelId { get; private set; } = channelId;
    public int MessageId { get; } = messageId;
    public int Pts { get; private set; } = pts;
}

public class
    PinForwardedChannelMessageSagaLocator : DefaultSagaLocator<PinForwardedChannelMessageSaga, PinForwardedChannelMessageSagaId>
{
    protected override PinForwardedChannelMessageSagaId CreateSagaId(string requestId)
    {
        return new PinForwardedChannelMessageSagaId(requestId);
    }
}

public class PinForwardedChannelMessageSaga(PinForwardedChannelMessageSagaId id, IEventStore eventStore, IIdGenerator idGenerator) : MyInMemoryAggregateSaga<PinForwardedChannelMessageSaga,
    PinForwardedChannelMessageSagaId, PinForwardedChannelMessageSagaLocator>(id, eventStore),
    ISagaIsStartedBy<MessageAggregate, MessageId, ChannelMessagePinnedEvent>,
    IApply<PinChannelMessagePtsIncrementedEvent>
{
    public async Task HandleAsync(IDomainEvent<MessageAggregate, MessageId, ChannelMessagePinnedEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
    {
        await IncrementPtsAsync(domainEvent.AggregateEvent.ChannelId, domainEvent.AggregateEvent.MessageId);
    }

    private async Task IncrementPtsAsync(long channelId, int messageId)
    {
        var pts = await idGenerator.NextIdAsync(IdType.Pts, channelId);
        Emit(new PinChannelMessagePtsIncrementedEvent(channelId, messageId, pts));
    }

    public void Apply(PinChannelMessagePtsIncrementedEvent aggregateEvent)
    {
        CompleteAsync();
    }
}