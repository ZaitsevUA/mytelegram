using Microsoft.Extensions.Configuration;

namespace MyTelegram.Messenger.QueryServer.Services;

public class PushReadModelMongoDbContext(IConfiguration configuration) : DefaultReadModelMongoDbContext(configuration)
{
    protected override string GetKeyOfDatabaseNameInConfiguration() => "App:QueryServerReadModelDatabaseName";
}