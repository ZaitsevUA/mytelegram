namespace MyTelegram.Domain.Events.Pts;

public class QtsIncrementedEvent : RequestAggregateEvent2<PtsAggregate, PtsId>
{
    public QtsIncrementedEvent(
        RequestInfo requestInfo,
        long peerId,
        int qts,
        string encryptedMessageBoxId) : base(requestInfo)
    {
        PeerId = peerId;
        Qts = qts;
        EncryptedMessageBoxId = encryptedMessageBoxId;

    }

    public string EncryptedMessageBoxId { get; }

    public long PeerId { get; }
    public int Qts { get; }

}
