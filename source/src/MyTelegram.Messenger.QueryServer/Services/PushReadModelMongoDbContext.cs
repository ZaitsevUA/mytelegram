using Microsoft.Extensions.Configuration;

namespace MyTelegram.Messenger.QueryServer.Services;

public class PushReadModelMongoDbContext(IConfiguration configuration) : DefaultReadModelMongoDbContext(configuration), ISingletonDependency
{
    protected override string GetKeyOfDatabaseNameInConfiguration() => "App:QueryServerReadModelDatabaseName";
}