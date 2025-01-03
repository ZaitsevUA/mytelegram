﻿namespace MyTelegram.AuthServer.EventHandlers;

public class UnencryptedMessageHandler(
    ILogger<UnencryptedMessageHandler> logger,
    IHandlerHelper handlerHelper,
    IEventBus eventBus
) : IEventHandler<UnencryptedMessage>, ITransientDependency
{
    public async Task HandleEventAsync(UnencryptedMessage eventData)
    {
        try
        {
            if (!handlerHelper.TryGetHandler(eventData.ObjectId, out var handler))
            {
                logger.LogWarning(
                    "Cannot find a handler with objectId {ObjectId:x2}, connectionId: {ConnectionId}",
                    eventData.ObjectId,
                    eventData.ConnectionId
                );

                return;
            }

            if (logger.IsEnabled(LogLevel.Trace))
            {
                logger.LogTrace(
                    "Processing unencrypted message, connectionId: {ConnectionId}, [objectId]: {ObjectId:x2}, handler: {Handler}, reqMsgId: {ReqMsgId}",
                    eventData.ConnectionId,
                    eventData.ObjectId,
                    handler.GetType().Name,
                    eventData.MessageId
                );
            }

            var obj = eventData.MessageData.ToTObject<IObject>();

            var r = await handler.HandleAsync(
                new RequestInput(
                    eventData.ConnectionId,
                    eventData.RequestId,
                    eventData.ObjectId,
                    eventData.MessageId,
                    0,
                    eventData.AuthKeyId,
                    eventData.AuthKeyId,
                    0,
                    eventData.Date,
                    DeviceType.Unknown,
                    eventData.ClientIp
                ),
                obj
            );

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
            logger.LogError(
                "Process request failed, connectionId: {ConnectionId}, reqMsgId: {ReqMsgId}, error: {Error}",
                eventData.ConnectionId,
                eventData.MessageId,
                ex
            );
        }

        if (logger.IsEnabled(LogLevel.Trace))
        {
            logger.LogTrace(
                "Process unencrypted message completed, reqMsgId: {ReqMsgId}",
                eventData.MessageId
            );
        }
    }
}