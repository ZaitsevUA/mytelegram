namespace MyTelegram.Messenger.CommandServer.DomainEventHandlers;
public class AllDomainEventsHandler(
    IEventBus eventBus,
    IDomainEventMessageFactory domainEventMessageFactory,
    ILogger<AllDomainEventsHandler> logger)
    : ISubscribeSynchronousToAll
{
    public async Task HandleAsync(IReadOnlyCollection<IDomainEvent> domainEvents, CancellationToken cancellationToken)
    {
        foreach (var domainEvent in domainEvents)
        {
            var aggregateEvent = domainEvent.GetAggregateEvent();
            if (aggregateEvent is IHasRequestInfo requestInfo)
            {
                var totalMilliseconds = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - requestInfo.RequestInfo.Date;
                if (totalMilliseconds > 500)
                {
                    logger.LogDebug("Process domain event '{DomainEvent}' is too slow,time={Timespan}ms,reqMsgId={ReqMsgId}",
                        domainEvent.GetAggregateEvent().GetType().Name,
                        totalMilliseconds,
                        requestInfo.RequestInfo.ReqMsgId);
                }
            }
            var message = domainEventMessageFactory.CreateDomainEventMessage(domainEvent);
            await eventBus.PublishAsync(message);
        }
    }
}