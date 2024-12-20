﻿// ReSharper disable All

namespace MyTelegram.Schema.Payments;

///<summary>
/// Payment receipt
/// See <a href="https://corefork.telegram.org/constructor/payments.PaymentReceipt" />
///</summary>
[JsonDerivedType(typeof(TPaymentReceipt), nameof(TPaymentReceipt))]
[JsonDerivedType(typeof(TPaymentReceiptStars), nameof(TPaymentReceiptStars))]
public interface IPaymentReceipt : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// Date of generation
    ///</summary>
    int Date { get; set; }

    ///<summary>
    /// Bot ID
    ///</summary>
    long BotId { get; set; }

    ///<summary>
    /// Title
    ///</summary>
    string Title { get; set; }

    ///<summary>
    /// Description
    ///</summary>
    string Description { get; set; }

    ///<summary>
    /// Product photo
    /// See <a href="https://corefork.telegram.org/type/WebDocument" />
    ///</summary>
    MyTelegram.Schema.IWebDocument? Photo { get; set; }

    ///<summary>
    /// Invoice
    /// See <a href="https://corefork.telegram.org/type/Invoice" />
    ///</summary>
    MyTelegram.Schema.IInvoice Invoice { get; set; }

    ///<summary>
    /// Currency, always <code>XTR</code>.
    ///</summary>
    string Currency { get; set; }

    ///<summary>
    /// Amount of <a href="https://corefork.telegram.org/api/stars">Telegram Stars</a>.
    ///</summary>
    long TotalAmount { get; set; }

    ///<summary>
    /// Info about users mentioned in the other fields.
    /// See <a href="https://corefork.telegram.org/type/User" />
    ///</summary>
    TVector<MyTelegram.Schema.IUser> Users { get; set; }
}
