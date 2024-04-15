namespace MyTelegram.Domain.Commands.Messaging;

public class DeleteChannelMessageCommand : Command<MessageAggregate, MessageId, IExecutionResult>, IHasRequestInfo
{
    public DeleteChannelMessageCommand(MessageId aggregateId, RequestInfo requestInfo) : base(aggregateId)
    {
        RequestInfo = requestInfo;
    }

    public RequestInfo RequestInfo { get; }
}