// ReSharper disable All

namespace MyTelegram.Schema.Payments;

///<summary>
/// Payment form
/// See <a href="https://corefork.telegram.org/constructor/payments.PaymentForm" />
///</summary>
[JsonDerivedType(typeof(TPaymentForm), nameof(TPaymentForm))]
[JsonDerivedType(typeof(TPaymentFormStars), nameof(TPaymentFormStars))]
public interface IPaymentForm : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// &nbsp;
    ///</summary>
    long FormId { get; set; }

    ///<summary>
    /// &nbsp;
    ///</summary>
    long BotId { get; set; }

    ///<summary>
    /// &nbsp;
    ///</summary>
    string Title { get; set; }

    ///<summary>
    /// &nbsp;
    ///</summary>
    string Description { get; set; }

    ///<summary>
    /// &nbsp;
    /// See <a href="https://corefork.telegram.org/type/WebDocument" />
    ///</summary>
    MyTelegram.Schema.IWebDocument? Photo { get; set; }

    ///<summary>
    /// &nbsp;
    /// See <a href="https://corefork.telegram.org/type/Invoice" />
    ///</summary>
    MyTelegram.Schema.IInvoice Invoice { get; set; }

    ///<summary>
    /// &nbsp;
    /// See <a href="https://corefork.telegram.org/type/User" />
    ///</summary>
    TVector<MyTelegram.Schema.IUser> Users { get; set; }
}
