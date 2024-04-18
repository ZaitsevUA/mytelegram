using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace MyTelegram.EventFlow.MongoDB;

public class DefaultReadModelMongoDbContext(IConfiguration configuration) : IMongoDbContext
{
    private IMongoDatabase? _database;

    public IMongoDatabase GetDatabase()
    {
        if (_database == null)
        {
            var connectionString = configuration.GetConnectionString(GetConnectionStringName());
            var databaseName = configuration.GetValue<string>(GetKeyOfDatabaseNameInConfiguration());
            var client = new MongoClient(connectionString);
            //client.Settings.MaxConnecting = 5000;
            //client.Settings.MaxConnectionPoolSize = 5000;
            _database = client.GetDatabase(databaseName);
        }

        return _database;
    }

    protected virtual string GetConnectionStringName() => "Default";
    protected virtual string GetKeyOfDatabaseNameInConfiguration() => "App:DatabaseName";
}