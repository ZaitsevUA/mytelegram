namespace MyTelegram.Domain.Events.Chat;

public class ClearGroupChatHistoryStartedEvent(
    RequestInfo requestInfo,
    long chatId,
    IReadOnlyList<ChatMember> memberUidList)
    : RequestAggregateEvent2<ChatAggregate, ChatId>(requestInfo)
{
    public long ChatId { get; } = chatId;
    public IReadOnlyList<ChatMember> MemberUidList { get; } = memberUidList;
}
