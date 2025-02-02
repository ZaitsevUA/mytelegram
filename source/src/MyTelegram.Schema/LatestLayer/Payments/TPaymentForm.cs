﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema.Payments;

///<summary>
/// Payment form
/// See <a href="https://corefork.telegram.org/constructor/payments.paymentForm" />
///</summary>
[TlObject(0xa0058751)]
public sealed class TPaymentForm : IPaymentForm
{
    public uint ConstructorId => 0xa0058751;
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    public BitArray Flags { get; set; } = new BitArray(32);

    ///<summary>
    /// Whether the user can choose to save credentials.
    /// See <a href="https://corefork.telegram.org/type/true" />
    ///</summary>
    public bool CanSaveCredentials { get; set; }

    ///<summary>
    /// Indicates that the user can save payment credentials, but only after setting up a <a href="https://corefork.telegram.org/api/srp">2FA password</a> (currently the account doesn't have a <a href="https://corefork.telegram.org/api/srp">2FA password</a>)
    /// See <a href="https://corefork.telegram.org/type/true" />
    ///</summary>
    public bool PasswordMissing { get; set; }

    ///<summary>
    /// Form ID
    ///</summary>
    public long FormId { get; set; }

    ///<summary>
    /// Bot ID
    ///</summary>
    public long BotId { get; set; }

    ///<summary>
    /// Form title
    ///</summary>
    public string Title { get; set; }

    ///<summary>
    /// Description
    ///</summary>
    public string Description { get; set; }

    ///<summary>
    /// Product photo
    /// See <a href="https://corefork.telegram.org/type/WebDocument" />
    ///</summary>
    public MyTelegram.Schema.IWebDocument? Photo { get; set; }

    ///<summary>
    /// Invoice
    /// See <a href="https://corefork.telegram.org/type/Invoice" />
    ///</summary>
    public MyTelegram.Schema.IInvoice Invoice { get; set; }

    ///<summary>
    /// Payment provider ID.
    ///</summary>
    public long ProviderId { get; set; }

    ///<summary>
    /// Payment form URL
    ///</summary>
    public string Url { get; set; }

    ///<summary>
    /// Payment provider name.<br>One of the following:<br>- <code>stripe</code>
    ///</summary>
    public string? NativeProvider { get; set; }

    ///<summary>
    /// Contains information about the payment provider, if available, to support it natively without the need for opening the URL.<br>A JSON object that can contain the following fields:<br><br>- <code>apple_pay_merchant_id</code>: Apple Pay merchant ID<br>- <code>google_pay_public_key</code>: Google Pay public key<br>- <code>need_country</code>: True, if the user country must be provided,<br>- <code>need_zip</code>: True, if the user ZIP/postal code must be provided,<br>- <code>need_cardholder_name</code>: True, if the cardholder name must be provided<br>
    /// See <a href="https://corefork.telegram.org/type/DataJSON" />
    ///</summary>
    public MyTelegram.Schema.IDataJSON? NativeParams { get; set; }

    ///<summary>
    /// Additional payment methods
    ///</summary>
    public TVector<MyTelegram.Schema.IPaymentFormMethod>? AdditionalMethods { get; set; }

    ///<summary>
    /// Saved server-side order information
    /// See <a href="https://corefork.telegram.org/type/PaymentRequestedInfo" />
    ///</summary>
    public MyTelegram.Schema.IPaymentRequestedInfo? SavedInfo { get; set; }

    ///<summary>
    /// Contains information about saved card credentials
    ///</summary>
    public TVector<MyTelegram.Schema.IPaymentSavedCredentials>? SavedCredentials { get; set; }

    ///<summary>
    /// Users
    ///</summary>
    public TVector<MyTelegram.Schema.IUser> Users { get; set; }

    public void ComputeFlag()
    {
        if (CanSaveCredentials) { Flags[2] = true; }
        if (PasswordMissing) { Flags[3] = true; }
        if (Photo != null) { Flags[5] = true; }
        if (NativeProvider != null) { Flags[4] = true; }
        if (NativeParams != null) { Flags[4] = true; }
        if (AdditionalMethods?.Count > 0) { Flags[6] = true; }
        if (SavedInfo != null) { Flags[0] = true; }
        if (SavedCredentials?.Count > 0) { Flags[1] = true; }

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Flags);
        writer.Write(FormId);
        writer.Write(BotId);
        writer.Write(Title);
        writer.Write(Description);
        if (Flags[5]) { writer.Write(Photo); }
        writer.Write(Invoice);
        writer.Write(ProviderId);
        writer.Write(Url);
        if (Flags[4]) { writer.Write(NativeProvider); }
        if (Flags[4]) { writer.Write(NativeParams); }
        if (Flags[6]) { writer.Write(AdditionalMethods); }
        if (Flags[0]) { writer.Write(SavedInfo); }
        if (Flags[1]) { writer.Write(SavedCredentials); }
        writer.Write(Users);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Flags = reader.ReadBitArray();
        if (Flags[2]) { CanSaveCredentials = true; }
        if (Flags[3]) { PasswordMissing = true; }
        FormId = reader.ReadInt64();
        BotId = reader.ReadInt64();
        Title = reader.ReadString();
        Description = reader.ReadString();
        if (Flags[5]) { Photo = reader.Read<MyTelegram.Schema.IWebDocument>(); }
        Invoice = reader.Read<MyTelegram.Schema.IInvoice>();
        ProviderId = reader.ReadInt64();
        Url = reader.ReadString();
        if (Flags[4]) { NativeProvider = reader.ReadString(); }
        if (Flags[4]) { NativeParams = reader.Read<MyTelegram.Schema.IDataJSON>(); }
        if (Flags[6]) { AdditionalMethods = reader.Read<TVector<MyTelegram.Schema.IPaymentFormMethod>>(); }
        if (Flags[0]) { SavedInfo = reader.Read<MyTelegram.Schema.IPaymentRequestedInfo>(); }
        if (Flags[1]) { SavedCredentials = reader.Read<TVector<MyTelegram.Schema.IPaymentSavedCredentials>>(); }
        Users = reader.Read<TVector<MyTelegram.Schema.IUser>>();
    }
}