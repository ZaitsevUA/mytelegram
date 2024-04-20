namespace MyTelegram.Domain.Events.Chat;

public class ChatDeletedEvent(RequestInfo requestInfo, long chatId, string title)
    : RequestAggregateEvent2<ChatAggregate, ChatId>(requestInfo)
{
    public long ChatId { get; } = chatId;
    public string Title { get; } = title;
}
