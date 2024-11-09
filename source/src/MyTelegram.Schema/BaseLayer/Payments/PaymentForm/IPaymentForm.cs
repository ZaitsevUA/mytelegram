// ReSharper disable All

namespace MyTelegram.Schema.Payments;

///<summary>
/// Payment form
/// See <a href="https://corefork.telegram.org/constructor/payments.PaymentForm" />
///</summary>
[JsonDerivedType(typeof(TPaymentForm), nameof(TPaymentForm))]
[JsonDerivedType(typeof(TPaymentFormStars), nameof(TPaymentFormStars))]
[JsonDerivedType(typeof(TPaymentFormStarGift), nameof(TPaymentFormStarGift))]
public interface IPaymentForm : IObject
{
    long FormId { get; set; }
    MyTelegram.Schema.IInvoice Invoice { get; set; }
}
