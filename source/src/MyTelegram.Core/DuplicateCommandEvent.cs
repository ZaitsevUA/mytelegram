namespace MyTelegram.Core;

public record DuplicateCommandEvent(long UserId, long ReqMsgId);