// ReSharper disable All

namespace MyTelegram.Schema.Stats;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/stats.BroadcastRevenueTransactions" />
///</summary>
[JsonDerivedType(typeof(TBroadcastRevenueTransactions), nameof(TBroadcastRevenueTransactions))]
public interface IBroadcastRevenueTransactions : IObject
{
    int Count { get; set; }
    TVector<MyTelegram.Schema.IBroadcastRevenueTransaction> Transactions { get; set; }
}
