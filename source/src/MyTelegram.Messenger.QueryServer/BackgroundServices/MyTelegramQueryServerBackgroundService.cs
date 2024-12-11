using Microsoft.Extensions.Hosting;
using MyTelegram.Messenger.Services.Filters;

namespace MyTelegram.Messenger.QueryServer.BackgroundServices;

public class MyTelegramQueryServerBackgroundService(
    IServiceProvider serviceProvider,
    ILogger<MyTelegramQueryServerBackgroundService> logger,
    IHandlerHelper handlerHelper,
    IOptionsMonitor<MyTelegramMessengerServerOptions> options,
    IMongoDbIndexesCreator mongoDbIndexesCreator)
    : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("Query server starting...");
        handlerHelper.InitAllHandlers(typeof(MyTelegramMessengerServerExtensions).Assembly);
        await mongoDbIndexesCreator.CreateAllIndexesAsync();
        if (options.CurrentValue.UseInMemoryFilters)
        {
            await serviceProvider.GetRequiredService<IInMemoryFilterDataLoader>().LoadAllFilterDataAsync()
         ;
        }
        //await _dataSeeder.SeedAsync();
        logger.LogInformation("Query server started");
    }
}
