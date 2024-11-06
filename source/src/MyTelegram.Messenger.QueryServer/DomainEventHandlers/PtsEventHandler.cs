using EventFlow.Aggregates.ExecutionResults;
using MyTelegram.Messenger.Services.Caching;

namespace MyTelegram.Messenger.QueryServer.DomainEventHandlers;

public class PtsEventHandler(
    IPtsHelper ptsHelper,
    IPeerHelper peerHelper,
    IIdGenerator idGenerator,
    IQueuedCommandExecutor<PtsAggregate, PtsId, IExecutionResult> ptsCommandExecutor)
    :
        ISubscribeSynchronousTo<UpdatePinnedMessageSaga, UpdatePinnedMessageSagaId, UpdatePinnedBoxPtsCompletedSagaEvent>,
        ISubscribeSynchronousTo<SendMessageSaga, SendMessageSagaId, SendOutboxMessageCompletedSagaEvent>,
        ISubscribeSynchronousTo<SendMessageSaga, SendMessageSagaId, ReceiveInboxMessageCompletedSagaEvent>,
        ISubscribeSynchronousTo<ClearHistorySaga, ClearHistorySagaId, ClearSingleUserHistoryCompletedSagaEvent>,
        ISubscribeSynchronousTo<DeleteMessagesSaga4, DeleteMessagesSaga4Id, DeleteMessagePtsIncrementedSagaEvent>,
        ISubscribeSynchronousTo<ReadHistorySaga, ReadHistorySagaId, ReadHistoryPtsIncrementSagaEvent>,
        ISubscribeSynchronousTo<ReadHistorySaga, ReadHistorySagaId, ReadHistoryPtsIncrementedSagaEvent>,
        ISubscribeSynchronousTo<EditMessageSaga, EditMessageSagaId, OutboxMessageEditCompletedSagaEvent>,
        ISubscribeSynchronousTo<EditMessageSaga, EditMessageSagaId, InboxMessageEditCompletedSagaEvent>,
        ISubscribeSynchronousTo<PtsAggregate, PtsId, PtsAckedEvent>,
        ISubscribeSynchronousTo<PtsAggregate, PtsId, QtsAckedEvent>,
        ISubscribeSynchronousTo<DeleteChannelMessagesSaga, DeleteChannelMessagesSagaId,
            DeleteChannelMessagePtsIncrementedEvent>,
        ISubscribeSynchronousTo<PinForwardedChannelMessageSaga, PinForwardedChannelMessageSagaId,
            PinChannelMessagePtsIncrementedEvent>,
        ISubscribeSynchronousTo<MessageAggregate, MessageId, MessageReplyUpdatedEvent>,
        ISubscribeSynchronousTo<DeleteReplyMessagesSaga, DeleteReplyMessagesSagaId,
            DeleteReplyMessagePtsIncrementedSagaEvent>,
        ISubscribeSynchronousTo<UnpinAllMessagesSaga, UnpinAllMessagesSagaId, MessageUnpinnedSagaEvent>,
        ISubscribeSynchronousTo<UpdateMessagePinnedSaga, UpdateMessagePinnedSagaId, MessagePinnedUpdatedSagaEvent>
{
    public async Task HandleAsync(
        IDomainEvent<ClearHistorySaga, ClearHistorySagaId, ClearSingleUserHistoryCompletedSagaEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        await ptsHelper.IncrementPtsAsync(domainEvent.AggregateEvent.DeletedBoxItem.OwnerPeerId,
            domainEvent.AggregateEvent.DeletedBoxItem.Pts,
            domainEvent.AggregateEvent.DeletedBoxItem.PtsCount);

        await IncrementTempPtsAsync(domainEvent.AggregateEvent.DeletedBoxItem.OwnerPeerId,
            domainEvent.AggregateEvent.DeletedBoxItem.Pts, domainEvent.AggregateEvent.RequestInfo.PermAuthKeyId);
    }

    public async Task HandleAsync(
        IDomainEvent<DeleteMessagesSaga4, DeleteMessagesSaga4Id, DeleteMessagePtsIncrementedSagaEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        await ptsHelper.IncrementPtsAsync(domainEvent.AggregateEvent.UserId, domainEvent.AggregateEvent.Pts);

        await IncrementTempPtsAsync(domainEvent.AggregateEvent.UserId, domainEvent.AggregateEvent.Pts);
    }

    public async Task HandleAsync(
        IDomainEvent<EditMessageSaga, EditMessageSagaId, InboxMessageEditCompletedSagaEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        await ptsHelper.IncrementPtsAsync(domainEvent.AggregateEvent.OwnerPeerId, domainEvent.AggregateEvent.Pts);

        await IncrementTempPtsAsync(domainEvent.AggregateEvent.OwnerPeerId, domainEvent.AggregateEvent.Pts);
    }

    public async Task HandleAsync(
        IDomainEvent<EditMessageSaga, EditMessageSagaId, OutboxMessageEditCompletedSagaEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        await ptsHelper.IncrementPtsAsync(domainEvent.AggregateEvent.OwnerPeerId, domainEvent.AggregateEvent.Pts);

        await IncrementTempPtsAsync(domainEvent.AggregateEvent.OwnerPeerId, domainEvent.AggregateEvent.Pts,
            domainEvent.AggregateEvent.RequestInfo.PermAuthKeyId);
    }

    public async Task HandleAsync(IDomainEvent<PtsAggregate, PtsId, PtsAckedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        if (!peerHelper.IsChannelPeer(domainEvent.AggregateEvent.PeerId))
        {
            if (await ptsHelper.UpdatePtsForAuthKeyIdAsync(domainEvent.AggregateEvent.PeerId,
                    domainEvent.AggregateEvent.PermAuthKeyId, domainEvent.AggregateEvent.Pts,
                    domainEvent.AggregateEvent.IsFromGetDifference))
            {
                await UpdatePtsForAuthKeyIdAsync(domainEvent.AggregateEvent.PeerId, domainEvent.AggregateEvent.PermAuthKeyId,
                       domainEvent.AggregateEvent.Pts, 0, domainEvent.AggregateEvent.GlobalSeqNo);
                return;
            }
        }

        await UpdatePtsForAuthKeyIdAsync(domainEvent.AggregateEvent.PeerId, domainEvent.AggregateEvent.PermAuthKeyId,
           domainEvent.AggregateEvent.Pts, 0, domainEvent.AggregateEvent.GlobalSeqNo);
    }

    public async Task HandleAsync(
        IDomainEvent<ReadHistorySaga, ReadHistorySagaId, ReadHistoryPtsIncrementSagaEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        await ptsHelper.IncrementPtsAsync(domainEvent.AggregateEvent.PeerId, domainEvent.AggregateEvent.Pts,
            newUnreadCount: -domainEvent.AggregateEvent.ReadCount);

        await IncrementTempPtsAsync(domainEvent.AggregateEvent.PeerId, domainEvent.AggregateEvent.Pts,
            domainEvent.AggregateEvent.RequestInfo.PermAuthKeyId, -domainEvent.AggregateEvent.ReadCount);

        //return Task.CompletedTask;
    }

    public async Task HandleAsync(
        IDomainEvent<SendMessageSaga, SendMessageSagaId, ReceiveInboxMessageCompletedSagaEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        await ptsHelper.IncrementPtsAsync(domainEvent.AggregateEvent.MessageItem.OwnerPeer.PeerId,
            domainEvent.AggregateEvent.Pts, newUnreadCount: 1);

        await IncrementTempPtsAsync(domainEvent.AggregateEvent.MessageItem.OwnerPeer.PeerId,
            domainEvent.AggregateEvent.Pts, changedUnreadCount: 1);
    }

    public async Task HandleAsync(
        IDomainEvent<SendMessageSaga, SendMessageSagaId, SendOutboxMessageCompletedSagaEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        await ptsHelper.IncrementPtsAsync(domainEvent.AggregateEvent.MessageItem.OwnerPeer.PeerId,
            domainEvent.AggregateEvent.Pts);

        if (domainEvent.AggregateEvent.MessageItem.ToPeer.PeerType == PeerType.Channel)
        {
            await IncrementGlobalSeqNoAsync(domainEvent.AggregateEvent.RequestInfo.UserId);
        }
        else
        {
            await IncrementTempPtsAsync(domainEvent.AggregateEvent.MessageItem.OwnerPeer.PeerId,
                domainEvent.AggregateEvent.Pts,
                domainEvent.AggregateEvent.RequestInfo.PermAuthKeyId);
        }
    }

    public async Task HandleAsync(
        IDomainEvent<UpdatePinnedMessageSaga, UpdatePinnedMessageSagaId, UpdatePinnedBoxPtsCompletedSagaEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        await ptsHelper.IncrementPtsAsync(domainEvent.AggregateEvent.PeerId, domainEvent.AggregateEvent.Pts);

        await IncrementTempPtsAsync(domainEvent.AggregateEvent.PeerId, domainEvent.AggregateEvent.Pts);
    }

    public async Task HandleAsync(IDomainEvent<DeleteChannelMessagesSaga, DeleteChannelMessagesSagaId, DeleteChannelMessagePtsIncrementedEvent> domainEvent, CancellationToken cancellationToken)
    {
        await ptsHelper.IncrementPtsAsync(domainEvent.AggregateEvent.ChannelId, domainEvent.AggregateEvent.Pts);
        await IncrementTempPtsAsync(domainEvent.AggregateEvent.ChannelId, domainEvent.AggregateEvent.Pts);
    }
    public async Task HandleAsync(IDomainEvent<PinForwardedChannelMessageSaga, PinForwardedChannelMessageSagaId, PinChannelMessagePtsIncrementedEvent> domainEvent, CancellationToken cancellationToken)
    {
        await ptsHelper.IncrementPtsAsync(domainEvent.AggregateEvent.ChannelId, domainEvent.AggregateEvent.Pts);
        await IncrementTempPtsAsync(domainEvent.AggregateEvent.ChannelId, domainEvent.AggregateEvent.Pts);
    }
    private Task IncrementTempPtsAsync(long ownerPeerId, int newPts, long permAuthKeyId = 0,
        int changedUnreadCount = 0)
    {
        var command =
            new UpdatePtsCommand(
                PtsId.Create(ownerPeerId),
                ownerPeerId,
                permAuthKeyId,
                newPts,
                0,
                changedUnreadCount);

        ptsCommandExecutor.Enqueue(command);

        return Task.CompletedTask;
    }

    private Task UpdatePtsForAuthKeyIdAsync(long ownerPeerId, long permAuthKeyId, int pts, int changedUnreadCount, long globalSeqNo)
    {
        var updatePtsForAuthKeyIdCommand =
            new UpdatePtsForAuthKeyIdCommand(PtsId.Create(ownerPeerId, permAuthKeyId), ownerPeerId, permAuthKeyId,
                pts,
                changedUnreadCount,
                globalSeqNo);

        ptsCommandExecutor.Enqueue(updatePtsForAuthKeyIdCommand);

        return Task.CompletedTask;
    }
    private Task UpdateQtsForAuthKeyIdAsync(long ownerPeerId, long permAuthKeyId, int qts, long globalSeqNo)
    {
        var command =
            new UpdateQtsForAuthKeyIdCommand(PtsId.Create(ownerPeerId, permAuthKeyId), ownerPeerId, permAuthKeyId,
                qts,
                globalSeqNo);
        ptsCommandExecutor.Enqueue(command);

        return Task.CompletedTask;
    }

    private Task IncrementGlobalSeqNoAsync(long userId)
    {
        Task.Run(async () =>
        {
            var globalSeqNo = await idGenerator.NextLongIdAsync(IdType.GlobalSeqNo);
            var command = new UpdateGlobalSeqNoCommand(PtsId.Create(userId), userId, 0, globalSeqNo);
            ptsCommandExecutor.Enqueue(command);
        });

        return Task.CompletedTask;
    }

    public async Task HandleAsync(IDomainEvent<MessageAggregate, MessageId, MessageReplyUpdatedEvent> domainEvent, CancellationToken cancellationToken)
    {
        await ptsHelper.IncrementPtsAsync(domainEvent.AggregateEvent.OwnerChannelId, domainEvent.AggregateEvent.Pts);
        await IncrementTempPtsAsync(domainEvent.AggregateEvent.OwnerChannelId, domainEvent.AggregateEvent.Pts);
    }
    public async Task HandleAsync(IDomainEvent<DeleteReplyMessagesSaga, DeleteReplyMessagesSagaId, DeleteReplyMessagePtsIncrementedSagaEvent> domainEvent, CancellationToken cancellationToken)
    {
        await ptsHelper.IncrementPtsAsync(domainEvent.AggregateEvent.ChannelId, domainEvent.AggregateEvent.Pts);
        await IncrementTempPtsAsync(domainEvent.AggregateEvent.ChannelId, domainEvent.AggregateEvent.Pts);
    }
    public async Task HandleAsync(IDomainEvent<ReadHistorySaga, ReadHistorySagaId, ReadHistoryPtsIncrementedSagaEvent> domainEvent, CancellationToken cancellationToken)
    {
        await ptsHelper.IncrementPtsAsync(domainEvent.AggregateEvent.UserId, domainEvent.AggregateEvent.Pts);
        await IncrementTempPtsAsync(domainEvent.AggregateEvent.UserId, domainEvent.AggregateEvent.Pts);
    }
    public Task HandleAsync(IDomainEvent<PtsAggregate, PtsId, QtsAckedEvent> domainEvent, CancellationToken cancellationToken)
    {
        return UpdateQtsForAuthKeyIdAsync(domainEvent.AggregateEvent.PeerId, domainEvent.AggregateEvent.PermAuthKeyId,
            domainEvent.AggregateEvent.Qts, domainEvent.AggregateEvent.GlobalSeqNo);
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