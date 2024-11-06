namespace MyTelegram.Domain.Sagas.Events;

public class OutboxMessageEditCompletedSagaEvent(
    RequestInfo requestInfo,
    long ownerPeerId,
    long senderPeerId,
    int messageId,
    bool post,
    int? views,
    string message,
    int pts,
    int date,
    Peer toPeer,
    byte[]? entities,
    byte[]? media,
    byte[]? replyMarkup,
    MessageFwdHeader? fwdHeader,
    MessageReply? reply)
    : RequestAggregateEvent2<EditMessageSaga, EditMessageSagaId>(requestInfo)
{
    public int Date { get; } = date;
    public Peer ToPeer { get; } = toPeer;
    public byte[]? Entities { get; } = entities;
    public byte[]? Media { get; } = media;
    public byte[]? ReplyMarkup { get; } = replyMarkup;
    public MessageFwdHeader? FwdHeader { get; } = fwdHeader;
    public MessageReply? Reply { get; } = reply;
    public string Message { get; } = message;
    public int MessageId { get; } = messageId;
    public long OwnerPeerId { get; } = ownerPeerId;
    public bool Post { get; } = post;
    public int Pts { get; } = pts;

    public long SenderPeerId { get; } = senderPeerId;
    public int? Views { get; } = views;
}
