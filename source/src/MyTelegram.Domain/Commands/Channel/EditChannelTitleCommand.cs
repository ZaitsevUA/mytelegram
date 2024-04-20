namespace MyTelegram.Domain.Commands.Channel;

public class EditChannelTitleCommand(
    ChannelId aggregateId,
    RequestInfo requestInfo,
    string title,
    string messageActionData,
    long randomId)
    : RequestCommand2<ChannelAggregate, ChannelId, IExecutionResult>(aggregateId, requestInfo) //, IHasCorrelationId
{
    public string MessageActionData { get; } = messageActionData;
    public long RandomId { get; } = randomId;
    public string Title { get; } = title;
}