namespace MyTelegram.Domain.Events.Channel;

public class ChannelMemberLeftEvent(
    RequestInfo requestInfo,
    long channelId,
    long memberUserId,
    bool isBot
    )
    : RequestAggregateEvent2<ChannelMemberAggregate, ChannelMemberId>(requestInfo)
{
    public long ChannelId { get; } = channelId;
    public long MemberUserId { get; } = memberUserId;
    public bool IsBot { get; } = isBot;
}
