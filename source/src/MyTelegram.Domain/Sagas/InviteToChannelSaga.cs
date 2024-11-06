namespace MyTelegram.Domain.Sagas;

public class InviteToChannelSaga :
    MyInMemoryAggregateSaga<InviteToChannelSaga, InviteToChannelSagaId, InviteToChannelSagaLocator>,
    ISagaIsStartedBy<ChannelAggregate, ChannelId, StartInviteToChannelEvent>,
    ISagaHandles<ChannelMemberAggregate, ChannelMemberId, ChannelMemberCreatedEvent>,
    IApply<InviteToChannelCompletedSagaEvent>
{
    private readonly IIdGenerator _idGenerator;
    private readonly InviteToChannelSagaState _state = new();

    public InviteToChannelSaga(InviteToChannelSagaId id, IEventStore eventStore, IIdGenerator idGenerator) : base(id, eventStore)
    {
        _idGenerator = idGenerator;
        Register(_state);
    }

    public void Apply(InviteToChannelCompletedSagaEvent aggregateEvent)
    {
        CompleteAsync();
    }

    public async Task HandleAsync(
        IDomainEvent<ChannelMemberAggregate, ChannelMemberId, ChannelMemberCreatedEvent> domainEvent,
        ISagaContext sagaContext,
        CancellationToken cancellationToken)
    {
        Emit(new InviteToChannelSagaMemberCreatedSagaEvent());
        var command = new IncrementParticipantCountCommand(ChannelId.Create(domainEvent.AggregateEvent.ChannelId));
        Publish(command);

        if (!domainEvent.AggregateEvent.IsRejoin)
        {
            var toPeer = new Peer(PeerType.Channel, domainEvent.AggregateEvent.ChannelId);
            var createDialogCommand = new CreateDialogCommand(
                DialogId.Create(domainEvent.AggregateEvent.UserId, toPeer),
                _state.RequestInfo,
                domainEvent.AggregateEvent.UserId,
                toPeer,
                _state.ChannelHistoryMinId,
                _state.MaxMessageId
            );
            Publish(createDialogCommand);
        }

        await HandleInviteToChannelCompletedAsync();
    }

    public Task HandleAsync(IDomainEvent<ChannelAggregate, ChannelId, StartInviteToChannelEvent> domainEvent,
        ISagaContext sagaContext,
        CancellationToken cancellationToken)
    {
        Emit(new InviteToChannelSagaStartSagaEvent(
            domainEvent.AggregateEvent.RequestInfo,
            domainEvent.AggregateEvent.ChannelId,
            domainEvent.AggregateEvent.InviterId,
            domainEvent.AggregateEvent.Date,
            domainEvent.AggregateEvent.MemberUidList.Count,
            domainEvent.AggregateEvent.MemberUidList,
            domainEvent.AggregateEvent.PrivacyRestrictedUserId,
            domainEvent.AggregateEvent.MaxMessageId,
            domainEvent.AggregateEvent.ChannelHistoryMinId,
            domainEvent.AggregateEvent.RandomId,
            domainEvent.AggregateEvent.MessageActionData,
            domainEvent.AggregateEvent.Broadcast,
            domainEvent.AggregateEvent.HasLink
        ));
        foreach (var userId in domainEvent.AggregateEvent.MemberUidList)
        {
            var isBot = domainEvent.AggregateEvent.BotUidList.Contains(userId);
            var command = new CreateChannelMemberCommand(
                ChannelMemberId.Create(domainEvent.AggregateEvent.ChannelId, userId),
                domainEvent.AggregateEvent.RequestInfo,
                domainEvent.AggregateEvent.ChannelId,
                userId,
                domainEvent.AggregateEvent.InviterId,
                domainEvent.AggregateEvent.Date,
                isBot,
                null,
                domainEvent.AggregateEvent.Broadcast
            );
            Publish(command);
        }

        return Task.CompletedTask;
    }

    private async Task HandleInviteToChannelCompletedAsync()
    {
        if (_state.Completed)
        {
            // send service message to member after invited to super group
            if (_state is { Broadcast: false, HasLink: false })
            {
                var ownerPeerId = _state.ChannelId;
                var outMessageId = await _idGenerator.NextIdAsync(IdType.MessageId, ownerPeerId);
                //var aggregateId = MessageId.Create(ownerPeerId, outMessageId);
                var ownerPeer = new Peer(PeerType.Channel, ownerPeerId);
                var senderPeer = new Peer(PeerType.User, _state.InviterId);
                var messageItem = new MessageItem(
                    ownerPeer,
                    ownerPeer,
                    senderPeer,
                    _state.InviterId,
                    outMessageId,
                    string.Empty,
                    DateTime.UtcNow.ToTimestamp(),
                    _state.RandomId,
                    true,
                    SendMessageType.MessageService,
                    MessageType.Text,
                    MessageSubType.InviteToChannel,
                    null,
                    _state.MessageActionData,
                    MessageActionType.ChatAddUser
                );
                var command = new StartSendMessageCommand(TempId.New, _state.RequestInfo with { RequestId = Guid.NewGuid() },
                    [new SendMessageItem(messageItem)]);

                Publish(command);
            }

            Emit(new InviteToChannelCompletedSagaEvent(_state.RequestInfo,
                _state.ChannelId,
                _state.InviterId,
                _state.Broadcast,
                _state.MemberUidList,
                _state.PrivacyRestrictedUserId,
                _state.HasLink
                ));
        }
    }
}
