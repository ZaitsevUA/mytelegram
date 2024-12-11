using Microsoft.Extensions.Hosting;
using MyTelegram.Messenger.Services.Filters;

namespace MyTelegram.Messenger.CommandServer.BackgroundServices;

public class MyTelegramCommandServerBackgroundService(
    IServiceProvider serviceProvider,
    ILogger<MyTelegramCommandServerBackgroundService> logger,
    IHandlerHelper handlerHelper,
    IDataSeeder dataSeeder,
    IOptions<MyTelegramMessengerServerOptions> options,
    IMongoDbIndexesCreator mongoDbIndexesCreator)
    : BackgroundService
{
    private readonly MyTelegramMessengerServerOptions _options = options.Value;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("Command server starting...");
        handlerHelper.InitAllHandlers(typeof(MyTelegramMessengerServerExtensions).Assembly);
        //IdGeneratorFactory.SetDefaultIdGenerator(_idGenerator);
        await mongoDbIndexesCreator.CreateAllIndexesAsync();
        if (_options.UseInMemoryFilters)
        {
            await serviceProvider.GetRequiredService<IInMemoryFilterDataLoader>().LoadAllFilterDataAsync()
         ;
        }
        await dataSeeder.SeedAsync();
        logger.LogInformation("Command server started");
    }
}
