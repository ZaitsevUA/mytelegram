namespace MyTelegram.Domain.Commands.Chat;

public class AddChatUserCommand(
    ChatId aggregateId,
    RequestInfo requestInfo,
    long userId,
    int date,
    string messageActionData,
    long randomId)
    : RequestCommand2<ChatAggregate, ChatId, IExecutionResult>(aggregateId, requestInfo) //, IHasCorrelationId
{
    public int Date { get; } = date;
    public string MessageActionData { get; } = messageActionData;
    public long RandomId { get; } = randomId;
    public long UserId { get; } = userId;
}