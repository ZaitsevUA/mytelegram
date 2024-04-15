namespace MyTelegram.Domain.Events.Dialog;

public class ClearChannelHistoryEvent : RequestAggregateEvent2<DialogAggregate, DialogId>
{
    public ClearChannelHistoryEvent(RequestInfo requestInfo,
        int channelHistoryMinId) : base(requestInfo)
    {
        ChannelHistoryMinId = channelHistoryMinId;
    }

    public int ChannelHistoryMinId { get; }
}