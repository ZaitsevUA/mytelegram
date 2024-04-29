namespace MyTelegram.Core;

public record FileDataResultResponseReceivedEvent(long ReqMsgId,
    byte[] Data) : ISessionMessage;