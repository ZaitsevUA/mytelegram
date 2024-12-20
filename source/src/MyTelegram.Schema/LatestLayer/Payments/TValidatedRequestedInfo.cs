﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema.Payments;

///<summary>
/// Validated user-provided info
/// See <a href="https://corefork.telegram.org/constructor/payments.validatedRequestedInfo" />
///</summary>
[TlObject(0xd1451883)]
public sealed class TValidatedRequestedInfo : IValidatedRequestedInfo
{
    public uint ConstructorId => 0xd1451883;
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    public BitArray Flags { get; set; } = new BitArray(32);

    ///<summary>
    /// ID
    ///</summary>
    public string? Id { get; set; }

    ///<summary>
    /// Shipping options
    ///</summary>
    public TVector<MyTelegram.Schema.IShippingOption>? ShippingOptions { get; set; }

    public void ComputeFlag()
    {
        if (Id != null) { Flags[0] = true; }
        if (ShippingOptions?.Count > 0) { Flags[1] = true; }
    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Flags);
        if (Flags[0]) { writer.Write(Id); }
        if (Flags[1]) { writer.Write(ShippingOptions); }
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Flags = reader.ReadBitArray();
        if (Flags[0]) { Id = reader.ReadString(); }
        if (Flags[1]) { ShippingOptions = reader.Read<TVector<MyTelegram.Schema.IShippingOption>>(); }
    }
}