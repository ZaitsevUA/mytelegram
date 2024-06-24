namespace MyTelegram.AuthServer.EventHandlers;

public class UnencryptedMessageHandler(
    ILogger<UnencryptedMessageHandler> logger,
    IHandlerHelper handlerHelper,
    IEventBus eventBus)
    : IEventHandler<UnencryptedMessage>
{
    public async Task HandleEventAsync(UnencryptedMessage eventData)
    {
        try
        {
            if (!handlerHelper.TryGetHandler(eventData.ObjectId, out var handler))
            {
                logger.LogWarning(
                    "ConnectionId={ConnectionId} Can not find object handler for objectId={ObjectId:x2}",
                    eventData.ConnectionId,
                    eventData.ObjectId);

                return;
            }

            if (logger.IsEnabled(LogLevel.Trace))
            {
                logger.LogTrace(
                    "Start process unencrypted message,connectionId={ConnectionId} [req]#{ObjectId:x2} {Handler} reqMsgId={ReqMsgId}",
                    eventData.ConnectionId,
                    eventData.ObjectId,
                    handler.GetType().Name,
                    eventData.MessageId
                );
            }

            var obj = eventData.MessageData.ToTObject<IObject>();

            var r = await handler.HandleAsync(new RequestInput(
                    eventData.ConnectionId,
                    eventData.RequestId,
                    eventData.ObjectId,
                    eventData.MessageId,
                    0,
                    eventData.AuthKeyId,
                    eventData.AuthKeyId, 0, eventData.Date, DeviceType.Unknown, eventData.ClientIp),
                obj);

            if (r != null)
            {
                var unencryptedResponse = new UnencryptedMessageResponse(
                    eventData.AuthKeyId,
                    r.ToBytes(),
                    eventData.ConnectionId,
                    eventData.MessageId
                );
                await eventBus.PublishAsync(unencryptedResponse);
            }
        }
        catch (Exception ex)
        {
            logger.LogError("ConnectionId={ConnectionId} process request failed,reqMsgId={ReqMsgId},error={Error}",
                eventData.ConnectionId, eventData.MessageId, ex);
        }

        if (logger.IsEnabled(LogLevel.Trace))
        {
            logger.LogTrace("Process unencrypted message completed,reqMsgId={ReqMsgId}", eventData.MessageId);
        }
    }
}