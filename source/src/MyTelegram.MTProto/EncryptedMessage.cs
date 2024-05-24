namespace MyTelegram.MTProto;

public record EncryptedMessage(long AuthKeyId,
    byte[] MsgKey,
    byte[] EncryptedData,
    string ConnectionId,
    int ConnectionType,
    string ClientIp,
    Guid RequestId,
    long Date
) : IMtpMessage
{
    public string ConnectionId { get; set; } = ConnectionId;
    public int ConnectionType { get; set; } = ConnectionType;
    public string ClientIp { get; set; } = ClientIp;
}
