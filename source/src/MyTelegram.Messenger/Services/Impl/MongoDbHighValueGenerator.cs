using MyTelegram.Services.Services.IdGenerator;

namespace MyTelegram.Messenger.Services.Impl;


public class MongoDbHighValueGenerator(IMongoDbIdGenerator idGenerator) : IHiLoHighValueGenerator
{
    public Task<long> GetNewHighValueAsync(IdType idType, long key, CancellationToken cancellationToken = default)
    {
        return idGenerator.NextLongIdAsync(idType, key, cancellationToken: cancellationToken);
    }
}
