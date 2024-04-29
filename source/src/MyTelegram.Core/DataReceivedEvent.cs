namespace MyTelegram.Core;

public record DataReceivedEvent(
    string ConnectionId,
    Guid RequestId,
    uint ObjectId,
    long UserId,
    long ReqMsgId,
    int SeqNumber,
    long AuthKeyId,
    long PermAuthKeyId,
    byte[] Data,
    //byte[] Data,
    int Layer,
    long Date,
    DeviceType DeviceType,
    string ClientIp
);