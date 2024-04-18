namespace MyTelegram.Domain.Sagas.Events;

public class ReadHistoryStartedEvent(
    RequestInfo requestInfo,
    long readerUid,
    int readerMessageId,
    Peer toPeer,
    string sourceCommandId)
    : RequestAggregateEvent2<ReadHistorySaga, ReadHistorySagaId>(requestInfo) //,IHasCorrelationId
{
    public int ReaderMessageId { get; } = readerMessageId;
    public long ReaderUid { get; } = readerUid;
    public string SourceCommandId { get; } = sourceCommandId;
    public Peer ToPeer { get; } = toPeer;
}
