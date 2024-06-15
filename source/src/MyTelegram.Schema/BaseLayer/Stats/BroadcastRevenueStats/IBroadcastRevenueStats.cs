// ReSharper disable All

namespace MyTelegram.Schema.Stats;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/stats.BroadcastRevenueStats" />
///</summary>
[JsonDerivedType(typeof(TBroadcastRevenueStats), nameof(TBroadcastRevenueStats))]
public interface IBroadcastRevenueStats : IObject
{
    MyTelegram.Schema.IStatsGraph TopHoursGraph { get; set; }
    MyTelegram.Schema.IStatsGraph RevenueGraph { get; set; }
    MyTelegram.Schema.IBroadcastRevenueBalances Balances { get; set; }
    double UsdRate { get; set; }
}
