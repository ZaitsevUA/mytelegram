using EventFlow.Exceptions;
using MyTelegram.Domain.Aggregates.Device;
using MyTelegram.Domain.Commands.Device;

namespace MyTelegram.Messenger.CommandServer.EventHandlers;

public class MessengerEventHandler(
    ICommandBus commandBus,
    IMessageQueueProcessor<MessengerCommandDataReceivedEvent> processor,
    IObjectMessageSender objectMessageSender,
    ILogger<MessengerEventHandler> logger,
    IMessageQueueProcessor<NewDeviceCreatedEvent> newDeviceCreatedProcessor)
    :
        IEventHandler<MessengerCommandDataReceivedEvent>,
        IEventHandler<NewDeviceCreatedEvent>,
        IEventHandler<BindUidToAuthKeyIntegrationEvent>,
        IEventHandler<AuthKeyUnRegisteredIntegrationEvent>
{
    public Task HandleEventAsync(AuthKeyUnRegisteredIntegrationEvent eventData)
    {
        var command = new UnRegisterDeviceForAuthKeyCommand(DeviceId.Create(eventData.PermAuthKeyId),
            eventData.PermAuthKeyId,
            eventData.TempAuthKeyId);
        return commandBus.PublishAsync(command, default);
    }

    public Task HandleEventAsync(BindUidToAuthKeyIntegrationEvent eventData)
    {
        var command = new BindUidToDeviceCommand(DeviceId.Create(eventData.PermAuthKeyId),
            eventData.UserId,
            eventData.PermAuthKeyId);
        return commandBus.PublishAsync(command, default);
    }

    public Task HandleEventAsync(MessengerCommandDataReceivedEvent eventData)
    {
        processor.Enqueue(eventData, eventData.PermAuthKeyId);
        return Task.CompletedTask;
    }

    public Task HandleEventAsync(NewDeviceCreatedEvent eventData)
    {
        newDeviceCreatedProcessor.Enqueue(eventData, 0);
        return Task.CompletedTask;
        //try
        //{
        //    var createDeviceCommand = new CreateDeviceCommand(DeviceId.Create(eventData.PermAuthKeyId),
        //        eventData.RequestInfo,
        //        eventData.PermAuthKeyId,
        //        eventData.TempAuthKeyId,
        //        eventData.UserId,
        //        eventData.ApiId,
        //        eventData.AppVersion,
        //        eventData.AppVersion,
        //        eventData.Hash,
        //        eventData.OfficialApp,
        //        eventData.PasswordPending,
        //        eventData.DeviceModel,
        //        eventData.Platform,
        //        eventData.SystemVersion,
        //        eventData.SystemLangCode,
        //        eventData.LangPack,
        //        eventData.LangCode,
        //        eventData.Ip,
        //        eventData.Layer
        //    );
        //    await _commandBus.PublishAsync(createDeviceCommand, default);
        //}
        //catch (DuplicateOperationException)
        //{
            // Ignore duplicate exception
        //}
    }

    public Task HandleEventAsync(UserIsOnlineEvent eventData)
    {
        logger.LogDebug("User {UserId} is online,tempAuthKeyId={TempAuthKeyId:x2},permAuthKeyId={PermAuthKeyId:x2}",
            eventData.UserId,
            eventData.TempAuthKeyId,
            eventData.PermAuthKeyId);
        var updatesTooLong = new TUpdatesTooLong();
        return objectMessageSender.PushSessionMessageToAuthKeyIdAsync(eventData.TempAuthKeyId, updatesTooLong);
    }
}
