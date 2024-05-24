namespace MyTelegram.Domain.Aggregates.Channel;

public class ChannelMemberSnapshot(
    bool banned,
    ChatBannedRights? bannedRights,
    bool kicked,
    long kickedBy,
    bool left,
    bool isBot)
    : ISnapshot
{
    public bool Banned { get; } = banned;

    public ChatBannedRights? BannedRights { get; } = bannedRights;

    public bool Kicked { get; } = kicked;
    public long KickedBy { get; } = kickedBy;
    public bool Left { get; } = left;
    public bool IsBot { get; } = isBot;
}