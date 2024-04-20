namespace MyTelegram.Domain.Sagas.Events;

public class ReceiveInboxMessageCompletedEvent(MessageItem messageItem, int pts, string? chatTitle)
    : AggregateEvent<SendMessageSaga, SendMessageSagaId>
{
    public MessageItem MessageItem { get; } = messageItem;
    public int Pts { get; } = pts;
    public string? ChatTitle { get; } = chatTitle;
}