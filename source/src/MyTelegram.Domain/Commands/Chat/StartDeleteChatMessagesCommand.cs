namespace MyTelegram.Domain.Commands.Chat;

public class StartDeleteChatMessagesCommand(
    ChatId aggregateId,
    RequestInfo requestInfo,
    List<int> messageIds,
    bool revoke,
    bool isClearHistory,
    Guid correlationId)
    : RequestCommand2<ChatAggregate, ChatId, IExecutionResult>(aggregateId, requestInfo)
{
    public List<int> MessageIds { get; } = messageIds;
    public bool Revoke { get; } = revoke;
    public bool IsClearHistory { get; } = isClearHistory;
    public Guid CorrelationId { get; } = correlationId;
}