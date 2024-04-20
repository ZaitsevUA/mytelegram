namespace MyTelegram.Domain.Commands.Dialog;

public class SaveDraftCommand(
    DialogId aggregateId,
    RequestInfo requestInfo,
    string message,
    bool noWebpage,
    int date,
    int? replyToMsgId,
    byte[]? entities)
    : RequestCommand2<DialogAggregate, DialogId, IExecutionResult>(aggregateId, requestInfo)
{
    public int Date { get; } = date;
    public byte[]? Entities { get; } = entities;
    public string Message { get; } = message;
    public bool NoWebpage { get; } = noWebpage;
    public int? ReplyToMsgId { get; } = replyToMsgId;

    protected override IEnumerable<byte[]> GetSourceIdComponents()
    {
        yield return RequestInfo.RequestId.ToByteArray();
    }
}
