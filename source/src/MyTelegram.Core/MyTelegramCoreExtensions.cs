namespace MyTelegram.Core;

public static class MyTelegramCoreExtensions
{
    public static IServiceCollection AddMyTelegramCoreServices(this IServiceCollection services)
    {
        services.AddTransient<IClock, UtcClock>();
        services.AddSingleton<IRandomHelper, RandomHelper>();
        services.AddSingleton<IHashHelper, HashHelper>();
        services.AddSingleton<IMyRsaHelper, MyRsaHelper>();
        services.AddSingleton<IAesHelper, AesHelper>();
        //services.AddSingleton<IOldMtpHelper, OldMtpHelper>();
        //services.AddSingleton<IMtpHelper, MtpHelper>();
        services.AddSingleton<IAuthKeyIdHelper, AuthKeyIdHelper>();

        services.AddSingleton<IObjectMapper, DefaultObjectMapper>();
        services.AddSingleton(typeof(IObjectMapper<>), typeof(DefaultObjectMapper<>));

        return services;
    }
}