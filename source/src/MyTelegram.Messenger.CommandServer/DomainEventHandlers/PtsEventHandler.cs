using MyTelegram.Domain.Aggregates.Messaging;
using MyTelegram.Domain.Events.Messaging;
using MyTelegram.Messenger.Services.Caching;

namespace MyTelegram.Messenger.CommandServer.DomainEventHandlers;

public class PtsEventHandler(IPtsHelper ptsHelper) :
    ISubscribeSynchronousTo<UpdatePinnedMessageSaga, UpdatePinnedMessageSagaId, UpdatePinnedBoxPtsCompletedSagaEvent>,
    ISubscribeSynchronousTo<SendMessageSaga, SendMessageSagaId, SendOutboxMessageCompletedSagaEvent>,
    ISubscribeSynchronousTo<SendMessageSaga, SendMessageSagaId, ReceiveInboxMessageCompletedSagaEvent>,
    ISubscribeSynchronousTo<ClearHistorySaga, ClearHistorySagaId, ClearSingleUserHistoryCompletedSagaEvent>,
    ISubscribeSynchronousTo<DeleteMessagesSaga4, DeleteMessagesSaga4Id, DeleteMessagePtsIncrementedSagaEvent>,
    ISubscribeSynchronousTo<ReadHistorySaga, ReadHistorySagaId, ReadHistoryPtsIncrementSagaEvent>,
    ISubscribeSynchronousTo<ReadHistorySaga, ReadHistorySagaId, ReadHistoryPtsIncrementedSagaEvent>,
    ISubscribeSynchronousTo<EditMessageSaga, EditMessageSagaId, OutboxMessageEditCompletedSagaEvent>,
    ISubscribeSynchronousTo<EditMessageSaga, EditMessageSagaId, InboxMessageEditCompletedSagaEvent>,
    ISubscribeSynchronousTo<DeleteChannelMessagesSaga, DeleteChannelMessagesSagaId,
        DeleteChannelMessagePtsIncrementedEvent>,
    ISubscribeSynchronousTo<PinForwardedChannelMessageSaga, PinForwardedChannelMessageSagaId,
        PinChannelMessagePtsIncrementedEvent>,
    ISubscribeSynchronousTo<MessageAggregate, MessageId, MessageReplyUpdatedEvent>,
    ISubscribeSynchronousTo<DeleteReplyMessagesSaga, DeleteReplyMessagesSagaId, DeleteReplyMessagePtsIncrementedSagaEvent>,
    ISubscribeSynchronousTo<UnpinAllMessagesSaga,UnpinAllMessagesSagaId, MessageUnpinnedSagaEvent>,
      ISubscribeSynchronousTo<UpdateMessagePinnedSaga, UpdateMessagePinnedSagaId, MessagePinnedUpdatedSagaEvent>

{
    public Task HandleAsync(
        IDomainEvent<ClearHistorySaga, ClearHistorySagaId, ClearSingleUserHistoryCompletedSagaEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        ptsHelper.IncrementPtsAsync(domainEvent.AggregateEvent.DeletedBoxItem.OwnerPeerId,
            domainEvent.AggregateEvent.DeletedBoxItem.Pts,
            domainEvent.AggregateEvent.DeletedBoxItem.PtsCount);
        return Task.CompletedTask;
    }

    public Task HandleAsync(
        IDomainEvent<DeleteMessagesSaga4, DeleteMessagesSaga4Id, DeleteMessagePtsIncrementedSagaEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        ptsHelper.IncrementPtsAsync(domainEvent.AggregateEvent.UserId, domainEvent.AggregateEvent.Pts);
        return Task.CompletedTask;
    }

    public Task HandleAsync(
        IDomainEvent<EditMessageSaga, EditMessageSagaId, InboxMessageEditCompletedSagaEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        ptsHelper.IncrementPtsAsync(domainEvent.AggregateEvent.OwnerPeerId, domainEvent.AggregateEvent.Pts);
        return Task.CompletedTask;
    }

    public Task HandleAsync(
        IDomainEvent<EditMessageSaga, EditMessageSagaId, OutboxMessageEditCompletedSagaEvent> domainEvent,
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

    public Task HandleAsync(IDomainEvent<ReadHistorySaga, ReadHistorySagaId, ReadHistoryPtsIncrementSagaEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        ptsHelper.IncrementPtsAsync(domainEvent.AggregateEvent.PeerId, domainEvent.AggregateEvent.Pts);
        return Task.CompletedTask;
    }

    public Task HandleAsync(
        IDomainEvent<UpdatePinnedMessageSaga, UpdatePinnedMessageSagaId, UpdatePinnedBoxPtsCompletedSagaEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        ptsHelper.IncrementPtsAsync(domainEvent.AggregateEvent.PeerId, domainEvent.AggregateEvent.Pts);
        return Task.CompletedTask;
    }

    public Task HandleAsync(IDomainEvent<SendMessageSaga, SendMessageSagaId, SendOutboxMessageCompletedSagaEvent> domainEvent, CancellationToken cancellationToken)
    {
        ptsHelper.IncrementPtsAsync(domainEvent.AggregateEvent.MessageItem.OwnerPeer.PeerId,
            domainEvent.AggregateEvent.Pts);
        return Task.CompletedTask;
    }

    public Task HandleAsync(IDomainEvent<SendMessageSaga, SendMessageSagaId, ReceiveInboxMessageCompletedSagaEvent> domainEvent, CancellationToken cancellationToken)
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

    public Task HandleAsync(IDomainEvent<DeleteReplyMessagesSaga, DeleteReplyMessagesSagaId, DeleteReplyMessagePtsIncrementedSagaEvent> domainEvent, CancellationToken cancellationToken)
    {
        ptsHelper.IncrementPtsAsync(domainEvent.AggregateEvent.ChannelId, domainEvent.AggregateEvent.Pts);
        return Task.CompletedTask;
    }

    public Task HandleAsync(IDomainEvent<ReadHistorySaga, ReadHistorySagaId, ReadHistoryPtsIncrementedSagaEvent> domainEvent, CancellationToken cancellationToken)
    {
        ptsHelper.IncrementPtsAsync(domainEvent.AggregateEvent.UserId, domainEvent.AggregateEvent.Pts);
        return Task.CompletedTask;
    }

    public Task HandleAsync(IDomainEvent<UnpinAllMessagesSaga, UnpinAllMessagesSagaId, MessageUnpinnedSagaEvent> domainEvent, CancellationToken cancellationToken)
    {
        ptsHelper.IncrementPtsAsync(domainEvent.AggregateEvent.OwnerPeerId, domainEvent.AggregateEvent.Pts);

        return Task.CompletedTask;
    }

    public Task HandleAsync(IDomainEvent<UpdateMessagePinnedSaga, UpdateMessagePinnedSagaId, MessagePinnedUpdatedSagaEvent> domainEvent, CancellationToken cancellationToken)
    {
        ptsHelper.IncrementPtsAsync(domainEvent.AggregateEvent.OwnerPeerId, domainEvent.AggregateEvent.Pts);

        return Task.CompletedTask;
    }
}