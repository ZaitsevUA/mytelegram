namespace MyTelegram.GatewayServer.BackgroundServices;

public class MessageDataProcessor(IEventBus eventBus, IOptions<MyTelegramGatewayServerOption> options)
    : IDataProcessor<UnencryptedMessage>,
        IDataProcessor<EncryptedMessage>
{
    public Task ProcessAsync(EncryptedMessage data)
    {
        return eventBus.PublishAsync(new MyTelegram.Core.EncryptedMessage(data.AuthKeyId, data.MsgKey, data.EncryptedData, data.ConnectionId,
            options.Value.MediaOnly ? ConnectionType.Media : ConnectionType.Generic,
            data.ClientIp, data.RequestId, data.Date));
    }

    public Task ProcessAsync(UnencryptedMessage data)
    {
        return eventBus.PublishAsync(new MyTelegram.Core.UnencryptedMessage(data.AuthKeyId, data.ClientIp, data.ConnectionId, data.MessageData, data.MessageDataLength, data.MessageId, data.ObjectId, data.RequestId, data.Date));
    }
}