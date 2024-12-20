﻿// ReSharper disable All

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
    ///<summary>
    /// Form ID.
    ///</summary>
    long FormId { get; set; }

    ///<summary>
    /// Invoice
    /// See <a href="https://corefork.telegram.org/type/Invoice" />
    ///</summary>
    MyTelegram.Schema.IInvoice Invoice { get; set; }
}
