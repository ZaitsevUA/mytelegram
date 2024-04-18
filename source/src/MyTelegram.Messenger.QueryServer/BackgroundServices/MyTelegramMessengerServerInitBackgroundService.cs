using EventFlow.ReadStores;
using Microsoft.Extensions.Hosting;
using MyTelegram.Messenger.Services.Filters;
using MyTelegram.Messenger.Services.Interfaces;

namespace MyTelegram.Messenger.QueryServer.BackgroundServices;

public class MyTelegramMessengerServerInitBackgroundService(
    IServiceProvider serviceProvider,
    ILogger<MyTelegramMessengerServerInitBackgroundService> logger,
    IHandlerHelper handlerHelper,
    IDataSeeder dataSeeder,
    IIdGenerator idGenerator,
    IOptions<MyTelegramMessengerServerOptions> options,
    IMongoDbIndexesCreator mongoDbIndexesCreator,
    IEnumerable<IReadStoreManager> readStoreManagers)
    : BackgroundService
{
    private readonly IDataSeeder _dataSeeder = dataSeeder;
    private readonly IIdGenerator _idGenerator = idGenerator;
    private readonly MyTelegramMessengerServerOptions _options = options.Value;

    //private readonly IReadOnlyCollection<IReadStoreManager> _readStoreManagers;
    private readonly IEnumerable<IReadStoreManager> _readStoreManagers = readStoreManagers;
    //private readonly IMyMongoDbReadModelStore<MyTelegram.ReadModel.InMemory.UserReadModel> _readModelStore;

    /*IReadOnlyCollection<IReadStoreManager> readStoreManagers*/
    //_readModelStore = readModelStore;
    //_readStoreManagers = readStoreManagers;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("App init starting...");
        handlerHelper.InitAllHandlers(typeof(MyTelegramMessengerServerExtensions).Assembly);
        await mongoDbIndexesCreator.CreateAllIndexesAsync();
        if (_options.UseInMemoryFilters)
        {
            await serviceProvider.GetRequiredService<IInMemoryFilterDataLoader>().LoadAllFilterDataAsync()
         ;
        }
        //await _dataSeeder.SeedAsync();
        logger.LogInformation("Messenger service init ok");
    }
}
