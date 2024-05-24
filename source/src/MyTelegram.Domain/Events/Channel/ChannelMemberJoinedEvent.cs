namespace MyTelegram.Domain.Events.Channel;

public class ChannelMemberJoinedEvent(
    RequestInfo requestInfo,
    long channelId,
    long memberUserId,
    int date,
    bool isRejoin,
    bool isBot
    )
    : RequestAggregateEvent2<ChannelMemberAggregate, ChannelMemberId>(requestInfo)
{
    public long ChannelId { get; } = channelId;
    public int Date { get; } = date;
    public bool IsRejoin { get; } = isRejoin;
    public bool IsBot { get; } = isBot;
    public long MemberUserId { get; } = memberUserId;
}
