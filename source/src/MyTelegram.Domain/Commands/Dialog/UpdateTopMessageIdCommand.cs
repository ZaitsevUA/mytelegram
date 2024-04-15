namespace MyTelegram.Domain.Commands.Dialog;

public class UpdateTopMessageIdCommand : Command<DialogAggregate, DialogId, IExecutionResult>
{
    public int NewTopMessageId { get; }

    public UpdateTopMessageIdCommand(DialogId aggregateId, int newTopMessageId) : base(aggregateId)
    {
        NewTopMessageId = newTopMessageId;
    }
}