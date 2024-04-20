namespace MyTelegram.Domain.Sagas;

public class JoinChannelSaga(JoinChannelSagaId id)
    : AggregateSaga<JoinChannelSaga, JoinChannelSagaId, JoinChannelSagaLocator>(id),
        ISagaIsStartedBy<ChannelMemberAggregate, ChannelMemberId, ChannelMemberJoinedEvent>
{
    public Task HandleAsync(IDomainEvent<ChannelMemberAggregate, ChannelMemberId, ChannelMemberJoinedEvent> domainEvent,
        ISagaContext sagaContext,
        CancellationToken cancellationToken)
    {
        var command = new IncrementParticipantCountCommand(ChannelId.Create(domainEvent.AggregateEvent.ChannelId));
        Publish(command);
        Complete();
        return Task.CompletedTask;
    }
}
