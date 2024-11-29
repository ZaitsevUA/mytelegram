using EventFlow.ReadStores;

namespace MyTelegram.Messenger.Services.Caching;

public class SingleAggregateCachedReadModelManager<TReadModelInterface, TReadModel>(IReadModelDomainEventApplier readModelDomainEventApplier, IServiceProvider serviceProvider, ILogger<CachedReadModelManager<TReadModelInterface, TReadModel>> logger, IReadModelCacheHelper<TReadModelInterface> readModelCacheHelper) :
    CachedReadModelManager<TReadModelInterface, TReadModel>(readModelDomainEventApplier, serviceProvider, logger, readModelCacheHelper) where TReadModel : class, IReadModel where TReadModelInterface : IReadModel
{
    protected override IEnumerable<string> GetReadModelIds(IDomainEvent domainEvent)
    {
        yield return domainEvent.GetIdentity().Value;
    }
}