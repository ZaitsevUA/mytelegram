namespace MyTelegram.Domain.Commands.Channel;

public class SetDiscussionGroupCommand : RequestCommand2<ChannelAggregate, ChannelId, IExecutionResult>
{
    public SetDiscussionGroupCommand(ChannelId aggregateId,
        RequestInfo requestInfo,
        long broadcastChannelId,
        long? groupChannelId) : base(aggregateId, requestInfo)
    {
        BroadcastChannelId = broadcastChannelId;
        GroupChannelId = groupChannelId;
    }

    public long BroadcastChannelId { get; }
    public long? GroupChannelId { get; }
}
