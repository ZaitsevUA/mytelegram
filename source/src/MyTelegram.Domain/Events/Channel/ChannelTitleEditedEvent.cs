namespace MyTelegram.Domain.Events.Channel;

public class ChannelTitleEditedEvent(
    RequestInfo requestInfo,
    long channelId,
    string title,
    string messageActionData,
    long randomId)
    : RequestAggregateEvent2<ChannelAggregate, ChannelId>(requestInfo)
{
    public long ChannelId { get; } = channelId;
    public string MessageActionData { get; } = messageActionData;
    public long RandomId { get; } = randomId;
    public string Title { get; } = title;
}
