﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Used to top up the <a href="https://corefork.telegram.org/api/stars">Telegram Stars</a> balance of the current account or someone else's account, or to start a <a href="https://corefork.telegram.org/api/giveaways#star-giveaways">Telegram Star giveaway »</a>.
/// See <a href="https://corefork.telegram.org/constructor/inputInvoiceStars" />
///</summary>
[TlObject(0x65f00ce3)]
public sealed class TInputInvoiceStars : IInputInvoice
{
    public uint ConstructorId => 0x65f00ce3;
    ///<summary>
    /// An <a href="https://corefork.telegram.org/constructor/inputStorePaymentStarsGiveaway">inputStorePaymentStarsGiveaway</a>, <a href="https://corefork.telegram.org/constructor/inputStorePaymentStarsTopup">inputStorePaymentStarsTopup</a> or <a href="https://corefork.telegram.org/constructor/inputStorePaymentStarsGift">inputStorePaymentStarsGift</a>.
    /// See <a href="https://corefork.telegram.org/type/InputStorePaymentPurpose" />
    ///</summary>
    public MyTelegram.Schema.IInputStorePaymentPurpose Purpose { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Purpose);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Purpose = reader.Read<MyTelegram.Schema.IInputStorePaymentPurpose>();
    }
}