namespace MyTelegram.Domain.Sagas.Events;

public class InboxMessageEditCompletedEvent(
    long ownerPeerId,
    long senderPeerId,
    int messageId,
    string message,
    int editDate,
    int pts,
    Peer toPeer,
    byte[]? entities,
    byte[]? media,
    byte[]? replyMarkup,
    MessageFwdHeader? fwdHeader)
    : AggregateEvent<EditMessageSaga, EditMessageSagaId>
{
    public int EditDate { get; } = editDate;
    public Peer ToPeer { get; } = toPeer;
    public byte[]? Entities { get; } = entities;
    public byte[]? Media { get; } = media;
    public byte[]? ReplyMarkup { get; } = replyMarkup;
    public MessageFwdHeader? FwdHeader { get; } = fwdHeader;
    public string Message { get; } = message;
    public long OwnerPeerId { get; } = ownerPeerId;
    public long SenderPeerId { get; } = senderPeerId;
    public int MessageId { get; } = messageId;
    public int Pts { get; } = pts;
}
