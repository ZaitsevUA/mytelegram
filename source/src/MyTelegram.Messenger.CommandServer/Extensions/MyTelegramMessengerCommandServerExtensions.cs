using MyTelegram.Caching.Redis;
using MyTelegram.EventFlow;
using MyTelegram.Messenger.CommandServer.BackgroundServices;
using MyTelegram.Messenger.CommandServer.EventHandlers;
using MyTelegram.Messenger.NativeAot;
using MyTelegram.Services.Extensions;
using MyTelegram.Services.NativeAot;

namespace MyTelegram.Messenger.CommandServer.Extensions;
public static class MyTelegramMessengerCommandServerExtensions
{
    public static void ConfigureEventBus(this IEventBus eventBus)
    {
        eventBus.Subscribe<MessengerCommandDataReceivedEvent, MessengerEventHandler>();
        eventBus.Subscribe<NewDeviceCreatedEvent, MessengerEventHandler>();
        eventBus.Subscribe<BindUidToAuthKeyIntegrationEvent, MessengerEventHandler>();
        eventBus.Subscribe<AuthKeyUnRegisteredIntegrationEvent, MessengerEventHandler>();

    }

    public static void AddMyTelegramMessengerCommandServer(this IServiceCollection services,
        Action<IEventFlowOptions>? configure = null)
    {
        //services.AddTransient<PtsEventHandler>();
        services.AddTransient<MessengerEventHandler>();
        services.AddTransient<IDataProcessor<NewDeviceCreatedEvent>, NewDeviceCreatedEventDataProcessor>();
        //services.AddTransient<IReadModelUpdateManager, MyTelegramCommandServerReadModelUpdateManager>();

        services.AddCacheJsonSerializer(jsonOptions =>
        {
            jsonOptions.TypeInfoResolverChain.Add(MyJsonSerializeContext.Default);
            jsonOptions.TypeInfoResolverChain.Add(MyMessengerJsonContext.Default);
        });
        services.AddSystemTextJson(options =>
        {
            options.AddSingleValueObjects();
            options.TypeInfoResolverChain.Add(MyJsonSerializeContext.Default);
            options.TypeInfoResolverChain.Add(MyMessengerJsonContext.Default);
        });
    }
}
