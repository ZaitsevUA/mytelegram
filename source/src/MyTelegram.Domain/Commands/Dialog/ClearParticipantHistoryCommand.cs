namespace MyTelegram.Domain.Commands.Dialog;

public class ClearParticipantHistoryCommand : RequestCommand2<DialogAggregate, DialogId, IExecutionResult>
{
    public ClearParticipantHistoryCommand(DialogId aggregateId,
        RequestInfo requestInfo) : base(aggregateId,requestInfo)
    {
    }

}
