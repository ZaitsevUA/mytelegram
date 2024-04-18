using System.Collections.Concurrent;

namespace MyTelegram.Domain.Aggregates.Chat;

public class ChatSnapshot(
    long chatId,
    string title,
    long creatorUid,
    long? photoId,
    List<ChatMember> chatMembers,
    List<ChatAdmin> chatAdmins,
    List<long> botUserIds,
    long latestSenderPeerId,
    int latestSenderMessageId,
    long latestDeletedUserId,
    ChatBannedRights? defaultBannedRights,
    string? about,
    int? ttlPeriod,
    long? migrateToChannelId,
    bool noForwards)
    : ISnapshot
{
    //IReadOnlyList<ChatMember> chatMemberList,
    //ChatMemberList = chatMemberList;

    public string? About { get; } = about;
    public int? TtlPeriod { get; } = ttlPeriod;
    public long? MigrateToChannelId { get; } = migrateToChannelId;
    public bool NoForwards { get; } = noForwards;

    public long ChatId { get; } = chatId;

    //public IReadOnlyList<ChatMember> ChatMemberList { get; }
    public long CreatorUid { get; } = creatorUid;
    public long? PhotoId { get; } = photoId;
    public List<ChatMember> ChatMembers { get; } = chatMembers;
    public List<ChatAdmin> ChatAdmins { get; } = chatAdmins;
    public List<long> BotUserIds { get; } = botUserIds;
    public ChatBannedRights? DefaultBannedRights { get; } = defaultBannedRights;
    public long LatestDeletedUserId { get; } = latestDeletedUserId;
    public int LatestSenderMessageId { get; } = latestSenderMessageId;
    public long LatestSenderPeerId { get; } = latestSenderPeerId;
    public string Title { get; } = title;
}