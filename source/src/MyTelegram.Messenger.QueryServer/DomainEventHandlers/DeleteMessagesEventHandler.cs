using MyTelegram.Messenger.Services.Caching;
using MyTelegram.Messenger.TLObjectConverters.Interfaces;
using MyTelegram.Services.TLObjectConverters;

namespace MyTelegram.Messenger.QueryServer.DomainEventHandlers;
public class DeleteMessagesEventHandler(IObjectMessageSender objectMessageSender, ICommandBus commandBus,
        IIdGenerator idGenerator, IAckCacheService ackCacheService, IResponseCacheAppService responseCacheAppService,
        ILayeredService<IUpdatesConverter> layeredUpdatesService)
    : DomainEventHandlerBase(objectMessageSender, commandBus, idGenerator, ackCacheService, responseCacheAppService),
        ISubscribeSynchronousTo<DeleteMessagesSaga4, DeleteMessagesSaga4Id, DeleteSelfMessagesCompletedSagaEvent>,
        ISubscribeSynchronousTo<DeleteMessagesSaga4, DeleteMessagesSaga4Id, DeleteOtherParticipantMessagesCompletedSagaEvent>,
        ISubscribeSynchronousTo<DeleteMessagesSaga4, DeleteMessagesSaga4Id, DeleteSelfHistoryCompletedSagaEvent>,
        ISubscribeSynchronousTo<DeleteMessagesSaga4, DeleteMessagesSaga4Id, DeleteOtherParticipantHistoryCompletedSagaEvent>,
        ISubscribeSynchronousTo<DeleteChannelMessagesSaga, DeleteChannelMessagesSagaId, DeleteChannelMessagesCompletedEvent>,
        ISubscribeSynchronousTo<DeleteChannelMessagesSaga, DeleteChannelMessagesSagaId, DeleteChannelHistoryCompletedEvent>,
        ISubscribeSynchronousTo<DeleteReplyMessagesSaga, DeleteReplyMessagesSagaId, DeleteReplyMessagesCompletedSagaEvent>,

ISubscribeSynchronousTo<DialogAggregate, DialogId, ChannelHistoryClearedEvent>
{
    public async Task HandleAsync(IDomainEvent<DeleteMessagesSaga4, DeleteMessagesSaga4Id, DeleteSelfMessagesCompletedSagaEvent> domainEvent, CancellationToken cancellationToken)
    {
        var r = new TAffectedMessages
        {
            Pts = domainEvent.AggregateEvent.Pts,
            PtsCount = domainEvent.AggregateEvent.PtsCount
        };
        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.RequestInfo,
            r,
            domainEvent.AggregateEvent.RequestInfo.UserId, domainEvent.AggregateEvent.Pts);

        var selfOtherDeviceUpdates = layeredUpdatesService.Converter.ToDeleteMessagesUpdates(PeerType.User,
            new DeletedBoxItem(domainEvent.AggregateEvent.RequestInfo.UserId, domainEvent.AggregateEvent.Pts,
                domainEvent.AggregateEvent.PtsCount, domainEvent.AggregateEvent.MessageIds),
            DateTime.UtcNow.ToTimestamp());
        await PushUpdatesToPeerAsync(domainEvent.AggregateEvent.RequestInfo.UserId.ToUserPeer(), selfOtherDeviceUpdates,
            domainEvent.AggregateEvent.RequestInfo.AuthKeyId, pts: domainEvent.AggregateEvent.Pts);
    }

    public Task HandleAsync(IDomainEvent<DeleteMessagesSaga4, DeleteMessagesSaga4Id, DeleteOtherParticipantMessagesCompletedSagaEvent> domainEvent, CancellationToken cancellationToken)
    {
        var updates = layeredUpdatesService.Converter.ToDeleteMessagesUpdates(PeerType.User,
            new DeletedBoxItem(domainEvent.AggregateEvent.UserId, domainEvent.AggregateEvent.Pts,
                domainEvent.AggregateEvent.PtsCount, domainEvent.AggregateEvent.MessageIds),
            DateTime.UtcNow.ToTimestamp());
        var layeredUpdates = layeredUpdatesService.GetLayeredData(c => c.ToDeleteMessagesUpdates(PeerType.User,
            new DeletedBoxItem(domainEvent.AggregateEvent.UserId, domainEvent.AggregateEvent.Pts,
                domainEvent.AggregateEvent.PtsCount, domainEvent.AggregateEvent.MessageIds),
            DateTime.UtcNow.ToTimestamp()));

        return PushUpdatesToPeerAsync(domainEvent.AggregateEvent.UserId.ToUserPeer(), updates,
            pts: domainEvent.AggregateEvent.Pts, layeredData: layeredUpdates);

    }

    public async Task HandleAsync(IDomainEvent<DeleteChannelMessagesSaga, DeleteChannelMessagesSagaId, DeleteChannelHistoryCompletedEvent> domainEvent, CancellationToken cancellationToken)
    {
        var nextMaxId = domainEvent.AggregateEvent.MessageIds.Min();
        var r = new TAffectedHistory
        {
            Pts = domainEvent.AggregateEvent.Pts,
            PtsCount = domainEvent.AggregateEvent.PtsCount,
            Offset = nextMaxId
        };
        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.RequestInfo, r);
        var date = DateTime.UtcNow.ToTimestamp();
        var deletedBoxItem = new DeletedBoxItem(domainEvent.AggregateEvent.ChannelId,
            domainEvent.AggregateEvent.Pts,
            domainEvent.AggregateEvent.PtsCount,
            domainEvent.AggregateEvent.MessageIds);
        var updates = layeredUpdatesService.GetConverter(domainEvent.AggregateEvent.RequestInfo.Layer)
            .ToDeleteMessagesUpdates(PeerType.Channel,
                deletedBoxItem,
                date);
        var layeredData = layeredUpdatesService.GetLayeredData(c =>
            c.ToDeleteMessagesUpdates(PeerType.Channel,
                deletedBoxItem,
                date));
        await PushUpdatesToPeerAsync(
            new Peer(PeerType.Channel, domainEvent.AggregateEvent.ChannelId),
            updates,
            pts: domainEvent.AggregateEvent.Pts,
            layeredData: layeredData);
    }

    public async Task HandleAsync(IDomainEvent<DeleteMessagesSaga4, DeleteMessagesSaga4Id, DeleteSelfHistoryCompletedSagaEvent> domainEvent, CancellationToken cancellationToken)
    {
        IObject r = new TAffectedHistory
        {
            Pts = domainEvent.AggregateEvent.Pts,
            PtsCount = domainEvent.AggregateEvent.PtsCount,
            Offset = domainEvent.AggregateEvent.Offset
        };
        if (domainEvent.AggregateEvent.IsDeletePhoneCallHistory)
        {
            r = new TAffectedFoundMessages
            {
                Pts = domainEvent.AggregateEvent.Pts,
                PtsCount = domainEvent.AggregateEvent.PtsCount,
                Offset = domainEvent.AggregateEvent.Offset,
                Messages = new TVector<int>(domainEvent.AggregateEvent.MessageIds)
            };
        }

        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.RequestInfo, r);
        var selfOtherDeviceUpdates = layeredUpdatesService.Converter.ToDeleteMessagesUpdates(PeerType.User,
            new DeletedBoxItem(domainEvent.AggregateEvent.RequestInfo.UserId, domainEvent.AggregateEvent.Pts,
                domainEvent.AggregateEvent.PtsCount, domainEvent.AggregateEvent.MessageIds),
            DateTime.UtcNow.ToTimestamp());

        await PushUpdatesToPeerAsync(domainEvent.AggregateEvent.RequestInfo.UserId.ToUserPeer(), selfOtherDeviceUpdates,
            domainEvent.AggregateEvent.RequestInfo.AuthKeyId);
    }

    public Task HandleAsync(IDomainEvent<DeleteMessagesSaga4, DeleteMessagesSaga4Id, DeleteOtherParticipantHistoryCompletedSagaEvent> domainEvent, CancellationToken cancellationToken)
    {
        var updates = layeredUpdatesService.Converter.ToDeleteMessagesUpdates(PeerType.User,
            new DeletedBoxItem(domainEvent.AggregateEvent.UserId, domainEvent.AggregateEvent.Pts,
                domainEvent.AggregateEvent.PtsCount, domainEvent.AggregateEvent.MessageIds),
            DateTime.UtcNow.ToTimestamp());
        var layeredUpdates = layeredUpdatesService.GetLayeredData(c => c.ToDeleteMessagesUpdates(PeerType.User,
            new DeletedBoxItem(domainEvent.AggregateEvent.UserId, domainEvent.AggregateEvent.Pts,
                domainEvent.AggregateEvent.PtsCount, domainEvent.AggregateEvent.MessageIds),
            DateTime.UtcNow.ToTimestamp()));

        return PushUpdatesToPeerAsync(domainEvent.AggregateEvent.UserId.ToUserPeer(), updates,
            pts: domainEvent.AggregateEvent.Pts, layeredData: layeredUpdates);
    }

    public async Task HandleAsync(IDomainEvent<DeleteChannelMessagesSaga, DeleteChannelMessagesSagaId, DeleteChannelMessagesCompletedEvent> domainEvent, CancellationToken cancellationToken)
    {
        var channelId = domainEvent.AggregateEvent.ChannelId;
        var updates = layeredUpdatesService.GetConverter(domainEvent.AggregateEvent.RequestInfo.Layer)
            .ToDeleteMessagesUpdates(PeerType.Channel,
                new DeletedBoxItem(channelId, domainEvent.AggregateEvent.Pts,
                    domainEvent.AggregateEvent.PtsCount, domainEvent.AggregateEvent.MessageIds),
                DateTime.UtcNow.ToTimestamp());

        var globalSeqNo = await SavePushUpdatesAsync(channelId, updates, domainEvent.AggregateEvent.Pts,
            domainEvent.AggregateEvent.RequestInfo.AuthKeyId, chats: new List<long> { channelId });
        await AddRpcGlobalSeqNoForAuthKeyIdAsync(domainEvent.AggregateEvent.RequestInfo.ReqMsgId,
            domainEvent.AggregateEvent.RequestInfo.UserId, globalSeqNo);
        await UpdateSelfGlobalSeqNoAfterSendChannelMessageAsync(domainEvent.AggregateEvent.RequestInfo.UserId,
            globalSeqNo);

        var r = new TAffectedMessages
        {
            Pts = domainEvent.AggregateEvent.Pts,
            PtsCount = domainEvent.AggregateEvent.PtsCount
        };

        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.RequestInfo, r,
            domainEvent.AggregateEvent.RequestInfo.UserId,
            domainEvent.AggregateEvent.Pts,
            PeerType.Channel
        );

        await PushUpdatesToChannelMemberAsync(domainEvent.AggregateEvent.RequestInfo.UserId.ToUserPeer(),
            channelId.ToChannelPeer(),
            updates,
            domainEvent.AggregateEvent.RequestInfo.AuthKeyId,
            skipSaveUpdates: true
        );
    }

    public async Task HandleAsync(IDomainEvent<DialogAggregate, DialogId, ChannelHistoryClearedEvent> domainEvent, CancellationToken cancellationToken)
    {
        var updateChannelAvailableMessages = new TUpdateChannelAvailableMessages
        {
            ChannelId = domainEvent.AggregateEvent.ChannelId,
            AvailableMinId = domainEvent.AggregateEvent.HistoryMinId
        };
        var updates = new TUpdates
        {
            Updates = new TVector<IUpdate>(updateChannelAvailableMessages),
            Users = new(),
            Chats = new(),
            Date = DateTime.UtcNow.ToTimestamp(),
            Seq = 0
        };
        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.RequestInfo, updates);

        await PushMessageToPeerAsync(domainEvent.AggregateEvent.RequestInfo.UserId.ToUserPeer(), updates,
            domainEvent.AggregateEvent.RequestInfo.AuthKeyId);
    }

    public Task HandleAsync(IDomainEvent<DeleteReplyMessagesSaga, DeleteReplyMessagesSagaId, DeleteReplyMessagesCompletedSagaEvent> domainEvent, CancellationToken cancellationToken)
    {
        var updates = new TUpdates
        {
            Updates = new TVector<IUpdate>(new TUpdateDeleteChannelMessages
            {
                ChannelId = domainEvent.AggregateEvent.ChannelId,
                Messages = new TVector<int>(domainEvent.AggregateEvent.MessageIds),
                Pts = domainEvent.AggregateEvent.Pts,
                PtsCount = domainEvent.AggregateEvent.PtsCount
            }),
            Users = new(),
            Chats = new(),
            Date = DateTime.UtcNow.ToTimestamp(),
        };

        return PushUpdatesToPeerAsync(domainEvent.AggregateEvent.ChannelId.ToChannelPeer(), updates);
    }
}
