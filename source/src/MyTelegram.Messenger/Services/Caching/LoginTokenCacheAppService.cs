namespace MyTelegram.Messenger.Services.Caching;

public class LoginTokenCacheAppService(IInMemoryRepository<CacheLoginToken, long> inMemoryRepository)
    : ILoginTokenCacheAppService //, ISingletonDependency
{
    public void AddLoginSuccessAuthKeyIdToCache(long authKeyId,
        long userId)
    {
        inMemoryRepository.Insert(authKeyId, new CacheLoginToken(authKeyId, userId));
    }

    public bool TryGetCachedLoginInfo(long authKeyId,
        [NotNullWhen(true)] out CacheLoginToken? loginTokenCache)
    {
        loginTokenCache = inMemoryRepository.Find(authKeyId);

        return loginTokenCache != null;
    }

    public bool TryRemoveLoginInfo(long authKeyId,
        [NotNullWhen(true)] out CacheLoginToken? loginTokenCache)
    {
        return inMemoryRepository.TryDelete(authKeyId, out loginTokenCache);
    }
}