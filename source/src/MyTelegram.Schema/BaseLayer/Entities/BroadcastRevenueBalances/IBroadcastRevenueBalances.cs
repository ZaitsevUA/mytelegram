// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/BroadcastRevenueBalances" />
///</summary>
[JsonDerivedType(typeof(TBroadcastRevenueBalances), nameof(TBroadcastRevenueBalances))]
public interface IBroadcastRevenueBalances : IObject
{
    long CurrentBalance { get; set; }
    long AvailableBalance { get; set; }
    long OverallRevenue { get; set; }
}
