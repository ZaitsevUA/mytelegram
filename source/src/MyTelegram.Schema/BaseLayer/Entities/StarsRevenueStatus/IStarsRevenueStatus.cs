// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/StarsRevenueStatus" />
///</summary>
[JsonDerivedType(typeof(TStarsRevenueStatus), nameof(TStarsRevenueStatus))]
public interface IStarsRevenueStatus : IObject
{
    BitArray Flags { get; set; }
    bool WithdrawalEnabled { get; set; }
    long CurrentBalance { get; set; }
    long AvailableBalance { get; set; }
    long OverallRevenue { get; set; }
    int? NextWithdrawalAt { get; set; }
}
