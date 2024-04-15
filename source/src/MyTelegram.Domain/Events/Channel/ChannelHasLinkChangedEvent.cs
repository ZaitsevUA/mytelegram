namespace MyTelegram.Domain.Events.Channel;

public class LinkedChannelChangedEvent : AggregateEvent<ChannelAggregate, ChannelId>, IHasRequestInfo
{
    public long ChannelId { get; }
    public long? LinkedChannelId { get; }
    public long? OldLinkedChannelId { get; }

    public LinkedChannelChangedEvent(RequestInfo requestInfo, long channelId, long? linkedChannelId, long? oldLinkedChannelId)
    {
        ChannelId = channelId;
        LinkedChannelId = linkedChannelId;
        OldLinkedChannelId = oldLinkedChannelId;
        RequestInfo = requestInfo;
    }

    public RequestInfo RequestInfo { get; }
}