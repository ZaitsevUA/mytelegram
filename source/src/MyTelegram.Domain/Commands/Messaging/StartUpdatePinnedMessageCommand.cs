namespace MyTelegram.Domain.Commands.Messaging;

public class StartUpdatePinnedMessageCommand(
    MessageId aggregateId,
    RequestInfo requestInfo,
    bool pinned,
    bool pmOneSide,
    bool silent,
    int date,
    long randomId,
    string messageActionData)
    : RequestCommand2<MessageAggregate, MessageId, IExecutionResult>(aggregateId, requestInfo)
{
    public bool Pinned { get; } = pinned;
    public bool PmOneSide { get; } = pmOneSide;
    public bool Silent { get; } = silent;
    public int Date { get; } = date;
    public long RandomId { get; } = randomId;
    public string MessageActionData { get; } = messageActionData;
}
