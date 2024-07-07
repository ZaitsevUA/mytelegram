// ReSharper disable All

namespace MyTelegram.Schema.Payments;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/payments.StarsRevenueWithdrawalUrl" />
///</summary>
[JsonDerivedType(typeof(TStarsRevenueWithdrawalUrl), nameof(TStarsRevenueWithdrawalUrl))]
public interface IStarsRevenueWithdrawalUrl : IObject
{
    string Url { get; set; }
}
