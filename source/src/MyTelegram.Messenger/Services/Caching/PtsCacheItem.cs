using Interlocked = System.Threading.Interlocked;

namespace MyTelegram.Messenger.Services.Caching;

public class PtsCacheItem(long ownerUid, int pts = 0, int qts = 0)
{
    private int _unreadCount;

    public long OwnerPeerId { get; private set; } = ownerUid;

    public int Pts => pts;

    public int Qts => qts;
    public int UnreadCount => _unreadCount;
    //public int UnreadCount { get; set; }

    public void IncrementPts()
    {
        Interlocked.Increment(ref pts);
    }

    public void AddUnreadCount(int unreadCount)
    {
        Interlocked.Add(ref _unreadCount, unreadCount);
    }

    public void AddPts(int ptsCount)
    {
        Interlocked.Add(ref pts, ptsCount);
    }

    public void AddQts(int qtsCount)
    {
        Interlocked.Add(ref qts, qtsCount);
    }

    public void IncrementQts()
    {
        Interlocked.Increment(ref qts);
    }
}