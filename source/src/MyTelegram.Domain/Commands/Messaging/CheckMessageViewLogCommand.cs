namespace MyTelegram.Domain.Commands.Messaging;

public class CheckMessageViewLogCommand : RequestCommand2<MessageViewLogAggregate, MessageViewLogId, IExecutionResult>
{
    public CheckMessageViewLogCommand(MessageViewLogId aggregateId,
        RequestInfo requestInfo,
        int messageId) : base(aggregateId, requestInfo)
    {
        MessageId = messageId;
    }

    public int MessageId { get; }
}
