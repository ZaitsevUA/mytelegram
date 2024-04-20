namespace MyTelegram.Domain.Events.Chat;

public class ChatCreatedEvent(
    RequestInfo requestInfo,
    long chatId,
    long creatorUid,
    string title,
    IReadOnlyList<ChatMember> memberUidList,
    int date,
    long randomId,
    string messageActionData,
    int? ttlPeriod)
    : RequestAggregateEvent2<ChatAggregate, ChatId>(requestInfo)
{
    public long ChatId { get; } = chatId;
    public long CreatorUid { get; } = creatorUid;
    public int Date { get; } = date;
    public IReadOnlyList<ChatMember> MemberUidList { get; } = memberUidList;
    public string MessageActionData { get; } = messageActionData;
    public long RandomId { get; } = randomId;
    public string Title { get; } = title;
    public int? TtlPeriod { get; } = ttlPeriod;
}
