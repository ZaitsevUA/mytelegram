namespace MyTelegram.Domain.Events.Pts;

public class QtsUpdatedEvent(
    long peerId,
    int newQts) : AggregateEvent<PtsAggregate, PtsId>
{
    public int NewQts { get; } = newQts;

    public long PeerId { get; } = peerId;
}
