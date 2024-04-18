namespace MyTelegram.Domain.Commands.Channel;

public class JoinChannelCommand(
    ChannelMemberId aggregateId,
    RequestInfo requestInfo,
    long selfUserId,
    long channelId)
    : RequestCommand2<ChannelMemberAggregate, ChannelMemberId, IExecutionResult>(aggregateId, requestInfo) //,
//IHasCorrelationId
{
    public long ChannelId { get; } = channelId;
    public long SelfUserId { get; } = selfUserId;
}
