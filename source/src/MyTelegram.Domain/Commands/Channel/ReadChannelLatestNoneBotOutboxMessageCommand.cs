namespace MyTelegram.Domain.Commands.Channel;

public class ReadChannelLatestNoneBotOutboxMessageCommand : RequestCommand2<ChannelAggregate, ChannelId, IExecutionResult>//,
                                                                                                                  //IHasCorrelationId
{
    public ReadChannelLatestNoneBotOutboxMessageCommand(ChannelId aggregateId,
        RequestInfo requestInfo,
        string sourceCommandId) : base(aggregateId,requestInfo)
    {
        SourceCommandId = sourceCommandId;
    }

    public string SourceCommandId { get; }
}
