namespace MyTelegram.Domain.Commands.Messaging;

public class EditOutboxMessageCommand(
    MessageId aggregateId,
    RequestInfo requestInfo,
    int messageId,
    string newMessage,
    byte[]? entities,
    int editDate,
    byte[]? media,
    List<long>? chatMembers)
    : RequestCommand2<MessageAggregate, MessageId, IExecutionResult>(aggregateId, requestInfo)
{
    public int MessageId { get; } = messageId;
    public string NewMessage { get; } = newMessage;
    public byte[]? Entities { get; } = entities;
    public int EditDate { get; } = editDate;
    public byte[]? Media { get; } = media;
    public List<long>? ChatMembers { get; } = chatMembers;

    protected override IEnumerable<byte[]> GetSourceIdComponents()
    {
        yield return RequestInfo.RequestId.ToByteArray();
    }
}