namespace MyTelegram.Domain.Commands.PushUpdates;

public class CreateEncryptedPushUpdatesCommand(
    PushUpdatesId aggregateId,
    long inboxOwnerPeerId,
    byte[] data,
    int qts,
    long inboxOwnerPermAuthKeyId)
    : Command<PushUpdatesAggregate, PushUpdatesId, IExecutionResult>(aggregateId)
{
    public byte[] Data { get; } = data;
    public long InboxOwnerPermAuthKeyId { get; } = inboxOwnerPermAuthKeyId;
    public long InboxOwnerPeerId { get; } = inboxOwnerPeerId;
    public int Qts { get; } = qts;
}
