// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/StarsSubscriptionPricing" />
///</summary>
[JsonDerivedType(typeof(TStarsSubscriptionPricing), nameof(TStarsSubscriptionPricing))]
public interface IStarsSubscriptionPricing : IObject
{
    int Period { get; set; }
    long Amount { get; set; }
}
