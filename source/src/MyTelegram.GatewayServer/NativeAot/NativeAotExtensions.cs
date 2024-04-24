namespace MyTelegram.GatewayServer.NativeAot;

public static class NativeAotExtensions
{
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(MyTelegramGatewayServerOption))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(List<GatewayServerItem>))]

    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(MyTelegram.MTProto.EncryptedMessageResponse))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(MyTelegram.Core.EncryptedMessageResponse))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(EncryptedMessage))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(UnencryptedMessage))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(MyTelegram.MTProto.UnencryptedMessageResponse))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(MyTelegram.Core.UnencryptedMessageResponse))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(AuthKeyNotFoundEvent))]
    public static void FixNativeAotIssues(this IServiceCollection services)
    {
        //services.AddTransient<IRabbitMqSerializer, NativeAotUtf8JsonRabbitMqSerializer>();
        //services.AddTransient<IJsonContextProvider, GatewayServerJsonContextProvider>();
    }
}