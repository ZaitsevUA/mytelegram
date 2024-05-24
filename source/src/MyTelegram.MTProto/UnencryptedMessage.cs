namespace MyTelegram.MTProto;

public record UnencryptedMessage(
    long AuthKeyId,
    string ClientIp,
    string ConnectionId,
    int ConnectionType,
    byte[] MessageData,
    int MessageDataLength,
    long MessageId,
    uint ObjectId,
    Guid RequestId,
    long Date
) : IMtpMessage
{
    public string ConnectionId { get; set; } = ConnectionId;
    public int ConnectionType { get; set; } = ConnectionType;
    public string ClientIp { get; set; } = ClientIp;
}