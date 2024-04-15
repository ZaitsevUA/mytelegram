namespace MyTelegram.Domain.Events.Channel;

public class DiscussionGroupUpdatedEvent : RequestAggregateEvent2<ChannelAggregate, ChannelId>
{
    public DiscussionGroupUpdatedEvent(RequestInfo requestInfo,
        long broadcastChannelId,
        long? groupChannelId, long? oldGroupChannelId) : base(requestInfo)
    {
        BroadcastChannelId = broadcastChannelId;
        GroupChannelId = groupChannelId;
        OldGroupChannelId = oldGroupChannelId;
    }

    public long BroadcastChannelId { get; }
    public long? OldGroupChannelId { get; }
    public long? GroupChannelId { get; }
}