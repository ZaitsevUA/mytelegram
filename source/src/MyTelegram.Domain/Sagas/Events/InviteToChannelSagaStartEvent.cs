namespace MyTelegram.Domain.Sagas.Events;

public class InviteToChannelSagaStartEvent(
    RequestInfo requestInfo,
    long channelId,
    long inviterId,
    int date,
    int totalCount,
    IReadOnlyList<long> memberUidList,
    IReadOnlyList<long>? privacyRestrictedUserId,
    int maxMessageId,
    int channelHistoryMinId,
    long randomId,
    string messageActionData,
    bool broadcast,
    bool hasLink
    )
    : RequestAggregateEvent2<InviteToChannelSaga, InviteToChannelSagaId>(requestInfo)
{
    public int ChannelHistoryMinId { get; } = channelHistoryMinId;
    public long ChannelId { get; } = channelId;
    public int Date { get; } = date;
    public long InviterId { get; } = inviterId;
    public int MaxMessageId { get; } = maxMessageId;
    public IReadOnlyList<long> MemberUidList { get; } = memberUidList;
    public IReadOnlyList<long>? PrivacyRestrictedUserId { get; } = privacyRestrictedUserId;
    public string MessageActionData { get; } = messageActionData;
    public bool Broadcast { get; } = broadcast;
    public bool HasLink { get; } = hasLink;
    public long RandomId { get; } = randomId;
    public int TotalCount { get; } = totalCount;
}
