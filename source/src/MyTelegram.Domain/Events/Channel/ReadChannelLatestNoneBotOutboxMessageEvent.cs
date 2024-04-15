namespace MyTelegram.Domain.Events.Channel;

public class ReadChannelLatestNoneBotOutboxMessageEvent : RequestAggregateEvent2<ChannelAggregate, ChannelId>
{
    public ReadChannelLatestNoneBotOutboxMessageEvent(
        RequestInfo requestInfo,
        long latestNoneBotSenderPeerId,
        int latestNoneBotSenderMessageId,
        string sourceCommandId) : base(requestInfo)
    {
        LatestNoneBotSenderPeerId = latestNoneBotSenderPeerId;
        LatestNoneBotSenderMessageId = latestNoneBotSenderMessageId;
        SourceCommandId = sourceCommandId;
    }

    public int LatestNoneBotSenderMessageId { get; }

    public long LatestNoneBotSenderPeerId { get; }
    public string SourceCommandId { get; }
}
