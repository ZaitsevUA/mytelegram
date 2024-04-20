namespace MyTelegram.Domain.Sagas.Events;

public class UpdateOutboxPinnedCompletedEvent(
    long ownerPeerId,
    int messageId,
    Peer toPeer) : AggregateEvent<UpdatePinnedMessageSaga, UpdatePinnedMessageSagaId>
{
    public int MessageId { get; } = messageId;
    public Peer ToPeer { get; } = toPeer;
    public long OwnerPeerId { get; } = ownerPeerId;
}
