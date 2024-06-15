namespace MyTelegram.Domain.Extensions;

public static class EventFlowExtensions
{
    public static IServiceCollection AddInMemorySnapshotAggregateServices(this IServiceCollection services)
    {
        return services;
    }
}
