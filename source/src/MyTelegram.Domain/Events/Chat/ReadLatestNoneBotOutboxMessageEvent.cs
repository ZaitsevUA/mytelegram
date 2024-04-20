namespace MyTelegram.Domain.Events.Chat;

public class ReadLatestNoneBotOutboxMessageEvent(
    RequestInfo requestInfo,
    long chatId,
    long senderPeerId,
    int senderMessageId,
    string sourceCommandId)
    : RequestAggregateEvent2<ChatAggregate, ChatId>(requestInfo)
{
    public long ChatId { get; } = chatId;
    public int SenderMessageId { get; } = senderMessageId;
    public long SenderPeerId { get; } = senderPeerId;
    public string SourceCommandId { get; } = sourceCommandId;
}