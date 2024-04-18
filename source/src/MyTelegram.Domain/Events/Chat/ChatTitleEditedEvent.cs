namespace MyTelegram.Domain.Events.Chat;

public class ChatTitleEditedEvent(
    RequestInfo requestInfo,
    long chatId,
    string title,
    string messageActionData,
    long randomId)
    : RequestAggregateEvent2<ChatAggregate, ChatId>(requestInfo)
{
    public long ChatId { get; } = chatId;
    public string MessageActionData { get; } = messageActionData;
    public long RandomId { get; } = randomId;
    public string Title { get; } = title;
}

