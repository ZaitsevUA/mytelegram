namespace MyTelegram.Core;

public record DownloadDataReceivedEvent(
    string ConnectionId,
    Guid RequestId,
    uint ObjectId,
    long UserId,
    long ReqMsgId,
    int SeqNumber,
    long AuthKeyId,
    long PermAuthKeyId,
    //ReadOnlyMemory<byte> Data,
    byte[] Data,
    int Layer,
    long Date,
    DeviceType DeviceType,
    string ClientIp
) : DataReceivedEvent(
    ConnectionId,
    RequestId,
    ObjectId,
    UserId,
    ReqMsgId,
    SeqNumber,
    AuthKeyId,
    PermAuthKeyId,
    Data,
    Layer,
    Date,
    DeviceType,
    ClientIp
);