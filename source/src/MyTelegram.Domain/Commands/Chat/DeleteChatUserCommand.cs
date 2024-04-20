namespace MyTelegram.Domain.Commands.Chat;

public class DeleteChatUserCommand(
    ChatId aggregateId,
    RequestInfo requestInfo,
    long userId,
    string messageActionData,
    long randomId)
    : RequestCommand2<ChatAggregate, ChatId, IExecutionResult>(aggregateId, requestInfo) //, IHasCorrelationId
{
    //Date = date; 

    public string MessageActionData { get; } = messageActionData;
    public long RandomId { get; } = randomId;

    public long UserId { get; } = userId;

    //public int Date { get; } 
}
