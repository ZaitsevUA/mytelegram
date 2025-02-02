﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema.Payments;

///<summary>
/// Submit requested order information for validation
/// <para>Possible errors</para>
/// Code Type Description
/// 400 MESSAGE_ID_INVALID The provided message id is invalid.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// See <a href="https://corefork.telegram.org/method/payments.validateRequestedInfo" />
///</summary>
[TlObject(0xb6c8f12b)]
public sealed class RequestValidateRequestedInfo : IRequest<MyTelegram.Schema.Payments.IValidatedRequestedInfo>
{
    public uint ConstructorId => 0xb6c8f12b;
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    public BitArray Flags { get; set; } = new BitArray(32);

    ///<summary>
    /// Save order information to re-use it for future orders
    /// See <a href="https://corefork.telegram.org/type/true" />
    ///</summary>
    public bool Save { get; set; }

    ///<summary>
    /// Invoice
    /// See <a href="https://corefork.telegram.org/type/InputInvoice" />
    ///</summary>
    public MyTelegram.Schema.IInputInvoice Invoice { get; set; }

    ///<summary>
    /// Requested order information
    /// See <a href="https://corefork.telegram.org/type/PaymentRequestedInfo" />
    ///</summary>
    public MyTelegram.Schema.IPaymentRequestedInfo Info { get; set; }

    public void ComputeFlag()
    {
        if (Save) { Flags[0] = true; }

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Flags);
        writer.Write(Invoice);
        writer.Write(Info);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Flags = reader.ReadBitArray();
        if (Flags[0]) { Save = true; }
        Invoice = reader.Read<MyTelegram.Schema.IInputInvoice>();
        Info = reader.Read<MyTelegram.Schema.IPaymentRequestedInfo>();
    }
}
