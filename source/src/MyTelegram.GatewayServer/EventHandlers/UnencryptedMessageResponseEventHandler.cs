using UnencryptedMessageResponse = MyTelegram.Core.UnencryptedMessageResponse;

namespace MyTelegram.GatewayServer.EventHandlers;

public class UnencryptedMessageResponseEventHandler(IClientDataSender clientDataSender)
    : IEventHandler<UnencryptedMessageResponse>, ITransientDependency
{
    public Task HandleEventAsync(UnencryptedMessageResponse eventData)
    {
        return clientDataSender.SendAsync(new MTProto.UnencryptedMessageResponse(eventData.AuthKeyId, eventData.Data,
            eventData.ConnectionId, eventData.ReqMsgId));
    }
}