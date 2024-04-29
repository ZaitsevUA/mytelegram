namespace MyTelegram.Core;

public record NewPtsMessageHasSentEvent(
    long ToUid,
    Peer ToPeer,
    long MsgId,
    long TempAuthKeyId,
    long PermAuthKeyId,
    int Pts,
    long GlobalSeqNo,
    long ReqMsgId);