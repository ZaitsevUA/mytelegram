namespace MyTelegram.Domain.Commands.Channel;

public class EditChannelPhotoCommand(
    ChannelId aggregateId,
    RequestInfo requestInfo,
    long? fileId,
    string messageActionData,
    long randomId)
    : RequestCommand2<ChannelAggregate, ChannelId, IExecutionResult>(aggregateId, requestInfo) //, IHasCorrelationId
{
    //byte[] photo,
    //Photo = photo;

    public long? FileId { get; } = fileId;

    public string MessageActionData { get; } = messageActionData;

    //public byte[] Photo { get; }
    public long RandomId { get; } = randomId;
}
