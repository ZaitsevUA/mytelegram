namespace MyTelegram.AuthServer.Extensions;

public static class MyTelegramAuthServerExtensions
{
    public static IServiceCollection AddAuthServer(this IServiceCollection services)
    {
        services.RegisterServices();
        services.RegisterHandlers(typeof(MyTelegramAuthServerExtensions).Assembly);

        services.AddMyTelegramCoreServices();
        services.AddMyTelegramHandlerServices();
        services.AddSystemTextJson(options => { options.TypeInfoResolverChain.Add(MyJsonSerializeContext.Default); });

        return services;
    }

    public static void ConfigureEventBus(this IEventBus eventBus)
    {
        eventBus.Subscribe<UnencryptedMessage, UnencryptedMessageHandler>();
    }
}