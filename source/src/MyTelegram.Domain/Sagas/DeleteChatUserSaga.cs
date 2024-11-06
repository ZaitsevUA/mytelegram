namespace MyTelegram.Domain.Sagas;

public class DeleteChatUserSaga(DeleteChatUserSagaId id, IEventStore eventStore, IIdGenerator idGenerator)
    : MyInMemoryAggregateSaga<DeleteChatUserSaga, DeleteChatUserSagaId, DeleteChatUserSagaLocator>(id, eventStore),
        ISagaIsStartedBy<ChatAggregate, ChatId, ChatMemberDeletedEvent>
{
    public async Task HandleAsync(IDomainEvent<ChatAggregate, ChatId, ChatMemberDeletedEvent> domainEvent,
        ISagaContext sagaContext,
        CancellationToken cancellationToken)
    {
        var ownerPeerId = domainEvent.AggregateEvent.RequestInfo.UserId;
        var outMessageId = await idGenerator.NextIdAsync(IdType.MessageId, ownerPeerId, cancellationToken: cancellationToken);
        //var aggregateId = MessageId.Create(ownerPeerId, outMessageId);
        var messageItem = new MessageItem(
            new Peer(PeerType.User, ownerPeerId),
            new Peer(PeerType.Chat, domainEvent.AggregateEvent.ChatId),
            new Peer(PeerType.User, ownerPeerId),
            ownerPeerId,
            outMessageId,
            string.Empty,
            DateTime.UtcNow.ToTimestamp(),
            domainEvent.AggregateEvent.RandomId,
            true,
            SendMessageType.MessageService,
            MessageType.Text,
            MessageSubType.DeleteChatUser,
            MessageActionData: domainEvent.AggregateEvent.MessageActionData,
            MessageActionType: MessageActionType.ChatDeleteUser
        );
        //var command = new CreateOutboxMessageCommand(aggregateId,
        //    domainEvent.AggregateEvent.RequestInfo with { RequestId = Guid.NewGuid() },
        //    messageItem);
        var command = new StartSendMessageCommand(TempId.New,
            domainEvent.AggregateEvent.RequestInfo with { RequestId = Guid.NewGuid() },
            [new SendMessageItem(messageItem)]);

        Publish(command);
        await CompleteAsync(cancellationToken);
    }
}