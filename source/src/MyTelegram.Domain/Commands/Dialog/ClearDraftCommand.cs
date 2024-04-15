namespace MyTelegram.Domain.Commands.Dialog;

public class ClearDraftCommand : RequestCommand2<DialogAggregate, DialogId, IExecutionResult>
{
    public ClearDraftCommand(DialogId aggregateId, RequestInfo requestInfo) : base(aggregateId, requestInfo)
    {
    }
}
