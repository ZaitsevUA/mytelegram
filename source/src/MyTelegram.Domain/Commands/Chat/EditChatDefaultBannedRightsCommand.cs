namespace MyTelegram.Domain.Commands.Chat;

public class EditChatDefaultBannedRightsCommand(
    ChatId aggregateId,
    RequestInfo requestInfo,
    ChatBannedRights chatBannedRights,
    long selfUserId)
    : RequestCommand2<ChatAggregate, ChatId, IExecutionResult>(aggregateId, requestInfo)
{
    public ChatBannedRights ChatBannedRights { get; } = chatBannedRights;
    public long SelfUserId { get; } = selfUserId;
}
