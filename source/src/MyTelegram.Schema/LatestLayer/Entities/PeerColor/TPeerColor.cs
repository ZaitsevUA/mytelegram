﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Represents a <a href="https://corefork.telegram.org/api/colors">color palette »</a>.
/// See <a href="https://corefork.telegram.org/constructor/peerColor" />
///</summary>
[TlObject(0xb54b5acf)]
public sealed class TPeerColor : IPeerColor
{
    public uint ConstructorId => 0xb54b5acf;
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    public BitArray Flags { get; set; } = new BitArray(32);

    ///<summary>
    /// <a href="https://corefork.telegram.org/api/colors">Color palette ID, see here »</a> for more info; if not set, the default palette should be used.
    ///</summary>
    public int? Color { get; set; }

    ///<summary>
    /// Optional <a href="https://corefork.telegram.org/api/custom-emoji">custom emoji ID</a> used to generate the pattern.
    ///</summary>
    public long? BackgroundEmojiId { get; set; }

    public void ComputeFlag()
    {
        if (/*Color != 0 && */Color.HasValue) { Flags[0] = true; }
        if (/*BackgroundEmojiId != 0 &&*/ BackgroundEmojiId.HasValue) { Flags[1] = true; }
    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Flags);
        if (Flags[0]) { writer.Write(Color.Value); }
        if (Flags[1]) { writer.Write(BackgroundEmojiId.Value); }
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Flags = reader.ReadBitArray();
        if (Flags[0]) { Color = reader.ReadInt32(); }
        if (Flags[1]) { BackgroundEmojiId = reader.ReadInt64(); }
    }
}