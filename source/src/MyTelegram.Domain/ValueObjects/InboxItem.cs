namespace MyTelegram.Domain.ValueObjects;

public class InboxItem(
    long inboxOwnerPeerId,
    int inboxMessageId) : ValueObject
{
    public int InboxMessageId { get; init; } = inboxMessageId;

    public long InboxOwnerPeerId { get; init; } = inboxOwnerPeerId;
}
