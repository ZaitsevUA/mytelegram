// ReSharper disable All

namespace MyTelegram.Schema.Payments;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/payments.StarsRevenueAdsAccountUrl" />
///</summary>
[JsonDerivedType(typeof(TStarsRevenueAdsAccountUrl), nameof(TStarsRevenueAdsAccountUrl))]
public interface IStarsRevenueAdsAccountUrl : IObject
{
    string Url { get; set; }
}
