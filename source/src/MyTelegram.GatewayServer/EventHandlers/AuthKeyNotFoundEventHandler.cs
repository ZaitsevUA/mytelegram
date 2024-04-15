using EncryptedMessageResponse = MyTelegram.MTProto.EncryptedMessageResponse;

namespace MyTelegram.GatewayServer.EventHandlers;

public class AuthKeyNotFoundEventHandler : IEventHandler<AuthKeyNotFoundEvent>
{
    // 0x6c, 0xfe, 0xff, 0xff
    private static readonly byte[] AuthKeyNotFoundData = { 0x6c, 0xfe, 0xff, 0xff }; //-404
    private readonly IClientDataSender _clientDataSender;

    public AuthKeyNotFoundEventHandler(IClientDataSender clientDataSender)
    {
        _clientDataSender = clientDataSender;
    }

    public Task HandleEventAsync(AuthKeyNotFoundEvent eventData)
    {
        var m = new EncryptedMessageResponse(eventData.AuthKeyId, AuthKeyNotFoundData, eventData.ConnectionId, 2);
        return _clientDataSender.SendAsync(m);
    }
}

public class TransportErrorEventHandler : IEventHandler<TransportErrorEvent>
{
    private readonly IClientDataSender _clientDataSender;

    public TransportErrorEventHandler(IClientDataSender clientDataSender)
    {
        _clientDataSender = clientDataSender;
    }

    public Task HandleEventAsync(TransportErrorEvent eventData)
    {
        var m = new EncryptedMessageResponse(eventData.AuthKeyId, BitConverter.GetBytes(eventData.TransportErrorCode), eventData.ConnectionId, 2);
        return _clientDataSender.SendAsync(m);
    }
}