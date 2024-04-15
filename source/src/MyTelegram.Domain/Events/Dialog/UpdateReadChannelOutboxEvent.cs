namespace MyTelegram.Domain.Events.Dialog;

public class UpdateReadChannelOutboxEvent : AggregateEvent<DialogAggregate, DialogId>,IHasRequestInfo
{
    public long MessageSenderUserId { get; }
    public long ChannelId { get; }
    public int MaxId { get; }

    public UpdateReadChannelOutboxEvent(RequestInfo requestInfo, long messageSenderUserId, long channelId, int maxId )
    {
        MessageSenderUserId = messageSenderUserId;
        ChannelId = channelId;
        MaxId = maxId;
        RequestInfo = requestInfo;
    }

    public RequestInfo RequestInfo { get; }
}