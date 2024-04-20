namespace MyTelegram.Domain.Sagas.Events;

public class ReadHistoryReadLatestNoneBotOutboxMessageEvent(long senderPeerId)
    : AggregateEvent<ReadHistorySaga, ReadHistorySagaId>
{
    public long SenderPeerId { get; } = senderPeerId;
}
