// ReSharper disable All

namespace MyTelegram.Schema.Stats;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/stats.BroadcastRevenueWithdrawalUrl" />
///</summary>
[JsonDerivedType(typeof(TBroadcastRevenueWithdrawalUrl), nameof(TBroadcastRevenueWithdrawalUrl))]
public interface IBroadcastRevenueWithdrawalUrl : IObject
{
    string Url { get; set; }
}
