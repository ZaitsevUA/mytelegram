namespace MyTelegram.Domain.Sagas.Events;

public class EditOutboxMessageStartedEvent(
    RequestInfo requestInfo,
    MessageItem oldMessageItem,
    int messageId,
    string newMessage,
    int editDate,
    int inboxCount,
    byte[]? entities,
    byte[]? media)
    : RequestAggregateEvent2<EditMessageSaga, EditMessageSagaId>(requestInfo)
{
    public int EditDate { get; } = editDate;
    public byte[]? Entities { get; } = entities;
    public int InboxCount { get; } = inboxCount;
    public byte[]? Media { get; } = media;
    public string NewMessage { get; } = newMessage;
    public int MessageId { get; } = messageId;
    public MessageItem OldMessageItem { get; } = oldMessageItem;
}
