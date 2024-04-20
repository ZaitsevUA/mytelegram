namespace MyTelegram.Domain.Commands.Chat;

public class EditChatAboutCommand(
    ChatId aggregateId,
    RequestInfo requestInfo,
    long selfUserId,
    string? about)
    : RequestCommand2<ChatAggregate, ChatId, IExecutionResult>(aggregateId, requestInfo)
{
    public string? About { get; } = about;
    public long SelfUserId { get; } = selfUserId;
}
