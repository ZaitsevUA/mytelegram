namespace MyTelegram.Messenger.QueryServer.DomainEventHandlers;

public class ChatAndChannelMemberStateChangedEventHandler(IEventBus eventBus) :
    ISubscribeSynchronousTo<ChannelAggregate, ChannelId, ChannelCreatedEvent>,
    ISubscribeSynchronousTo<ChatAggregate, ChatId, ChatCreatedEvent>,
    ISubscribeSynchronousTo<ChatAggregate, ChatId, ChatMemberAddedEvent>,
    ISubscribeSynchronousTo<ChatAggregate, ChatId, ChatMemberDeletedEvent>,
    ISubscribeSynchronousTo<ChannelMemberAggregate, ChannelMemberId, ChannelMemberCreatedEvent>,
    ISubscribeSynchronousTo<ChannelMemberAggregate, ChannelMemberId, ChannelMemberJoinedEvent>,
    ISubscribeSynchronousTo<ChannelMemberAggregate, ChannelMemberId, ChannelMemberBannedRightsChangedEvent>,
    ISubscribeSynchronousTo<ChannelMemberAggregate, ChannelMemberId, ChannelMemberLeftEvent>
{
    public Task HandleAsync(IDomainEvent<ChannelAggregate, ChannelId, ChannelCreatedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        return eventBus.PublishAsync(new ChannelMemberChangedEvent(domainEvent.AggregateEvent.ChannelId,
            MemberStateChangeType.Add,
            new[] { domainEvent.AggregateEvent.CreatorId }));
    }

    public Task HandleAsync(
        IDomainEvent<ChannelMemberAggregate, ChannelMemberId, ChannelMemberBannedRightsChangedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        var memberStateChangeType = MemberStateChangeType.None;
        if (domainEvent.AggregateEvent.BannedRights.ViewMessages)
        {
            memberStateChangeType = MemberStateChangeType.Remove;
        }
        else if (domainEvent.AggregateEvent.RemovedFromKicked)
        {
            memberStateChangeType = MemberStateChangeType.Add;
        }

        return eventBus.PublishAsync(new ChannelMemberChangedEvent(domainEvent.AggregateEvent.ChannelId,
            memberStateChangeType,
            new[] { domainEvent.AggregateEvent.MemberUserId }));
    }

    public Task HandleAsync(
        IDomainEvent<ChannelMemberAggregate, ChannelMemberId, ChannelMemberCreatedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        return eventBus.PublishAsync(new ChannelMemberChangedEvent(domainEvent.AggregateEvent.ChannelId,
            MemberStateChangeType.Add,
            new[] { domainEvent.AggregateEvent.UserId }));
    }

    public Task HandleAsync(IDomainEvent<ChannelMemberAggregate, ChannelMemberId, ChannelMemberJoinedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        return eventBus.PublishAsync(new ChannelMemberChangedEvent(domainEvent.AggregateEvent.ChannelId,
            MemberStateChangeType.Add,
            new[] { domainEvent.AggregateEvent.MemberUserId }));
    }

    public Task HandleAsync(IDomainEvent<ChannelMemberAggregate, ChannelMemberId, ChannelMemberLeftEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        return eventBus.PublishAsync(new ChannelMemberChangedEvent(domainEvent.AggregateEvent.ChannelId,
            MemberStateChangeType.Remove,
            new[] { domainEvent.AggregateEvent.MemberUserId }));
    }

    public Task HandleAsync(IDomainEvent<ChatAggregate, ChatId, ChatCreatedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        return eventBus.PublishAsync(new ChatMemberChangedEvent(domainEvent.AggregateEvent.ChatId,
            MemberStateChangeType.Add,
            domainEvent.AggregateEvent.MemberUidList.Select(p => p.UserId).ToList()));
    }

    public Task HandleAsync(IDomainEvent<ChatAggregate, ChatId, ChatMemberAddedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        return eventBus.PublishAsync(new ChatMemberChangedEvent(domainEvent.AggregateEvent.ChatId,
            MemberStateChangeType.Add,
            new[] { domainEvent.AggregateEvent.ChatMember.UserId }));
    }

    public Task HandleAsync(IDomainEvent<ChatAggregate, ChatId, ChatMemberDeletedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        return eventBus.PublishAsync(new ChatMemberChangedEvent(domainEvent.AggregateEvent.ChatId,
            MemberStateChangeType.Remove,
            new[] { domainEvent.AggregateEvent.UserId }));
    }
}
