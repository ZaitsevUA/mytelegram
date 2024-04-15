namespace MyTelegram.Domain.Events.Pts;

public class PtsCreatedEvent : RequestAggregateEvent2<PtsAggregate, PtsId>
{
    public PtsCreatedEvent(
        RequestInfo requestInfo,
        long peerId,
        int pts,
        int qts,
        int unreadCount,
        int date) : base(requestInfo)
    {
        PeerId = peerId;
        Pts = pts;
        Qts = qts;
        UnreadCount = unreadCount;
        Date = date;

    }

    public int Date { get; }

    public long PeerId { get; }
    public int Pts { get; }
    public int Qts { get; }
    public int UnreadCount { get; }

}
