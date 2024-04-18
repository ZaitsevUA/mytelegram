namespace MyTelegram.Domain.Commands.Chat;

public class ReadLatestNoneBotOutboxMessageCommand(
    ChatId aggregateId,
    RequestInfo requestInfo,
    string sourceCommandId)
    : RequestCommand2<ChatAggregate, ChatId, IExecutionResult>(aggregateId, requestInfo)
{
    public string SourceCommandId { get; } = sourceCommandId;
}
