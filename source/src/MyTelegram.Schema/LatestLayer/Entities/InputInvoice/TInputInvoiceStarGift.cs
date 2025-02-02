﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Used to buy a <a href="https://corefork.telegram.org/api/gifts">Telegram Star Gift, see here »</a> for more info.
/// See <a href="https://corefork.telegram.org/constructor/inputInvoiceStarGift" />
///</summary>
[TlObject(0x25d8c1d8)]
public sealed class TInputInvoiceStarGift : IInputInvoice
{
    public uint ConstructorId => 0x25d8c1d8;
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    public BitArray Flags { get; set; } = new BitArray(32);

    ///<summary>
    /// If set, your name will be hidden if the destination user decides to display the gift on their profile (they will still see that you sent the gift)
    /// See <a href="https://corefork.telegram.org/type/true" />
    ///</summary>
    public bool HideName { get; set; }

    ///<summary>
    /// Identifier of the user that will receive the gift
    /// See <a href="https://corefork.telegram.org/type/InputUser" />
    ///</summary>
    public MyTelegram.Schema.IInputUser UserId { get; set; }

    ///<summary>
    /// Identifier of the gift, from <a href="https://corefork.telegram.org/constructor/starGift">starGift</a>.<code>id</code>
    ///</summary>
    public long GiftId { get; set; }

    ///<summary>
    /// Optional message, attached with the gift. <br>The maximum length for this field is specified in the <a href="https://corefork.telegram.org/api/config#stargifts-message-length-max">stargifts_message_length_max client configuration value »</a>.
    /// See <a href="https://corefork.telegram.org/type/TextWithEntities" />
    ///</summary>
    public MyTelegram.Schema.ITextWithEntities? Message { get; set; }

    public void ComputeFlag()
    {
        if (HideName) { Flags[0] = true; }
        if (Message != null) { Flags[1] = true; }
    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Flags);
        writer.Write(UserId);
        writer.Write(GiftId);
        if (Flags[1]) { writer.Write(Message); }
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Flags = reader.ReadBitArray();
        if (Flags[0]) { HideName = true; }
        UserId = reader.Read<MyTelegram.Schema.IInputUser>();
        GiftId = reader.ReadInt64();
        if (Flags[1]) { Message = reader.Read<MyTelegram.Schema.ITextWithEntities>(); }
    }
}