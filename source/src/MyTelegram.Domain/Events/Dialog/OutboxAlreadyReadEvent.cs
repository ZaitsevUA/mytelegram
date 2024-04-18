namespace MyTelegram.Domain.Events.Dialog;

public class OutboxAlreadyReadEvent(
    RequestInfo requestInfo,
    int oldMaxMessageId,
    int newMaxMessageId,
    Peer toPeer)
    : RequestAggregateEvent2<DialogAggregate, DialogId>(requestInfo)
{
    //long senderMsgId,
    //SenderMsgId = senderMsgId; 

    public int NewMaxMessageId { get; } = newMaxMessageId;
    public Peer ToPeer { get; } = toPeer;

    public int OldMaxMessageId { get; } = oldMaxMessageId;

    //public long SenderMsgId { get; } 


}