namespace MyTelegram.Domain.Commands.Dialog;

public class StartClearGroupChatHistoryCommand(
    ChatId aggregateId,
    RequestInfo requestInfo) : RequestCommand2<ChatAggregate, ChatId, IExecutionResult>(aggregateId, requestInfo);
