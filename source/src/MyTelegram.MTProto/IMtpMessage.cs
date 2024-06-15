namespace MyTelegram.MTProto;

public interface IMtpMessage
{
    string ClientIp { get; set; }
    string ConnectionId { get; set; }
    int ConnectionType { get; set; }
}
