namespace MyTelegram.Domain.Commands.Messaging;

public class UpdateInboxMessagePinnedCommand(
    MessageId aggregateId,
    RequestInfo requestInfo,
    bool pinned,
    bool pmOneSize,
    bool silent,
    int date)
    : RequestCommand2<MessageAggregate, MessageId, IExecutionResult>(aggregateId, requestInfo)
{
    public bool Pinned { get; } = pinned;
    public bool PmOneSize { get; } = pmOneSize;
    public bool Silent { get; } = silent;
    public int Date { get; } = date;
}