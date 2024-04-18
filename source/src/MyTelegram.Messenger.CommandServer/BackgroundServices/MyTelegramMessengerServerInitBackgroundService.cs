using Microsoft.Extensions.Hosting;
using MyTelegram.Messenger.Extensions;
using MyTelegram.Messenger.Services.Filters;

namespace MyTelegram.Messenger.CommandServer.BackgroundServices;

public class MyTelegramMessengerServerInitBackgroundService(
    IServiceProvider serviceProvider,
    ILogger<MyTelegramMessengerServerInitBackgroundService> logger,
    IHandlerHelper handlerHelper,
    IDataSeeder dataSeeder,
    IIdGenerator idGenerator,
    IOptions<MyTelegramMessengerServerOptions> options,
    IMongoDbIndexesCreator mongoDbIndexesCreator)
    : BackgroundService
{
    private readonly IIdGenerator _idGenerator = idGenerator;
    private readonly MyTelegramMessengerServerOptions _options = options.Value;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        
        logger.LogInformation("App init starting...");
        handlerHelper.InitAllHandlers(typeof(MyTelegramMessengerServerExtensions).Assembly);
        //IdGeneratorFactory.SetDefaultIdGenerator(_idGenerator);
        await mongoDbIndexesCreator.CreateAllIndexesAsync();
        if (_options.UseInMemoryFilters)
        {
            await serviceProvider.GetRequiredService<IInMemoryFilterDataLoader>().LoadAllFilterDataAsync()
         ;
        }
        await dataSeeder.SeedAsync();
        logger.LogInformation("Messenger service init ok");
    }
}
