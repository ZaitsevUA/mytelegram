namespace MyTelegram.Domain.Sagas;

public class
    EditChatTitleSaga(EditChatTitleSagaId id, IEventStore eventStore, IIdGenerator idGenerator)
    : MyInMemoryAggregateSaga<EditChatTitleSaga, EditChatTitleSagaId, EditChatTitleSagaLocator>(id, eventStore),
        ISagaIsStartedBy<ChatAggregate, ChatId, ChatTitleEditedEvent>
{
    public async Task HandleAsync(IDomainEvent<ChatAggregate, ChatId, ChatTitleEditedEvent> domainEvent,
        ISagaContext sagaContext,
        CancellationToken cancellationToken)
    {
        var ownerPeerId = domainEvent.AggregateEvent.RequestInfo.UserId;
        var toPeerId = domainEvent.AggregateEvent.ChatId;
        var outMessageId = await idGenerator.NextIdAsync(IdType.MessageId, ownerPeerId, cancellationToken: cancellationToken);
        var aggregateId = MessageId.Create(ownerPeerId, outMessageId);
        var ownerPeer = new Peer(PeerType.User, ownerPeerId);
        var toPeer = new Peer(PeerType.Chat, toPeerId);
        var senderPeer = new Peer(PeerType.User, ownerPeerId);
        var messageItem = new MessageItem(
            ownerPeer,
            toPeer,
            senderPeer,
            ownerPeerId,
            outMessageId,
            string.Empty,
            DateTime.UtcNow.ToTimestamp(),
            domainEvent.AggregateEvent.RandomId,
            true,
            SendMessageType.MessageService,
            MessageType.Text,
            MessageSubType.Normal,
            null,
            domainEvent.AggregateEvent.MessageActionData,
            MessageActionType.ChatEditTitle
        );

        var command = new CreateOutboxMessageCommand(
            aggregateId,
            domainEvent.AggregateEvent.RequestInfo,
            messageItem
        );
        Publish(command);
        await CompleteAsync(cancellationToken);
    }
}
