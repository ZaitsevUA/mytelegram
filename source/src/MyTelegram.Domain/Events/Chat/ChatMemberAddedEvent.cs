namespace MyTelegram.Domain.Events.Chat;

public class ChatMemberAddedEvent(
    RequestInfo requestInfo,
    long chatId,
    ChatMember chatMember,
    string messageActionData,
    long randomId,
    List<long> allChatMembers)
    : RequestAggregateEvent2<ChatAggregate, ChatId>(requestInfo)
{
    public long ChatId { get; } = chatId;
    public ChatMember ChatMember { get; } = chatMember;
    public string MessageActionData { get; } = messageActionData;
    public long RandomId { get; } = randomId;
    public List<long> AllChatMembers { get; } = allChatMembers;
}
