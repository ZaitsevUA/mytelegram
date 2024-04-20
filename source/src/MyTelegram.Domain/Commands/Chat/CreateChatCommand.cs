namespace MyTelegram.Domain.Commands.Chat;

public class CreateChatCommand(
    ChatId aggregateId,
    RequestInfo requestInfo,
    long chatId,
    long creatorUid,
    string title,
    IReadOnlyList<long> memberUidList,
    int date,
    long randomId,
    string messageActionData,
    int? ttlPeriod = null)
    : RequestCommand2<ChatAggregate, ChatId, IExecutionResult>(aggregateId, requestInfo) //, IHasCorrelationId
{
    public long ChatId { get; } = chatId;
    public long CreatorUid { get; } = creatorUid;
    public int Date { get; } = date;
    public IReadOnlyList<long> MemberUidList { get; } = memberUidList;
    public string MessageActionData { get; } = messageActionData;
    public int? TtlPeriod { get; } = ttlPeriod;
    public long RandomId { get; } = randomId;
    public string Title { get; } = title;
}
