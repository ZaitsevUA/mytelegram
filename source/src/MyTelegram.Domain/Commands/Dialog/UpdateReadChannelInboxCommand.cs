namespace MyTelegram.Domain.Commands.Dialog;

public class UpdateReadChannelInboxCommand : Command<DialogAggregate, DialogId, IExecutionResult>, IHasRequestInfo
{
    public long MessageSenderUserId { get; }
    public int MaxId { get; }

    public UpdateReadChannelInboxCommand(DialogId aggregateId, RequestInfo requestInfo, long messageSenderUserId, int maxId) : base(aggregateId)
    {
        RequestInfo = requestInfo;
        MessageSenderUserId = messageSenderUserId;
        MaxId = maxId;
    }

    public RequestInfo RequestInfo { get; }
}