namespace MyTelegram.Domain.Aggregates.UserName;

public class UserNameSnapshot(
    string? userName,
    bool isDeleted,
    Peer peer
    ) : ISnapshot
{
    public bool IsDeleted { get; } = isDeleted;
    public Peer Peer { get; } = peer;
    public string? UserName { get; } = userName;
}
