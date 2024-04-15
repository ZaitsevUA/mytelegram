namespace MyTelegram.Domain.Sagas;

public class CreateUserSaga : MyInMemoryAggregateSaga<CreateUserSaga, CreateUserSagaId, CreateUserSagaLocator>,
    ISagaIsStartedBy<UserAggregate, UserId, UserCreatedEvent>
{
    public CreateUserSaga(CreateUserSagaId id,
        IEventStore eventStore) : base(id, eventStore)
    {
    }

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