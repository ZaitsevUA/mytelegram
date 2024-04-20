namespace MyTelegram.Domain.Sagas.Events;

public class ReadChannelHistoryStartedEvent(RequestInfo requestInfo, long channelId)
    : RequestAggregateEvent2<ReadChannelHistorySaga, ReadChannelHistorySagaId>(requestInfo)
{
    //long readerUserId,
    //ReaderUserId = readerUserId;
    //TopMsgId = topMsgId;

    public long ChannelId { get; } = channelId;
    //public int? TopMsgId { get; }
    //public long ReaderUserId { get; }
}
