namespace MyTelegram.Domain.Sagas.Events;

public class EditInboxMessageStartedEvent(
    long userId,
    int messageId) : AggregateEvent<EditMessageSaga, EditMessageSagaId>
{
    public int MessageId { get; } = messageId;

    public long UserId { get; } = userId;
}
