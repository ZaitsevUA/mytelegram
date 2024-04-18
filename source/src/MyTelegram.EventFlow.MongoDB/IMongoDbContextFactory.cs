//namespace MyTelegram.EventFlow.MongoDB;

//public interface IMongoDbContextProvider<out TMongoDbContext> where TMongoDbContext : IMongoDbContext // IMongoDatabase
//{
//    TMongoDbContext CreateContext();
//}


namespace MyTelegram.EventFlow.MongoDB;

public interface IMongoDbContextFactory<out TMongoDbContext> where TMongoDbContext : IMongoDbContext
{
    TMongoDbContext CreateContext();
}

public class DefaultMongoDbContextFactory<TMongoDbContext>(TMongoDbContext dbContext)
    : IMongoDbContextFactory<TMongoDbContext>
    where TMongoDbContext : IMongoDbContext
{
    public TMongoDbContext CreateContext() => dbContext;
}