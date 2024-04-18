namespace MyTelegram.Domain.Events.Chat;

public class ChatDefaultBannedRightsEditedEvent(
    RequestInfo requestInfo,
    long chatId,
    ChatBannedRights defaultBannedRights,
    int version)
    : RequestAggregateEvent2<ChatAggregate, ChatId>(requestInfo)
{
    public long ChatId { get; } = chatId;
    public ChatBannedRights DefaultBannedRights { get; } = defaultBannedRights;
    public int Version { get; } = version;
}
