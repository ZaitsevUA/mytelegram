namespace MyTelegram.Domain.Commands.Chat;

public class EditChatTitleCommand(
    ChatId aggregateId,
    RequestInfo requestInfo,
    string title,
    string messageActionData,
    long randomId)
    : RequestCommand2<ChatAggregate, ChatId, IExecutionResult>(aggregateId, requestInfo) //, IHasCorrelationId
{
    public string MessageActionData { get; } = messageActionData;
    public long RandomId { get; } = randomId;
    public string Title { get; } = title;
}
