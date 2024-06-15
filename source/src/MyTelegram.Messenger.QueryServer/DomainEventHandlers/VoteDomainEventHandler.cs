using MyTelegram.Messenger.Services.Caching;
using MyTelegram.Messenger.TLObjectConverters.Interfaces;
using MyTelegram.Services.TLObjectConverters;

namespace MyTelegram.Messenger.QueryServer.DomainEventHandlers;

public class VoteDomainEventHandler(
    IObjectMessageSender objectMessageSender,
    ICommandBus commandBus,
    IIdGenerator idGenerator,
    IAckCacheService ackCacheService,
    IResponseCacheAppService responseCacheAppService,
    IQueryProcessor queryProcessor,
    ILayeredService<IPollConverter> layeredService)
    :
        DomainEventHandlerBase(objectMessageSender, commandBus, idGenerator, ackCacheService,
            responseCacheAppService),
        ISubscribeSynchronousTo<VoteSaga, VoteSagaId, VoteSagaCompletedEvent>
{
    //private readonly ITlPollConverter _pollConverter;

    public async Task HandleAsync(IDomainEvent<VoteSaga, VoteSagaId, VoteSagaCompletedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        var pollReadModel = await queryProcessor
            .ProcessAsync(new GetPollQuery(domainEvent.AggregateEvent.ToPeer.PeerId, domainEvent.AggregateEvent.PollId),
                default);
        if (pollReadModel != null)
        {
            var selfUpdates = layeredService.Converter.ToSelfPollUpdates(pollReadModel,
                domainEvent.AggregateEvent.ChosenOptions.ToList());
            await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.RequestInfo, selfUpdates)
         ;

            await PushMessageToPeerAsync(new Peer(PeerType.User, domainEvent.AggregateEvent.RequestInfo.UserId),
                selfUpdates,
                domainEvent.AggregateEvent.RequestInfo.AuthKeyId);

            // 
            var updatesForMember = layeredService.Converter.ToPollUpdates(pollReadModel, Array.Empty<string>());
            await PushMessageToPeerAsync(domainEvent.AggregateEvent.ToPeer,
                updatesForMember,
                excludeAuthKeyId: domainEvent.AggregateEvent.RequestInfo.AuthKeyId);
        }
    }
}

