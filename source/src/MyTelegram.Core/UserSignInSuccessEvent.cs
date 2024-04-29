namespace MyTelegram.Core;

public record UserSignInSuccessEvent(
    long TempAuthKeyId,
    long PermAuthKeyId,
    long UserId,
    PasswordState PasswordState);