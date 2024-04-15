namespace MyTelegram.Domain.Sagas;

public class ReadChannelHistorySaga : MyInMemoryAggregateSaga<ReadChannelHistorySaga, ReadChannelHistorySagaId,
        ReadChannelHistorySagaLocator>,
//ISagaIsStartedBy<DialogAggregate, DialogId, ReadChannelInboxMessageEvent>,
//ISagaHandles<DialogAggregate, DialogId, OutboxMessageHasReadEvent>,
//ISagaHandles<DialogAggregate, DialogId, OutboxAlreadyReadEvent>,
//IApply<ReadChannelHistoryCompletedEvent>
ISagaIsStartedBy<DialogAggregate, DialogId, UpdateReadChannelInboxEvent>,
    ISagaHandles<DialogAggregate, DialogId, UpdateReadChannelOutboxEvent>
{
    private readonly ReadChannelHistorySagaState _state = new();

    public ReadChannelHistorySaga(ReadChannelHistorySagaId id
        , IEventStore eventStore) : base(id, eventStore)
    {
        Register(_state);
    }
    //public void Apply(ReadChannelHistoryCompletedEvent aggregateEvent)
    //{
    //    CompleteAsync();
    //}

    //public Task HandleAsync(IDomainEvent<DialogAggregate, DialogId, OutboxAlreadyReadEvent> domainEvent,
    //    ISagaContext sagaContext,
    //    CancellationToken cancellationToken)
    //{
    //    Emit(new ReadChannelHistoryCompletedEvent(_state.RequestInfo,
    //        _state.ChannelId,
    //        0,
    //        0,
    //        false, null));
    //    return Task.CompletedTask;
    //}

    //public Task HandleAsync(IDomainEvent<DialogAggregate, DialogId, OutboxMessageHasReadEvent> domainEvent,
    //    ISagaContext sagaContext,
    //    CancellationToken cancellationToken)
    //{
    //    Emit(new ReadChannelHistoryCompletedEvent(domainEvent.AggregateEvent.RequestInfo,
    //        domainEvent.AggregateEvent.ToPeer.PeerId,
    //        domainEvent.AggregateEvent.OwnerPeerId,
    //        domainEvent.AggregateEvent.MaxMessageId,
    //        true, _state.TopMsgId));
    //    return Task.CompletedTask;
    //}

    //public Task HandleAsync(IDomainEvent<DialogAggregate, DialogId, ReadChannelInboxMessageEvent> domainEvent,
    //    ISagaContext sagaContext,
    //    CancellationToken cancellationToken)
    //{
    //    Emit(new ReadChannelHistoryStartedEvent(domainEvent.AggregateEvent.RequestInfo,
    //        domainEvent.AggregateEvent.ReaderUserId,
    //        domainEvent.AggregateEvent.ChannelId,
    //        domainEvent.AggregateEvent.TopMsgId));

    //    CreateReadingHistory(domainEvent.AggregateEvent.ChannelId, domainEvent.AggregateEvent.MaxId);
    //    UpdateSenderDialog(domainEvent.AggregateEvent.SenderUserId, domainEvent.AggregateEvent.ChannelId,
    //        domainEvent.AggregateEvent.MaxId);

    //    return Task.CompletedTask;
    //}

    //private void UpdateSenderDialog(long senderUserId, long channelId, int readOutboxMaxId)
    //{
    //    var command = new OutboxMessageHasReadCommand(DialogId.Create(senderUserId, PeerType.Channel, channelId),
    //        _state.RequestInfo, readOutboxMaxId, senderUserId, new Peer(PeerType.Channel, channelId));
    //    Publish(command);
    //}

    //private void CreateReadingHistory(long toPeerId,
    //    int senderMsgId)
    //{
    //    var command = new CreateReadingHistoryCommand(
    //        ReadingHistoryId.Create(_state.ReaderUserId, toPeerId, senderMsgId),
    //        _state.ReaderUserId,
    //        toPeerId,
    //        senderMsgId,
    //         DateTime.UtcNow.ToTimestamp()
    //        );
    //    Publish(command);
    //}
    public Task HandleAsync(IDomainEvent<DialogAggregate, DialogId, UpdateReadChannelInboxEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
    {
        Emit(new ReadChannelHistoryStartedEvent(domainEvent.AggregateEvent.RequestInfo,
                    domainEvent.AggregateEvent.ChannelId));

        var command = new UpdateReadChannelOutboxCommand(
            DialogId.Create(domainEvent.AggregateEvent.MessageSenderUserId, PeerType.Channel,
                domainEvent.AggregateEvent.ChannelId),
            _state.RequestInfo,
            domainEvent.AggregateEvent.MaxId);
        Publish(command);

        CreateReadingHistory(domainEvent.AggregateEvent.ChannelId, domainEvent.AggregateEvent.MaxId);

        return Task.CompletedTask;
    }

    private void CreateReadingHistory(long channelId, int messageId)
    {
        var command = new CreateReadingHistoryCommand(
            ReadingHistoryId.Create(_state.RequestInfo.UserId, channelId, messageId), _state.RequestInfo.UserId, channelId,
            messageId, DateTime.UtcNow.ToTimestamp());
        Publish(command);
    }

    public Task HandleAsync(IDomainEvent<DialogAggregate, DialogId, UpdateReadChannelOutboxEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
    {
        Emit(new ReadChannelHistoryCompletedEvent(domainEvent.AggregateEvent.RequestInfo, domainEvent.AggregateEvent.ChannelId, domainEvent.AggregateEvent.MessageSenderUserId, domainEvent.AggregateEvent.MaxId));
        return CompleteAsync(cancellationToken);
    }
}
