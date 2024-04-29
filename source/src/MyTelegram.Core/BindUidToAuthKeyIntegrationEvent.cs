namespace MyTelegram.Core;

public record BindUidToAuthKeyIntegrationEvent(long AuthKeyId,
    long PermAuthKeyId,
    long UserId);