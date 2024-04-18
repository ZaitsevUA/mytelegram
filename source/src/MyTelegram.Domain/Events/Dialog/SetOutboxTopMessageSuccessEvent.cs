namespace MyTelegram.Domain.Events.Dialog;

public class SetOutboxTopMessageSuccessEvent(
    int messageId,
    long ownerPeerId,
    Peer toPeer,
    bool clearDraft)
    : AggregateEvent<DialogAggregate, DialogId>
{
    //RequestInfo requestInfo,
    //MessageBoxId messageBoxId,
    //int pts,
    //MessageBoxId = messageBoxId;
    //Pts = pts;

    public bool ClearDraft { get; } = clearDraft;

    public int MessageId { get; } = messageId;
    public long OwnerPeerId { get; } = ownerPeerId;
    public Peer ToPeer { get; } = toPeer;

    //public MessageBoxId MessageBoxId { get; }

}
