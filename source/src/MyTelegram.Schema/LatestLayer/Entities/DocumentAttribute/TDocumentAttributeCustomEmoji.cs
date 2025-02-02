﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Info about a custom emoji
/// See <a href="https://corefork.telegram.org/constructor/documentAttributeCustomEmoji" />
///</summary>
[TlObject(0xfd149899)]
public sealed class TDocumentAttributeCustomEmoji : IDocumentAttribute
{
    public uint ConstructorId => 0xfd149899;
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    public BitArray Flags { get; set; } = new BitArray(32);

    ///<summary>
    /// Whether this custom emoji can be sent by non-Premium users
    /// See <a href="https://corefork.telegram.org/type/true" />
    ///</summary>
    public bool Free { get; set; }

    ///<summary>
    /// Whether the color of this TGS custom emoji should be changed to the text color when used in messages, the accent color if used as emoji status, white on chat photos, or another appropriate color based on context.
    /// See <a href="https://corefork.telegram.org/type/true" />
    ///</summary>
    public bool TextColor { get; set; }

    ///<summary>
    /// The actual emoji
    ///</summary>
    public string Alt { get; set; }

    ///<summary>
    /// The emoji stickerset to which this emoji belongs.
    /// See <a href="https://corefork.telegram.org/type/InputStickerSet" />
    ///</summary>
    public MyTelegram.Schema.IInputStickerSet Stickerset { get; set; }

    public void ComputeFlag()
    {
        if (Free) { Flags[0] = true; }
        if (TextColor) { Flags[1] = true; }

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Flags);
        writer.Write(Alt);
        writer.Write(Stickerset);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Flags = reader.ReadBitArray();
        if (Flags[0]) { Free = true; }
        if (Flags[1]) { TextColor = true; }
        Alt = reader.ReadString();
        Stickerset = reader.Read<MyTelegram.Schema.IInputStickerSet>();
    }
}