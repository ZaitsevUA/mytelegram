namespace MyTelegram.Domain.Sagas;

public class AddChatUserSaga(
    AddChatUserSagaId id,
    IEventStore eventStore,
    IIdGenerator idGenerator)
    : MyInMemoryAggregateSaga<AddChatUserSaga, AddChatUserSagaId, AddChatUserSagaLocator>(id, eventStore),
        ISagaIsStartedBy<ChatAggregate, ChatId, ChatMemberAddedEvent>
{
    public async Task HandleAsync(IDomainEvent<ChatAggregate, ChatId, ChatMemberAddedEvent> domainEvent,
        ISagaContext sagaContext,
        CancellationToken cancellationToken)
    {
        var ownerPeerId = domainEvent.AggregateEvent.ChatMember.InviterId;
        var ownerPeer = new Peer(PeerType.User, ownerPeerId);
        //var aggregateId = MessageId.Create(ownerPeerId, outMessageId);
        var outMessageId = await idGenerator.NextIdAsync(IdType.MessageId, ownerPeerId, cancellationToken: cancellationToken);
        //var aggregateId = MessageId.Create(ownerPeerId, outMessageId);

        var item = new MessageItem(
            ownerPeer,
            new Peer(PeerType.Chat, domainEvent.AggregateEvent.ChatId),
            ownerPeer,
            ownerPeerId,
            outMessageId,
            string.Empty,
            domainEvent.AggregateEvent.ChatMember.Date,
            domainEvent.AggregateEvent.RandomId,
            true,
            SendMessageType.MessageService,
            MessageType.Text,
            MessageSubType.AddChatUser,
            null,
            domainEvent.AggregateEvent.MessageActionData,
            MessageActionType.ChatAddUser
        );

        //var command = new CreateOutboxMessageCommand(aggregateId,
        //    domainEvent.AggregateEvent.RequestInfo ,//with { RequestId = Guid.NewGuid() },
        //    item,
        //    chatMembers: domainEvent.AggregateEvent.AllChatMembers
        //);
        var command = new StartSendMessageCommand(TempId.New,
            domainEvent.AggregateEvent.RequestInfo,
            [new SendMessageItem(item, ChatMembers: domainEvent.AggregateEvent.AllChatMembers)]
        );

        Publish(command);

        await CompleteAsync(cancellationToken);
    }
}