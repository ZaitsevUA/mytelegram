namespace MyTelegram.Messenger.Services.Caching;

public interface IReadModelCacheHelper<TReadModel>
{
    Task<TReadModel?> GetOrCreateAsync(long readModelId, Func<Task<TReadModel?>> createFactory, Func<TReadModel, string> createReadModelIdFunc);
    bool TryGetReadModel(long readModelId, [NotNullWhen(true)] out TReadModel? readModel);
    bool TryGetReadModel(string readModelId, [NotNullWhen(true)] out TReadModel? readModel);
    void Add(long id, string readModelId, TReadModel readModel);
    void Remove(string readModelId);
}