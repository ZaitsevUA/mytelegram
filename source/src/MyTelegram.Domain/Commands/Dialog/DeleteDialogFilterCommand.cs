namespace MyTelegram.Domain.Commands.Dialog;

public class DeleteDialogFilterCommand : RequestCommand2<DialogFilterAggregate, DialogFilterId, IExecutionResult>
{
    public DeleteDialogFilterCommand(DialogFilterId aggregateId,
        RequestInfo requestInfo) : base(aggregateId, requestInfo)
    {
    }
}