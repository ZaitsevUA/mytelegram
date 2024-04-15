namespace MyTelegram.Domain.Sagas;

public class UpdateContactProfilePhotoSaga : MyInMemoryAggregateSaga<UpdateContactProfilePhotoSaga,
        UpdateContactProfilePhotoSagaId, UpdateContactProfilePhotoSagaLocator>,
    ISagaIsStartedBy<ContactAggregate, ContactId, ContactProfilePhotoChangedEvent>
{
    private readonly IIdGenerator _idGenerator;

    public UpdateContactProfilePhotoSaga(UpdateContactProfilePhotoSagaId id, IEventStore eventStore, IIdGenerator idGenerator) : base(id, eventStore)
    {
        _idGenerator = idGenerator;
    }

    public async Task HandleAsync(IDomainEvent<ContactAggregate, ContactId, ContactProfilePhotoChangedEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
    {
        if (domainEvent.AggregateEvent.Suggest)
        {
            var ownerPeerId = domainEvent.AggregateEvent.SelfUserId;

            var outMessageId = await _idGenerator.NextIdAsync(IdType.MessageId, ownerPeerId, cancellationToken: cancellationToken);
            var randomId = Random.Shared.NextInt64();
            var aggregateId = MessageId.Create(ownerPeerId, outMessageId);
            var ownerPeer = new Peer(PeerType.User, ownerPeerId);
            var senderPeer = new Peer(PeerType.User, ownerPeerId);
            var toPeer = new Peer(PeerType.User, domainEvent.AggregateEvent.TargetUserId);

            var command = new CreateOutboxMessageCommand(
                aggregateId,
                domainEvent.AggregateEvent.RequestInfo with { RequestId = Guid.NewGuid() },
                new MessageItem(ownerPeer, toPeer, senderPeer,
                    senderPeer.PeerId,
                    outMessageId,
                    string.Empty,
                    DateTime.UtcNow.ToTimestamp(),
                    randomId,
                    true,
                    SendMessageType.MessageService,
                    MessageType.Text,
                    MessageSubType.None,
                    MessageActionData: domainEvent.AggregateEvent.MessageActionData
                    )
                );
            Publish(command);
        }

        await CompleteAsync(cancellationToken);
    }
}