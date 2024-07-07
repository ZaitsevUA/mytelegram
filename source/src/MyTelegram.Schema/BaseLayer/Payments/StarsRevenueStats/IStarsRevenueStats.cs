// ReSharper disable All

namespace MyTelegram.Schema.Payments;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/payments.StarsRevenueStats" />
///</summary>
[JsonDerivedType(typeof(TStarsRevenueStats), nameof(TStarsRevenueStats))]
public interface IStarsRevenueStats : IObject
{
    MyTelegram.Schema.IStatsGraph RevenueGraph { get; set; }
    MyTelegram.Schema.IStarsRevenueStatus Status { get; set; }
    double UsdRate { get; set; }
}
