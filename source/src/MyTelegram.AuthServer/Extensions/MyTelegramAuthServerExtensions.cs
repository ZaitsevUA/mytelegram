namespace MyTelegram.AuthServer.Extensions;

public static class MyTelegramAuthServerExtensions
{
    public static IServiceCollection AddAuthServer(this IServiceCollection services)
    {
        services.AddSingleton<IStep1Helper, Step1Helper>();
        services.AddSingleton<IStep2Helper, Step2Helper>();
        services.AddSingleton<IStep3Helper, Step3Helper>();
        services.AddSingleton<IRsaKeyProvider, RsaKeyProvider>();
        services.AddSingleton<IFingerprintHelper, FingerprintHelper>();

        services.RegisterHandlers(typeof(MyTelegramAuthServerExtensions).Assembly);

        services.AddMyTelegramCoreServices();
        services.AddMyTelegramHandlerServices();
        //services.AddMyTelegramRabbitMqEventBus();
        services.AddSystemTextJson(options =>
        {
            options.TypeInfoResolverChain.Add(MyJsonSerializeContext.Default);
        });
        services.AddTransient<UnencryptedMessageHandler>();

        return services;
    }

    public static void ConfigureEventBus(this IEventBus eventBus)
    {
        eventBus.Subscribe<UnencryptedMessage, UnencryptedMessageHandler>();
    }
}