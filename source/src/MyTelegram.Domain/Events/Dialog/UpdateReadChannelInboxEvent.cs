namespace MyTelegram.Domain.Events.Dialog;

public class UpdateReadChannelInboxEvent : AggregateEvent<DialogAggregate, DialogId>,IHasRequestInfo
{
    public long MessageSenderUserId { get; }
    public long ChannelId { get; }
    public int MaxId { get; }

    public UpdateReadChannelInboxEvent(RequestInfo requestInfo, long messageSenderUserId, long channelId, int maxId)
    {
        RequestInfo = requestInfo;
        MessageSenderUserId = messageSenderUserId;
        ChannelId = channelId;
        MaxId = maxId;
    }

    public RequestInfo RequestInfo { get; }
}