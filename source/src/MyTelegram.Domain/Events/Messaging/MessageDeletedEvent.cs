namespace MyTelegram.Domain.Events.Messaging;

public class MessageDeletedEvent : RequestAggregateEvent2<MessageAggregate, MessageId>
{
    public MessageDeletedEvent(
        RequestInfo requestInfo,
        Peer toPeer,
        long ownerPeerId,
        int messageId,
        bool isOut,
        long senderPeerId,
        int senderMessageId,
        IReadOnlyList<InboxItem>? inboxItems) : base(requestInfo)
    {
        ToPeer = toPeer;
        OwnerPeerId = ownerPeerId;
        MessageId = messageId;
        IsOut = isOut;
        SenderPeerId = senderPeerId;
        SenderMessageId = senderMessageId;
        InboxItems = inboxItems;

    }

    public IReadOnlyList<InboxItem>? InboxItems { get; }
    public bool IsOut { get; }
    public int MessageId { get; }

    public Peer ToPeer { get; }
    public long OwnerPeerId { get; }
    public int SenderMessageId { get; }
    public long SenderPeerId { get; }

}