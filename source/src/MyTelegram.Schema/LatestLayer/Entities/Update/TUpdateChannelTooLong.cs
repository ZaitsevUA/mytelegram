﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// There are new updates in the specified channel, the client must fetch them.<br>
/// If the difference is too long or if the channel isn't currently in the states, start fetching from the specified pts.
/// See <a href="https://corefork.telegram.org/constructor/updateChannelTooLong" />
///</summary>
[TlObject(0x108d941f)]
public sealed class TUpdateChannelTooLong : IUpdate
{
    public uint ConstructorId => 0x108d941f;
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    public BitArray Flags { get; set; } = new BitArray(32);

    ///<summary>
    /// The channel
    ///</summary>
    public long ChannelId { get; set; }

    ///<summary>
    /// The <a href="https://corefork.telegram.org/api/updates">PTS</a>.
    ///</summary>
    public int? Pts { get; set; }

    public void ComputeFlag()
    {
        if (/*Pts != 0 && */Pts.HasValue) { Flags[0] = true; }
    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Flags);
        writer.Write(ChannelId);
        if (Flags[0]) { writer.Write(Pts.Value); }
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Flags = reader.ReadBitArray();
        ChannelId = reader.ReadInt64();
        if (Flags[0]) { Pts = reader.ReadInt32(); }
    }
}