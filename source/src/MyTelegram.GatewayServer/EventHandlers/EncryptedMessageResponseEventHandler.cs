namespace MyTelegram.GatewayServer.EventHandlers;

public class EncryptedMessageResponseEventHandler(IClientDataSender clientDataSender)
    : IEventHandler<MyTelegram.Core.EncryptedMessageResponse>
{
    public Task HandleEventAsync(MyTelegram.Core.EncryptedMessageResponse eventData)
    {
        return clientDataSender.SendAsync(new MyTelegram.MTProto.EncryptedMessageResponse(eventData.AuthKeyId,eventData.Data,eventData.ConnectionId,eventData.SeqNumber));
    }
}
