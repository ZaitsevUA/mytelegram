namespace MyTelegram.Domain.Commands.Messaging;

public class PinChannelMessageCommand : Command<MessageAggregate, MessageId, IExecutionResult>,IHasRequestInfo
{
    public PinChannelMessageCommand(MessageId aggregateId, RequestInfo requestInfo) : base(aggregateId)
    {
        RequestInfo = requestInfo;
    }

    public RequestInfo RequestInfo { get; }
}