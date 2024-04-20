namespace MyTelegram.Domain.ValueObjects;

public class ChatMember(
    long userId,
    long inviterId,
    int date) : ValueObject
{
    public int Date { get; init; } = date;
    public long InviterId { get; init; } = inviterId;

    public long UserId { get; init; } = userId;
}
