using MyTelegram.Domain.Aggregates.Device;
using MyTelegram.Domain.Commands.Device;

namespace MyTelegram.Messenger.CommandServer.BackgroundServices;

public class NewDeviceCreatedEventDataProcessor(ICommandBus commandBus) : IDataProcessor<NewDeviceCreatedEvent>
{
    public async Task ProcessAsync(NewDeviceCreatedEvent eventData)
    {
        var createDeviceCommand = new CreateDeviceCommand(DeviceId.Create(eventData.PermAuthKeyId),
            eventData.RequestInfo,
            eventData.PermAuthKeyId,
            eventData.TempAuthKeyId,
            eventData.UserId,
            eventData.ApiId,
            eventData.AppVersion,
            eventData.AppVersion,
            eventData.Hash,
            eventData.OfficialApp,
            eventData.PasswordPending,
            eventData.DeviceModel,
            eventData.Platform,
            eventData.SystemVersion,
            eventData.SystemLangCode,
            eventData.LangPack,
            eventData.LangCode,
            eventData.Ip,
            eventData.Layer
        );
        await commandBus.PublishAsync(createDeviceCommand, default);
    }
}