using Microsoft.Extensions.Hosting;
using MyTelegram.Messenger.Services.Caching;
using MyTelegram.Messenger.Services.Filters;

namespace MyTelegram.Messenger.QueryServer.BackgroundServices;

public class MyTelegramQueryServerBackgroundService(
    IServiceProvider serviceProvider,
    ILogger<MyTelegramQueryServerBackgroundService> logger,
    IHandlerHelper handlerHelper,
    IOptionsMonitor<MyTelegramMessengerServerOptions> options,
    ILanguageCacheService languageCacheService,
    IMongoDbIndexesCreator mongoDbIndexesCreator)
    : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("Query server starting...");
        handlerHelper.InitAllHandlers();
        await mongoDbIndexesCreator.CreateAllIndexesAsync();
        if (options.CurrentValue.UseInMemoryFilters)
        {
            await serviceProvider.GetRequiredService<IInMemoryFilterDataLoader>().LoadAllFilterDataAsync()
         ;
        }
        await languageCacheService.LoadAllLanguagesAsync();
        await languageCacheService.LoadAllLanguageTextAsync();

        logger.LogInformation("Query server started");
    }
}
