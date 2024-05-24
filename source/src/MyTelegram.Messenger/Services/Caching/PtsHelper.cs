namespace MyTelegram.Messenger.Services.Caching;

public class PtsHelper : IPtsHelper
{
    private readonly ConcurrentDictionary<long, PtsCacheItem> _ownerToPtsDict = new();

    public int GetCachedPts(long ownerId)
    {
        if (_ownerToPtsDict.TryGetValue(ownerId, out var cacheItem))
        {
            return cacheItem.Pts;
        }

        return MyTelegramServerDomainConsts.PtsInitId;
    }

    public Task<int> IncrementPtsAsync(long ownerId, int currentPts, int ptsCount = 1, long permAuthKeyId = 0, int newUnreadCount = 0)
    {
        if (_ownerToPtsDict.TryGetValue(ownerId, out var cacheItem))
        {
            if (ptsCount == 1)
            {
                cacheItem.IncrementPts();
            }
            else
            {
                cacheItem.AddPts(ptsCount);
            }

            if (cacheItem.Pts < currentPts)
            {
                cacheItem.AddPts(currentPts - cacheItem.Pts);
            }
        }
        else
        {
            cacheItem = new PtsCacheItem(ownerId, currentPts);
            _ownerToPtsDict.TryAdd(ownerId, cacheItem);
        }

        if (newUnreadCount != 0)
        {
            cacheItem.AddUnreadCount(newUnreadCount);
        }

        return Task.FromResult(cacheItem.Pts);
    }

    public Task<int> IncrementQtsAsync(long ownerId, int currentQts, int qtsCount = 1, long permAuthKeyId = 0)
    {
        if (_ownerToPtsDict.TryGetValue(ownerId, out var cacheItem))
        {
            if (qtsCount == 1)
            {
                cacheItem.IncrementQts();
            }
            else
            {
                cacheItem.AddQts(qtsCount);
            }

            if (cacheItem.Qts < currentQts)
            {
                cacheItem.AddQts(currentQts - cacheItem.Qts);
            }
        }
        else
        {
            cacheItem = new PtsCacheItem(ownerId, currentQts);
            _ownerToPtsDict.TryAdd(ownerId, cacheItem);
        }

        return Task.FromResult(cacheItem.Qts);
    }

    public Task SyncCachedPtsToReadModelAsync(long ownerId)
    {
        if (_ownerToPtsDict.TryGetValue(ownerId, out _))
        {
            //var command = new UpdatePtsCommand(PtsId.Create(ownerId), ownerId, cacheItem.Pts);
            //await _commandBus.PublishAsync(command, CancellationToken.None);
        }

        return Task.CompletedTask;
    }
}