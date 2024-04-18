using MyTelegram.Domain.Aggregates.Messaging;
using MyTelegram.Domain.Events.Messaging;
using MyTelegram.Messenger.Services.Caching;
using SendOutboxMessageCompletedEvent = MyTelegram.Domain.Sagas.Events.SendOutboxMessageCompletedEvent;

namespace MyTelegram.Messenger.CommandServer.DomainEventHandlers;

public class PtsEventHandler(IPtsHelper ptsHelper) :
    ISubscribeSynchronousTo<UpdatePinnedMessageSaga, UpdatePinnedMessageSagaId, UpdatePinnedBoxPtsCompletedEvent>,
    ISubscribeSynchronousTo<SendMessageSaga, SendMessageSagaId, SendOutboxMessageCompletedEvent>,
    ISubscribeSynchronousTo<SendMessageSaga, SendMessageSagaId, ReceiveInboxMessageCompletedEvent>,
    ISubscribeSynchronousTo<ClearHistorySaga, ClearHistorySagaId, ClearSingleUserHistoryCompletedEvent>,
    ISubscribeSynchronousTo<DeleteMessagesSaga4, DeleteMessagesSaga4Id, DeleteMessagePtsIncrementedEvent4>,
    ISubscribeSynchronousTo<ReadHistorySaga, ReadHistorySagaId, ReadHistoryPtsIncrementEvent>,
    ISubscribeSynchronousTo<ReadHistorySaga, ReadHistorySagaId, ReadHistoryPtsIncrementedSagaEvent>,
    ISubscribeSynchronousTo<EditMessageSaga, EditMessageSagaId, OutboxMessageEditCompletedEvent>,
    ISubscribeSynchronousTo<EditMessageSaga, EditMessageSagaId, InboxMessageEditCompletedEvent>,
    ISubscribeSynchronousTo<DeleteChannelMessagesSaga, DeleteChannelMessagesSagaId,
        DeleteChannelMessagePtsIncrementedEvent>,
    ISubscribeSynchronousTo<PinForwardedChannelMessageSaga, PinForwardedChannelMessageSagaId,
        PinChannelMessagePtsIncrementedEvent>,
    ISubscribeSynchronousTo<MessageAggregate, MessageId, MessageReplyUpdatedEvent>,
    ISubscribeSynchronousTo<DeleteReplyMessagesSaga, DeleteReplyMessagesSagaId, DeleteReplyMessagePtsIncrementedEvent>
{
    public Task HandleAsync(
        IDomainEvent<ClearHistorySaga, ClearHistorySagaId, ClearSingleUserHistoryCompletedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        ptsHelper.IncrementPtsAsync(domainEvent.AggregateEvent.DeletedBoxItem.OwnerPeerId,
            domainEvent.AggregateEvent.DeletedBoxItem.Pts,
            domainEvent.AggregateEvent.DeletedBoxItem.PtsCount);
        return Task.CompletedTask;
    }

    public Task HandleAsync(
        IDomainEvent<DeleteMessagesSaga4, DeleteMessagesSaga4Id, DeleteMessagePtsIncrementedEvent4> domainEvent,
        CancellationToken cancellationToken)
    {
        ptsHelper.IncrementPtsAsync(domainEvent.AggregateEvent.UserId, domainEvent.AggregateEvent.Pts);
        return Task.CompletedTask;
    }

    public Task HandleAsync(
        IDomainEvent<EditMessageSaga, EditMessageSagaId, InboxMessageEditCompletedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        ptsHelper.IncrementPtsAsync(domainEvent.AggregateEvent.OwnerPeerId, domainEvent.AggregateEvent.Pts);
        return Task.CompletedTask;
    }

    public Task HandleAsync(
        IDomainEvent<EditMessageSaga, EditMessageSagaId, OutboxMessageEditCompletedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        ptsHelper.IncrementPtsAsync(domainEvent.AggregateEvent.OwnerPeerId, domainEvent.AggregateEvent.Pts);
        return Task.CompletedTask;
    }

    //public Task HandleAsync(IDomainEvent<MessageSaga, MessageSagaId, ReceiveInboxMessageCompletedEvent> domainEvent,
    //    CancellationToken cancellationToken)
    //{
    //    _ptsHelper.IncrementPtsAsync(domainEvent.AggregateEvent.MessageItem.OwnerPeer.PeerId,
    //        domainEvent.AggregateEvent.Pts);
    //    return Task.CompletedTask;
    //}

    //public Task HandleAsync(IDomainEvent<MessageSaga, MessageSagaId, Domain.Events.Messaging.SendOutboxMessageCompletedEvent> domainEvent,
    //    CancellationToken cancellationToken)
    //{
    //    _ptsHelper.IncrementPtsAsync(domainEvent.AggregateEvent.MessageItem.OwnerPeer.PeerId,
    //        domainEvent.AggregateEvent.Pts);
    //    return Task.CompletedTask;
    //}

    public Task HandleAsync(IDomainEvent<ReadHistorySaga, ReadHistorySagaId, ReadHistoryPtsIncrementEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        ptsHelper.IncrementPtsAsync(domainEvent.AggregateEvent.PeerId, domainEvent.AggregateEvent.Pts);
        return Task.CompletedTask;
    }

    public Task HandleAsync(
        IDomainEvent<UpdatePinnedMessageSaga, UpdatePinnedMessageSagaId, UpdatePinnedBoxPtsCompletedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        ptsHelper.IncrementPtsAsync(domainEvent.AggregateEvent.PeerId, domainEvent.AggregateEvent.Pts);
        return Task.CompletedTask;
    }

    public Task HandleAsync(IDomainEvent<SendMessageSaga, SendMessageSagaId, SendOutboxMessageCompletedEvent> domainEvent, CancellationToken cancellationToken)
    {
        ptsHelper.IncrementPtsAsync(domainEvent.AggregateEvent.MessageItem.OwnerPeer.PeerId,
            domainEvent.AggregateEvent.Pts);
        return Task.CompletedTask;
    }

    public Task HandleAsync(IDomainEvent<SendMessageSaga, SendMessageSagaId, ReceiveInboxMessageCompletedEvent> domainEvent, CancellationToken cancellationToken)
    {
        ptsHelper.IncrementPtsAsync(domainEvent.AggregateEvent.MessageItem.OwnerPeer.PeerId,
            domainEvent.AggregateEvent.Pts);
        return Task.CompletedTask;
    }
    public Task HandleAsync(IDomainEvent<DeleteChannelMessagesSaga, DeleteChannelMessagesSagaId, DeleteChannelMessagePtsIncrementedEvent> domainEvent, CancellationToken cancellationToken)
    {
        ptsHelper.IncrementPtsAsync(domainEvent.AggregateEvent.ChannelId, domainEvent.AggregateEvent.Pts);
        return Task.CompletedTask;
    }

    public Task HandleAsync(IDomainEvent<PinForwardedChannelMessageSaga, PinForwardedChannelMessageSagaId, PinChannelMessagePtsIncrementedEvent> domainEvent, CancellationToken cancellationToken)
    {
        ptsHelper.IncrementPtsAsync(domainEvent.AggregateEvent.ChannelId, domainEvent.AggregateEvent.Pts);
        return Task.CompletedTask;
    }

    public Task HandleAsync(IDomainEvent<MessageAggregate, MessageId, MessageReplyUpdatedEvent> domainEvent, CancellationToken cancellationToken)
    {
        ptsHelper.IncrementPtsAsync(domainEvent.AggregateEvent.OwnerChannelId, domainEvent.AggregateEvent.Pts);
        return Task.CompletedTask;
    }

    public Task HandleAsync(IDomainEvent<DeleteReplyMessagesSaga, DeleteReplyMessagesSagaId, DeleteReplyMessagePtsIncrementedEvent> domainEvent, CancellationToken cancellationToken)
    {
        ptsHelper.IncrementPtsAsync(domainEvent.AggregateEvent.ChannelId, domainEvent.AggregateEvent.Pts);
        return Task.CompletedTask;
    }

    public Task HandleAsync(IDomainEvent<ReadHistorySaga, ReadHistorySagaId, ReadHistoryPtsIncrementedSagaEvent> domainEvent, CancellationToken cancellationToken)
    {
        ptsHelper.IncrementPtsAsync(domainEvent.AggregateEvent.UserId, domainEvent.AggregateEvent.Pts);
        return Task.CompletedTask;
    }
}