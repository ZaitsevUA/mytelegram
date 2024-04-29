namespace MyTelegram.Core;

public record ChatMemberChangedEvent(long ChatId,
    MemberStateChangeType MemberStateChangeType,
    IReadOnlyList<long> MemberUidList);