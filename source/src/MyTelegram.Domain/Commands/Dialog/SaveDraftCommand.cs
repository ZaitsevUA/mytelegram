namespace MyTelegram.Domain.Commands.Dialog;

public class SaveDraftCommand(
    DialogId aggregateId,
    RequestInfo requestInfo,
    Draft draft)
    : RequestCommand2<DialogAggregate, DialogId, IExecutionResult>(aggregateId, requestInfo)
{
    public Draft Draft { get; } = draft;

    protected override IEnumerable<byte[]> GetSourceIdComponents()
    {
        yield return RequestInfo.RequestId.ToByteArray();
    }
}
