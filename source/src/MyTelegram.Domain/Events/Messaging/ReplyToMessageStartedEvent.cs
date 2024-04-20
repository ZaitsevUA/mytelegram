namespace MyTelegram.Domain.Events.Messaging;

public class ReplyToMessageStartedEvent(
    RequestInfo requestInfo,
    IInputReplyTo replyTo,
    bool isOut,
    IReadOnlyList<InboxItem> inboxItems,
    Peer ownerPeer,
    Peer senderPeer,
    Peer toPeer,
    int senderMessageId,
    long? savedFromPeerId,
    int? savedFromMsgId,
    IReadOnlyCollection<Peer> recentRepliers)
    : RequestAggregateEvent2<MessageAggregate, MessageId>(requestInfo)
{
    //int replyToMsgId, 
    //ReplyToMsgId = replyToMsgId;

    //public int ReplyToMsgId { get; }
    public IInputReplyTo ReplyTo { get; } = replyTo;
    public bool IsOut { get; } = isOut;
    public IReadOnlyList<InboxItem> InboxItems { get; } = inboxItems;
    public Peer OwnerPeer { get; } = ownerPeer;
    public Peer SenderPeer { get; } = senderPeer;
    public Peer ToPeer { get; } = toPeer;
    public int SenderMessageId { get; } = senderMessageId;
    public long? SavedFromPeerId { get; } = savedFromPeerId;
    public int? SavedFromMsgId { get; } = savedFromMsgId;
    public IReadOnlyCollection<Peer> RecentRepliers { get; } = recentRepliers;
}