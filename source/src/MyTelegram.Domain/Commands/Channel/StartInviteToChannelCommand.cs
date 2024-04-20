namespace MyTelegram.Domain.Commands.Channel;

public class StartInviteToChannelCommand(
    ChannelId aggregateId,
    RequestInfo requestInfo,
    long channelId,
    long inviterId,
    int maxMessageId,
    IReadOnlyList<long> memberUserIdList,
    IReadOnlyList<long>? privacyRestrictedUserIds,
    IReadOnlyList<long> botUserIdList,
    int date,
    long randomId,
    string messageActionData,
    ChatJoinType chatJoinType)
    : RequestCommand2<ChannelAggregate, ChannelId, IExecutionResult>(aggregateId, requestInfo)
{
    public IReadOnlyList<long> BotUserIdList { get; } = botUserIdList;
    public long ChannelId { get; } = channelId;
    public int Date { get; } = date;
    public long InviterId { get; } = inviterId;
    public int MaxMessageId { get; } = maxMessageId;
    public IReadOnlyList<long> MemberUserIdList { get; } = memberUserIdList;
    public IReadOnlyList<long>? PrivacyRestrictedUserIds { get; } = privacyRestrictedUserIds;
    public string MessageActionData { get; } = messageActionData;
    public ChatJoinType ChatJoinType { get; } = chatJoinType;
    public long RandomId { get; } = randomId;

    protected override IEnumerable<byte[]> GetSourceIdComponents()
    {
        yield return BitConverter.GetBytes(RequestInfo.ReqMsgId);
        yield return RequestInfo.RequestId.ToByteArray();
    }
}
