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
            var command = new CreateUserNameCommand(UserNameId.Create(domainEvent.AggregateEvent.UserName),
                domainEvent.AggregateEvent.UserId,
                domainEvent.AggregateEvent.UserName);
            Publish(command);
        }

        CompleteAsync(cancellationToken);
        return Task.CompletedTask;
    }
}