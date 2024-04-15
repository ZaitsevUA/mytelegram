namespace MyTelegram.Domain.Commands.Dialog;

public class UpdateReadChannelOutboxCommand : Command<DialogAggregate, DialogId, IExecutionResult>, IHasRequestInfo
{
    //public long MessageSenderUserId { get; }
    //public long ChannelId { get; }
    public int MaxId { get; }

    public UpdateReadChannelOutboxCommand(DialogId aggregateId, RequestInfo requestInfo,  /*long messageSenderUserId, long channelId, */int maxId) : base(aggregateId)
    {
        //MessageSenderUserId = messageSenderUserId;
        //ChannelId = channelId;
        MaxId = maxId;
        RequestInfo = requestInfo;
    }

    public RequestInfo RequestInfo { get; }
}