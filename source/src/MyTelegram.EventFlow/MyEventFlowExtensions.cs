namespace MyTelegram.EventFlow;
public static class MyEventFlowExtensions
{
    public static IServiceCollection AddMyEventFlow(this IServiceCollection services)
    {
        services.AddSingleton<IInMemoryEventPersistence, MyInMemoryEventPersistence>();

        services.AddTransient<IEventStore, MyEventStoreBase>();

        services.AddTransient<ISnapshotStore, SnapshotWithInMemoryCacheStore>();
        services.AddSingleton<IMyInMemorySnapshotPersistence, MyInMemorySnapshotPersistence>();
        
        return services;
    }
}