namespace MyTelegram.Domain.Commands.Messaging;

public class ReadInboxHistoryCommand : Command<MessageAggregate, MessageId, IExecutionResult>, IHasRequestInfo
{
    public long ReaderUserId { get; }
    public int Date { get; }
    public ReadInboxHistoryCommand(MessageId aggregateId,
        RequestInfo requestInfo, long readerUserId,
        int date) : base(aggregateId)
    {
        RequestInfo = requestInfo;
        ReaderUserId = readerUserId;
        Date = date;
    }

    public RequestInfo RequestInfo { get; }
}