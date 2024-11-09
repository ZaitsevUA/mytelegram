using EventFlow.Core.Caching;
using EventFlow.MongoDB.Extensions;
using EventFlow.Sagas;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MyTelegram.Domain.EventFlow;
using MyTelegram.Messenger.NativeAot;
using MyTelegram.Messenger.Services.Filters;
using MyTelegram.Messenger.Services.Impl;
using MyTelegram.QueryHandlers.MongoDB;
using MyTelegram.ReadModel.MongoDB;
using System.Text.Json.Serialization;

namespace MyTelegram.Messenger.Extensions;

public static class MyTelegramMessengerServerExtensions
{
    private static void RegisterMongoDbSerializer(this IServiceCollection services)
    {
        var baseType = typeof(IObject);
        var objectSerializer = new ObjectSerializer(type => type.IsAssignableTo(baseType));
        BsonSerializer.RegisterSerializer(objectSerializer);
        var asm = baseType.Assembly;
        var baseInterfaceTypes = asm
                .GetTypes()
                .Where(t => t.IsInterface && t.IsAssignableTo(baseType) &&
                            t.GetCustomAttributes<JsonDerivedTypeAttribute>().Any())
                .ToList();
        var types = asm.GetTypes()
            .Where(t => baseInterfaceTypes.Any(t.IsAssignableTo) &&
                        t is { IsAbstract: false, IsInterface: false })
        ;
        foreach (var type in types)
        {
            var cm = new BsonClassMap(type);
            cm.AutoMap();
            cm.SetDiscriminator(type.Name);
            BsonClassMap.RegisterClassMap(cm);
        }
    }
    public static void AddMyTelegramMessengerServer(this IServiceCollection services,
       Action<IEventFlowOptions>? configure = null)
    {
        services.AddTransient<IMongoDbIndexesCreator, MongoDbIndexesCreator>();
        services.AddEventFlow(options =>
        {
            options.AddDefaults(typeof(MyTelegramMessengerServerExtensions).Assembly);
            options.AddDefaults(typeof(EventFlowExtensions).Assembly);
            options.Configure(c => { c.IsAsynchronousSubscribersEnabled = true; });

            options.UseMongoDbEventStore();
            options.UseMongoDbSnapshotStore();

            options.AddMessengerMongoDbReadModel();
            options.AddMongoDbQueryHandlers();

            configure?.Invoke(options);

            options.AddSystemTextJson(jsonSerializerOptions =>
            {
                jsonSerializerOptions.AddSingleValueObjects(
                    new EventFlow.SystemTextJsonSingleValueObjectConverter<CacheKey>());
                jsonSerializerOptions.TypeInfoResolverChain.Add(MyMessengerJsonContext.Default);
            });
        })
            .AddMyTelegramCoreServices()
            .AddMyTelegramHandlerServices()
            .AddMyTelegramMessengerServices()
            .AddMyTelegramIdGeneratorServices()
            .AddMyEventFlow()
            ;

        services.AddMyNativeAot();
    }


    public static IServiceCollection AddMyTelegramIdGeneratorServices(this IServiceCollection services)
    {
        services.AddSingleton<IHiLoValueGeneratorCache, HiLoValueGeneratorCache>();
        services.AddTransient<IHiLoValueGeneratorFactory, HiLoValueGeneratorFactory>();
        services.AddSingleton<IHiLoStateBlockSizeHelper, HiLoStateBlockSizeHelper>();
        services.AddSingleton<IRedisIdGenerator, RedisIdGenerator>();
        services.AddTransient<IHiLoHighValueGenerator, MongoDbHighValueGenerator>();
        services.AddSingleton<IMongoDbIdGenerator, MongoDbIdGenerator>();
        return services;
    }

    public static IServiceCollection AddMyTelegramMessengerServices(this IServiceCollection services)
    {
        services.RegisterMongoDbSerializer();
        services.RegisterServices();

        services.AddLayeredServices();
        services.AddLayeredResultConverters();
        services.RegisterHandlers(typeof(MyTelegramMessengerServerExtensions).Assembly);
        services.RegisterAllMappers();
        services.AddSingleton<ISagaStore, MySagaAggregateStore>();

        services.AddSingleton(typeof(IDomainEventCacheHelper<>), typeof(DomainEventCacheHelper<>));
        services.AddSingleton<ICountryHelper, CountryHelper>();

        return services;
    }
}