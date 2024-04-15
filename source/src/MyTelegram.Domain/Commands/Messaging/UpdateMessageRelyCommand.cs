namespace MyTelegram.Domain.Commands.Messaging;

public class UpdateMessageRelyCommand : Command<MessageAggregate, MessageId, IExecutionResult>
{
    public int Pts { get; }

    public UpdateMessageRelyCommand(MessageId aggregateId,int pts) : base(aggregateId)
    {
        Pts = pts;
    }
}