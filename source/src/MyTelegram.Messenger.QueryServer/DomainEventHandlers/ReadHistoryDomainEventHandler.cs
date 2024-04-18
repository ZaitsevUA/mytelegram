using MyTelegram.Messenger.Services.Caching;
using MyTelegram.Messenger.TLObjectConverters.Interfaces;
using MyTelegram.Services.Extensions;
using MyTelegram.Services.TLObjectConverters;

namespace MyTelegram.Messenger.QueryServer.DomainEventHandlers;

public class ReadHistoryDomainEventHandler(
    IObjectMessageSender objectMessageSender,
    ICommandBus commandBus,
    IIdGenerator idGenerator,
    IAckCacheService ackCacheService,
    IResponseCacheAppService responseCacheAppService,
    IPeerHelper peerHelper,
    ILayeredService<IUpdatesConverter> layeredUpdatesService)
    : DomainEventHandlerBase(objectMessageSender,
            commandBus,
            idGenerator,
            ackCacheService,
            responseCacheAppService),
        ISubscribeSynchronousTo<ReadHistorySaga, ReadHistorySagaId, ReadHistoryCompletedEvent>,
        ISubscribeSynchronousTo<ReadChannelHistorySaga, ReadChannelHistorySagaId, ReadChannelHistoryCompletedEvent>,
        ISubscribeSynchronousTo<ReadHistorySaga, ReadHistorySagaId, UpdateInboxMaxIdCompletedSagaEvent>,
        ISubscribeSynchronousTo<ReadHistorySaga, ReadHistorySagaId, UpdateOutboxMaxIdCompletedSagaEvent>
{
    //private readonly ITlUpdatesConverter _updatesConverter;

    public async Task HandleAsync(
        IDomainEvent<ReadChannelHistorySaga, ReadChannelHistorySagaId, ReadChannelHistoryCompletedEvent>
            domainEvent,
        CancellationToken cancellationToken)
    {
        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.RequestInfo,
                new TBoolTrue(),
                domainEvent.AggregateEvent.SenderPeerId);

        var updates = new TUpdateShort
        {
            Update = new TUpdateReadChannelOutbox
            {
                ChannelId = domainEvent.AggregateEvent.ChannelId,
                MaxId = domainEvent.AggregateEvent.MessageId
            },
            Date = DateTime.UtcNow.ToTimestamp()
        };

        await PushUpdatesToPeerAsync(domainEvent.AggregateEvent.SenderPeerId.ToUserPeer(), updates);
    }

    public async Task HandleAsync(
        IDomainEvent<ReadHistorySaga, ReadHistorySagaId, ReadHistoryCompletedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        var affectedMessages = new TAffectedMessages { Pts = domainEvent.AggregateEvent.ReaderPts, PtsCount = 1 };

        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.RequestInfo,
                affectedMessages,
                domainEvent.AggregateEvent.ReaderUserId)
     ;
        var peer = domainEvent.AggregateEvent.ReaderToPeer;
        var updateReadHistoryInbox = new TUpdateReadHistoryInbox
        {
            Peer = peer.ToPeer(),
            MaxId = domainEvent.AggregateEvent.ReaderMessageId,
            Pts = domainEvent.AggregateEvent.ReaderPts,
            PtsCount = 1
        };
        var selfOtherDevicesUpdates = new TUpdates
        {
            Updates = new TVector<IUpdate>(updateReadHistoryInbox),
            Users = new TVector<IUser>(),
            Chats = new TVector<IChat>(),
            Date = DateTime.UtcNow.ToTimestamp()
        };
        await PushUpdatesToPeerAsync(new Peer(PeerType.User, domainEvent.AggregateEvent.ReaderUserId),
            selfOtherDevicesUpdates,
            domainEvent.AggregateEvent.RequestInfo.AuthKeyId,
            pts: domainEvent.AggregateEvent.ReaderPts
        //ptsType: PtsType.OtherUpdates
        );

        if (!domainEvent.AggregateEvent.IsOut && !domainEvent.AggregateEvent.OutboxAlreadyRead &&
            !peerHelper.IsBotUser(domainEvent.AggregateEvent.SenderPeerId))
        {
            var readHistoryUpdates =
                layeredUpdatesService.Converter.ToReadHistoryUpdates(domainEvent.AggregateEvent);

            var toPeer = new Peer(PeerType.User, domainEvent.AggregateEvent.SenderPeerId);
            await PushUpdatesToPeerAsync(
                toPeer,
                readHistoryUpdates,
                domainEvent.AggregateEvent.RequestInfo.AuthKeyId,
                pts: domainEvent.AggregateEvent.ReaderPts
            );
        }
    }

    public async Task HandleAsync(IDomainEvent<ReadHistorySaga, ReadHistorySagaId, UpdateInboxMaxIdCompletedSagaEvent> domainEvent, CancellationToken cancellationToken)
    {
        var affectedMessages = new TAffectedMessages { Pts = domainEvent.AggregateEvent.Pts, PtsCount = 1 };

        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.RequestInfo,
                affectedMessages,
                domainEvent.AggregateEvent.RequestInfo.UserId)
            ;

        var updateReadHistoryInbox = new TUpdateReadHistoryInbox
        {
            Peer = domainEvent.AggregateEvent.RequestInfo.UserId.ToUserPeer().ToPeer(),
            MaxId = domainEvent.AggregateEvent.MaxId,
            Pts = domainEvent.AggregateEvent.Pts,
            PtsCount = 1
        };
        var selfOtherDevicesUpdates = new TUpdates
        {
            Updates = new TVector<IUpdate>(updateReadHistoryInbox),
            Users = new TVector<IUser>(),
            Chats = new TVector<IChat>(),
            Date = DateTime.UtcNow.ToTimestamp()
        };
        await PushUpdatesToPeerAsync(domainEvent.AggregateEvent.RequestInfo.UserId.ToUserPeer(),
            selfOtherDevicesUpdates,
            excludeAuthKeyId: domainEvent.AggregateEvent.RequestInfo.AuthKeyId,
            pts: domainEvent.AggregateEvent.Pts
        );
    }

    public async Task HandleAsync(IDomainEvent<ReadHistorySaga, ReadHistorySagaId, UpdateOutboxMaxIdCompletedSagaEvent> domainEvent, CancellationToken cancellationToken)
    {
        var readHistoryUpdates =
            layeredUpdatesService.Converter.ToReadHistoryUpdates(domainEvent.AggregateEvent);

        var toPeer = new Peer(PeerType.User, domainEvent.AggregateEvent.UserId);
        await PushUpdatesToPeerAsync(
            toPeer,
            readHistoryUpdates,
            pts: domainEvent.AggregateEvent.Pts
        );
    }
}
