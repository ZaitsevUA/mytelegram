namespace MyTelegram.Domain.Sagas;

public class CreateUserSaga(
    CreateUserSagaId id,
    IEventStore eventStore)
    : MyInMemoryAggregateSaga<CreateUserSaga, CreateUserSagaId, CreateUserSagaLocator>(id, eventStore),
        ISagaIsStartedBy<UserAggregate, UserId, UserCreatedEvent>
{
    public Task HandleAsync(IDomainEvent<UserAggregate, UserId, UserCreatedEvent> domainEvent,
        ISagaContext sagaContext,
        CancellationToken cancellationToken)
    {
        if (!string.IsNullOrEmpty(domainEvent.AggregateEvent.UserName))
        {
            var requestInfo = domainEvent.AggregateEvent.RequestInfo;
            if (requestInfo.UserId == 0)
            {
                requestInfo = requestInfo with { UserId = domainEvent.AggregateEvent.UserId };
            }

            var command = new SetUserNameCommand(UserNameId.Create(domainEvent.AggregateEvent.UserName),
                requestInfo,
                domainEvent.AggregateEvent.UserId.ToUserPeer(),
                domainEvent.AggregateEvent.UserName,
                null
            );
            Publish(command);
        }

        CompleteAsync(cancellationToken);
        return Task.CompletedTask;
    }
}