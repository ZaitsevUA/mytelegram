namespace MyTelegram.Core;

//[MemoryPackable]
public record UserSignInSuccessEvent(
    //string ConnectionId,
    long ReqMsgId,
    long TempAuthKeyId,
    long PermAuthKeyId,
    long UserId,
    PasswordState PasswordState,
    bool SendRpcErrorToClient = false
    );