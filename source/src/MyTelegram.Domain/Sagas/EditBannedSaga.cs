namespace MyTelegram.Domain.Sagas;

public class EditBannedSaga : MyInMemoryAggregateSaga<EditBannedSaga, EditBannedSagaId, EditBannedSagaLocator>,
    ISagaIsStartedBy<ChannelMemberAggregate, ChannelMemberId, ChannelMemberBannedRightsChangedEvent>
{
    public EditBannedSaga(EditBannedSagaId id,
        IEventStore eventStore) : base(id, eventStore)
    {
    }

    public Task HandleAsync(IDomainEvent<ChannelMemberAggregate, ChannelMemberId, ChannelMemberBannedRightsChangedEvent> domainEvent,
        ISagaContext sagaContext,
        CancellationToken cancellationToken)
    {
        // view message rights is banned,should remove from bots
        if (domainEvent.AggregateEvent.BannedRights.ViewMessages)
        {
            //if (domainEvent.AggregateEvent.MemberUserId > MyTelegramServerDomainConsts.BotUserInitId)
            //{
            //    var command = new RemoveBotCommand(ChannelId.Create(domainEvent.AggregateEvent.ChannelId),
            //        domainEvent.AggregateEvent.MemberUserId);
            //    Publish(command);
            //}
        }

        return Task.CompletedTask;
    }
}
