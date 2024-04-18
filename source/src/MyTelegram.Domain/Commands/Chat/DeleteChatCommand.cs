namespace MyTelegram.Domain.Commands.Chat;

public class DeleteChatCommand(
    ChatId aggregateId,
    RequestInfo requestInfo)
    : RequestCommand2<ChatAggregate, ChatId, IExecutionResult>(aggregateId, requestInfo); //, IHasCorrelationId