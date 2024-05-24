namespace MyTelegram.Domain.Events.Chat;

public class CheckChatStateCompletedEvent(
    RequestInfo requestInfo,
    string title,
    IReadOnlyList<long> memberUidList)
    : RequestAggregateEvent2<ChatAggregate, ChatId>(requestInfo)
{
    public string Title { get; } = title;
    public IReadOnlyList<long> MemberUidList { get; } = memberUidList;
}
