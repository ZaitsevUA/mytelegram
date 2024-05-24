using Microsoft.Extensions.Options;

namespace MyTelegram.Services.Services.IdGenerator;


//public class MongoDbHighValueGenerator(IMongoDbIdGenerator idGenerator) : IHiLoHighValueGenerator
//{
//    public Task<long> GetNewHighValueAsync(IdType idType, long key, CancellationToken cancellationToken = default)
//    {
//        return idGenerator.NextLongIdAsync(idType, key, cancellationToken: cancellationToken);
//    }
//}

//public class RedisHighValueGenerator(IRedisIdGenerator redisIdGenerator) : IHiLoHighValueGenerator
//{
//    public Task<long> GetNewHighValueAsync(IdType idType, long key, CancellationToken cancellationToken = default)
//    {
//        return redisIdGenerator.NextLongIdAsync(idType, key, cancellationToken: cancellationToken);
//    }
//}

//public class HiLoHighValueGenerator(IOptions<MyTelegramMessengerServerOptions> options) : IHiLoHighValueGenerator
//{
//    public async Task<long> GetNewHighValueAsync(IdType idType,
//        long key,
//        CancellationToken cancellationToken = default)
//    {
//        if ((byte)idType > 25)
//        {
//            Console.WriteLine($"############ new high value error:{idType}");
//        }

//        try
//        {
//            var client = GrpcClientFactory.CreateIdGeneratorServiceClient(options.Value.IdGeneratorGrpcServiceUrl);
//            var r = await client
//                .GenerateNextHighValueAsync(new GenerateNextHighValueRequest { IdType = (int)idType, IdKey = key },
//                    cancellationToken: cancellationToken)
//                .ResponseAsync;
//            return r.HighValue;
//        }
//        catch (HttpRequestException ex)
//        {
//            Console.WriteLine($"Get next id failed:{ex}");
//            var client = GrpcClientFactory.CreateIdGeneratorServiceClient(options.Value.IdGeneratorGrpcServiceUrl, true);
//            var r = await client
//                .GenerateNextHighValueAsync(new GenerateNextHighValueRequest { IdType = (int)idType, IdKey = key },
//                    cancellationToken: cancellationToken)
//                .ResponseAsync;
//            return r.HighValue;
//        }
//    }
//}
