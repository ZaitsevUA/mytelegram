// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Info about a Telegram Premium purchase
/// See <a href="https://corefork.telegram.org/constructor/InputStorePaymentPurpose" />
///</summary>
[JsonDerivedType(typeof(TInputStorePaymentPremiumSubscription), nameof(TInputStorePaymentPremiumSubscription))]
[JsonDerivedType(typeof(TInputStorePaymentGiftPremium), nameof(TInputStorePaymentGiftPremium))]
[JsonDerivedType(typeof(TInputStorePaymentPremiumGiftCode), nameof(TInputStorePaymentPremiumGiftCode))]
[JsonDerivedType(typeof(TInputStorePaymentPremiumGiveaway), nameof(TInputStorePaymentPremiumGiveaway))]
[JsonDerivedType(typeof(TInputStorePaymentStars), nameof(TInputStorePaymentStars))]
public interface IInputStorePaymentPurpose : IObject
{

}
