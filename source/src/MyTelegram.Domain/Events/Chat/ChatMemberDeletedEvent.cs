namespace MyTelegram.Domain.Events.Chat;

public class ChatMemberDeletedEvent(
    RequestInfo requestInfo,
    long chatId,
    long userId,
    string messageActionData,
    long randomId)
    : RequestAggregateEvent2<ChatAggregate, ChatId>(requestInfo)
{
    //Date = date; 

    public long ChatId { get; } = chatId;

    //public int Date { get; }
    public string MessageActionData { get; } = messageActionData;
    public long RandomId { get; } = randomId;

    public long UserId { get; } = userId;
}
