using EventFlow.MongoDB.EventStore;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace MyTelegram.Messenger.Services.Impl;

public interface IMongoDbIdGenerator : IIdGenerator
{

}

public class MongoDbIdGenerator(IMongoDbEventSequenceStore eventSequenceStore) : IMongoDbIdGenerator
{
    public async Task<int> NextIdAsync(IdType idType, long id, int step = 1, CancellationToken cancellationToken = default)
    {
        return (int)(await NextLongIdAsync(idType, id, step, cancellationToken));
    }

    public Task<long> NextLongIdAsync(IdType idType, long id = 0, int step = 1, CancellationToken cancellationToken = default)
    {
        var nextId = eventSequenceStore.GetNextSequence($"{idType}-{id}");
        return Task.FromResult(nextId);
    }
}

public interface IRedisIdGenerator : IIdGenerator
{

}

public class RedisIdGenerator(IConfiguration configuration) : IRedisIdGenerator
{
    private ConnectionMultiplexer? _connection;
    private IDatabaseAsync? _database;

    public async Task<int> NextIdAsync(IdType idType, long id, int step = 1, CancellationToken cancellationToken = default)
    {
        return (int)(await NextLongIdAsync(idType, id, step, cancellationToken));
    }

    public async Task<long> NextLongIdAsync(IdType idType, long id = 0, int step = 1, CancellationToken cancellationToken = default)
    {
        var db = await GetDatabaseAsync();
        return await db.StringIncrementAsync(GetKey(idType, id));
    }

    private string GetKey(IdType idType, long id)
    {
        return MyCacheKey.With("seq", $"{idType}", $"{id}");
        //return $"m:seq:{idType}_{id}";
    }

    private async Task<IDatabaseAsync> GetDatabaseAsync()
    {
        if (_database == null)
        {
            var connection = await CreateConnectionAsync();
            _database = connection.GetDatabase();
        }

        return _database;
    }

    private async Task<ConnectionMultiplexer> CreateConnectionAsync()
    {
        if (_connection == null || !_connection.IsConnected)
        {
            var connectionString = configuration.GetValue<string>("Redis:Configuration");
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentException("Redis configuration is null,section name is 'Redis:Configuration'");
            }

            _connection =
                await ConnectionMultiplexer.ConnectAsync(connectionString);
        }

        return _connection;
    }
}