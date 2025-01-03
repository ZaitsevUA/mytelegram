using EncryptedMessageResponse = MyTelegram.Core.EncryptedMessageResponse;

namespace MyTelegram.GatewayServer.EventHandlers;

public class EncryptedMessageResponseEventHandler(IClientDataSender clientDataSender)
    : IEventHandler<EncryptedMessageResponse>, ITransientDependency
{
    public Task HandleEventAsync(EncryptedMessageResponse eventData)
    {
        return clientDataSender.SendAsync(new MTProto.EncryptedMessageResponse(eventData.AuthKeyId, eventData.Data,
            eventData.ConnectionId, eventData.SeqNumber));
    }
}