namespace MyTelegram.Core;

public record UnRegisterAuthKeyEvent(long PermAuthKeyId);
public record SessionRevokedEvent(long PermAuthKeyId, List<long> RevokedPermAuthKeyIdList);