namespace MyTelegram.Domain.Commands.Messaging;

public class CreateInboxMessageCommand(
    MessageId aggregateId,
    RequestInfo requestInfo,
    MessageItem inboxMessageItem,
    int senderMessageId)
    : RequestCommand2<MessageAggregate, MessageId, IExecutionResult>(aggregateId, requestInfo)
{
    public MessageItem InboxMessageItem { get; } = inboxMessageItem;
    public int SenderMessageId { get; } = senderMessageId;
}