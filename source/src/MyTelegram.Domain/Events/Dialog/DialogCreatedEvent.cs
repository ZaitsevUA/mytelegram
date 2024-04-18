namespace MyTelegram.Domain.Events.Dialog;

public class DialogCreatedEvent(
    long ownerId,
    Peer toPeer,
    int channelHistoryMinId,
    int topMessageId,
    DateTime creationTime)
    : AggregateEvent<DialogAggregate, DialogId>
{
    //string topMessageBoxId,
    //TopMessageBoxId = topMessageBoxId;

    public int ChannelHistoryMinId { get; } = channelHistoryMinId;

    //public string TopMessageBoxId { get; }
    public DateTime CreationTime { get; } = creationTime;

    public long OwnerId { get; } = ownerId;
    public Peer ToPeer { get; } = toPeer;
    public int TopMessageId { get; } = topMessageId;
}