namespace MyTelegram.Domain.Commands.Channel;

public class UpdateChatInviteRequestPendingCommand(ChannelId aggregateId, long requestUserId)
    : Command<ChannelAggregate, ChannelId, IExecutionResult>(aggregateId)
{
    public long RequestUserId { get; } = requestUserId;
}