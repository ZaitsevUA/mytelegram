// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/BroadcastRevenueTransaction" />
///</summary>
[JsonDerivedType(typeof(TBroadcastRevenueTransactionProceeds), nameof(TBroadcastRevenueTransactionProceeds))]
[JsonDerivedType(typeof(TBroadcastRevenueTransactionWithdrawal), nameof(TBroadcastRevenueTransactionWithdrawal))]
[JsonDerivedType(typeof(TBroadcastRevenueTransactionRefund), nameof(TBroadcastRevenueTransactionRefund))]
public interface IBroadcastRevenueTransaction : IObject
{
    long Amount { get; set; }
}
