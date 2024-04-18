namespace MyTelegram.Domain.Events.Chat;

public class DeleteChatMessagesStartedEvent(
    RequestInfo requestInfo,
    List<int> messageIds,
    bool revoke,
    long chatCreatorUserId,
    int chatMemberCount,
    bool isClearHistory)
    : RequestAggregateEvent2<ChatAggregate, ChatId>(requestInfo)
{
    public List<int> MessageIds { get; } = messageIds;
    public bool Revoke { get; } = revoke;
    public long ChatCreatorUserId { get; } = chatCreatorUserId;
    public int ChatMemberCount { get; } = chatMemberCount;
    public bool IsClearHistory { get; } = isClearHistory;
}
