namespace MyTelegram.Domain.Commands.Channel;

public class HideChatJoinRequestCommand(ChannelId aggregateId, RequestInfo requestInfo, long userId, bool approved)
    : RequestCommand2<ChannelAggregate, ChannelId, IExecutionResult>(aggregateId, requestInfo)
{
    public long UserId { get; } = userId;
    public bool Approved { get; } = approved;
}