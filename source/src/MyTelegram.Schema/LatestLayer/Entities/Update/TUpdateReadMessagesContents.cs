﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Contents of messages in the common <a href="https://corefork.telegram.org/api/updates">message box</a> were read
/// See <a href="https://corefork.telegram.org/constructor/updateReadMessagesContents" />
///</summary>
[TlObject(0xf8227181)]
public sealed class TUpdateReadMessagesContents : IUpdate
{
    public uint ConstructorId => 0xf8227181;
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    public BitArray Flags { get; set; } = new BitArray(32);

    ///<summary>
    /// IDs of read messages
    ///</summary>
    public TVector<int> Messages { get; set; }

    ///<summary>
    /// <a href="https://corefork.telegram.org/api/updates">Event count after generation</a>
    ///</summary>
    public int Pts { get; set; }

    ///<summary>
    /// <a href="https://corefork.telegram.org/api/updates">Number of events that were generated</a>
    ///</summary>
    public int PtsCount { get; set; }

    ///<summary>
    /// When was the last message in <code>messages</code> marked as read.
    ///</summary>
    public int? Date { get; set; }

    public void ComputeFlag()
    {
        if (/*Date != 0 && */Date.HasValue) { Flags[0] = true; }
    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Flags);
        writer.Write(Messages);
        writer.Write(Pts);
        writer.Write(PtsCount);
        if (Flags[0]) { writer.Write(Date.Value); }
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Flags = reader.ReadBitArray();
        Messages = reader.Read<TVector<int>>();
        Pts = reader.ReadInt32();
        PtsCount = reader.ReadInt32();
        if (Flags[0]) { Date = reader.ReadInt32(); }
    }
}