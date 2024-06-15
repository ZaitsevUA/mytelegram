namespace MyTelegram.GatewayServer.EventHandlers;

public class UnencryptedMessageResponseEventHandler(IClientDataSender clientDataSender)
    : IEventHandler<MyTelegram.Core.UnencryptedMessageResponse>
{
    public Task HandleEventAsync(MyTelegram.Core.UnencryptedMessageResponse eventData)
    {
        return clientDataSender.SendAsync(new MyTelegram.MTProto.UnencryptedMessageResponse(eventData.AuthKeyId, eventData.Data, eventData.ConnectionId, eventData.ReqMsgId));
    }
}