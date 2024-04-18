namespace MyTelegram.Domain.Commands.Dialog;

public class UpdateDialogFilterCommand(
    DialogFilterId aggregateId,
    RequestInfo requestInfo,
    long ownerUserId,
    DialogFilter filter)
    : RequestCommand2<DialogFilterAggregate, DialogFilterId, IExecutionResult>(aggregateId, requestInfo)
{
    public long OwnerUserId { get; } = ownerUserId;
    public DialogFilter Filter { get; } = filter;
}
