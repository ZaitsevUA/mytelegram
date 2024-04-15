//namespace MyTelegram.Domain.Sagas;

//public class DeleteUserSaga : MyInMemoryAggregateSaga<DeleteUserSaga, DeleteUserSagaId, DeleteUserSagaLocator>,
//    ISagaIsStartedBy<UserAggregate, UserId, BotUserDeletedEvent>
//{
//    public DeleteUserSaga(DeleteUserSagaId id,
//        IEventStore eventStore) : base(id, eventStore)
//    {
//    }

//    public Task HandleAsync(IDomainEvent<UserAggregate, UserId, BotUserDeletedEvent> domainEvent,
//        ISagaContext sagaContext,
//        CancellationToken cancellationToken)
//    {
//        if (!string.IsNullOrEmpty(domainEvent.AggregateEvent.UserName))
//        {
//            var command = new DeleteUserNameCommand(UserNameId.Create(domainEvent.AggregateEvent.UserName));
//            Publish(command);
//        }

//        CompleteAsync(cancellationToken);
//        return Task.CompletedTask;
//    }
//}
