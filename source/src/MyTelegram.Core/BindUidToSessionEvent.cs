namespace MyTelegram.Core;

public record BindUidToSessionEvent(
    long UserId,
    long AuthKeyId,
    long PermAuthKeyId);