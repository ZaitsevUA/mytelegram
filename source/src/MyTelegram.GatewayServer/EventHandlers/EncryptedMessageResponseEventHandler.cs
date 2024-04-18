namespace MyTelegram.GatewayServer.EventHandlers;

public class EncryptedMessageResponseEventHandler(IClientDataSender clientDataSender)
    : IEventHandler<Core.EncryptedMessageResponse>
{
    public Task HandleEventAsync(Core.EncryptedMessageResponse eventData)
    {
        return clientDataSender.SendAsync(new MTProto.EncryptedMessageResponse(eventData.AuthKeyId,eventData.Data,eventData.ConnectionId,eventData.SeqNumber));
    }
}
