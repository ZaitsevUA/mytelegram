namespace MyTelegram.Domain.Sagas.Events;

public class
    ReadChannelHistoryCompletedEvent(
        RequestInfo requestInfo,
        long channelId,
        long senderPeerId,
        int messageId)
    : RequestAggregateEvent2<ReadChannelHistorySaga, ReadChannelHistorySagaId>(requestInfo)
{
    //,
    //bool needNotifySender,
    //int? topMsgId
    //NeedNotifySender = needNotifySender;
    //TopMsgId = topMsgId;

    public long ChannelId { get; } = channelId;

    public int MessageId { get; } = messageId;

    //public bool NeedNotifySender { get; }
    //public int? TopMsgId { get; }
    public long SenderPeerId { get; } = senderPeerId;
}
