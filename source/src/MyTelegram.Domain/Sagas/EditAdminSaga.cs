namespace MyTelegram.Domain.Sagas;

public class EditAdminSaga(
    EditAdminSagaId id,
    IEventStore eventStore)
    : MyInMemoryAggregateSaga<EditAdminSaga, EditAdminSagaId, EditAdminSagaLocator>(id, eventStore),
        ISagaIsStartedBy<ChannelAggregate, ChannelId, ChannelAdminRightsEditedEvent>
{
    public async Task HandleAsync(IDomainEvent<ChannelAggregate, ChannelId, ChannelAdminRightsEditedEvent> domainEvent,
        ISagaContext sagaContext,
        CancellationToken cancellationToken)
    {
        if (domainEvent.AggregateEvent.IsBot && domainEvent.AggregateEvent.IsNewAdmin)
        {
            var command = new CreateChannelMemberCommand(
                ChannelMemberId.Create(domainEvent.AggregateEvent.ChannelId, domainEvent.AggregateEvent.UserId),
                domainEvent.AggregateEvent.RequestInfo,
                domainEvent.AggregateEvent.ChannelId,
                domainEvent.AggregateEvent.UserId,
                domainEvent.AggregateEvent.PromotedBy,
                DateTime.UtcNow.ToTimestamp(),
                true,
                null
                );
            Publish(command);

            var incrementMemberCountCommand = new IncrementParticipantCountCommand(ChannelId.Create(domainEvent.AggregateEvent.ChannelId));
            Publish(incrementMemberCountCommand);
        }

        await CompleteAsync(cancellationToken);
    }
}