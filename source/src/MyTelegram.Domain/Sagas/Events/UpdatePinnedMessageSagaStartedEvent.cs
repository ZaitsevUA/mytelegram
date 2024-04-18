namespace MyTelegram.Domain.Sagas.Events;

public class
    UpdatePinnedMessageSagaStartedEvent(
        RequestInfo requestInfo,
        bool needWaitForOutboxPinnedUpdated,
        bool pinned,
        bool pmOneSide,
        bool silent,
        int inboxCount,
        int replyToMsgId,
        long ownerPeerId,
        int messageId,
        long senderPeerId,
        int senderMessageId,
        Peer toPeer,
        long randomId,
        int date,
        string? messageActionData)
    : RequestAggregateEvent2<UpdatePinnedMessageSaga, UpdatePinnedMessageSagaId>(requestInfo),
        IHasCorrelationId
{
    public int Date { get; } = date;
    public int InboxCount { get; } = inboxCount;

    public string? MessageActionData { get; } = messageActionData;
    public int MessageId { get; } = messageId;
    public bool NeedWaitForOutboxPinnedUpdated { get; } = needWaitForOutboxPinnedUpdated;

    public long OwnerPeerId { get; } = ownerPeerId;
    public bool Pinned { get; } = pinned;
    public bool PmOneSide { get; } = pmOneSide;

    public long RandomId { get; } = randomId;
    public int ReplyToMsgId { get; } = replyToMsgId;

    public int SenderMessageId { get; } = senderMessageId;
    public Peer ToPeer { get; } = toPeer;
    public long SenderPeerId { get; } = senderPeerId;
    public bool Silent { get; } = silent;

    //public int MessageId { get; }
    ////public long ChannelId { get; }
    //public bool Pinned { get; }
    //public bool PmOneSide { get; }
    //public bool Silent { get; }
    //public int Date { get; }

    //public bool IsOut { get; }
    //public IReadOnlyList<InboxItem> InboxItems { get; }
    //public long SenderPeerId { get; }
    //public int SenderMessageId { get; }
    //public long ToPeerId { get; } 
}
