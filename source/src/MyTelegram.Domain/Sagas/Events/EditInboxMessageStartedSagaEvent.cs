namespace MyTelegram.Domain.Sagas.Events;

public class EditInboxMessageStartedSagaEvent(
    long userId,
    int messageId) : AggregateEvent<EditMessageSaga, EditMessageSagaId>
{
    public int MessageId { get; } = messageId;

    public long UserId { get; } = userId;
}
