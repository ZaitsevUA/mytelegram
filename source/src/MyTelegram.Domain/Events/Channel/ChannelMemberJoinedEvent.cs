namespace MyTelegram.Domain.Events.Channel;

public class ChannelMemberJoinedEvent(
    RequestInfo requestInfo,
    long channelId,
    long memberUserId,
    int date,
    bool isRejoin)
    : RequestAggregateEvent2<ChannelMemberAggregate, ChannelMemberId>(requestInfo)
{
    public long ChannelId { get; } = channelId;
    public int Date { get; } = date;
    public bool IsRejoin { get; } = isRejoin;
    public long MemberUserId { get; } = memberUserId;
}
