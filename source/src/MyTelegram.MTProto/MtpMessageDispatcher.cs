namespace MyTelegram.MTProto;

public class MtpMessageDispatcher(
    IMessageQueueProcessor<UnencryptedMessage> unencryptedMessageQueueProcessor,
    IMessageQueueProcessor<EncryptedMessage> encryptedMessageQueueProcessor)
    : IMtpMessageDispatcher
{
    public Task DispatchAsync(IMtpMessage message)
    {
        switch (message)
        {
            case EncryptedMessage encryptedMessage:
                encryptedMessageQueueProcessor.Enqueue(encryptedMessage, encryptedMessage.AuthKeyId);
                break;
            case UnencryptedMessage unencryptedMessage:
                unencryptedMessageQueueProcessor.Enqueue(unencryptedMessage, unencryptedMessage.AuthKeyId);
                break;
        }

        return Task.CompletedTask;
    }
}
