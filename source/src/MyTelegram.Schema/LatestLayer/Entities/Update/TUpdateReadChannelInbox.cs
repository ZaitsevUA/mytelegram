﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Incoming messages in a <a href="https://corefork.telegram.org/api/channel">channel/supergroup</a> were read
/// See <a href="https://corefork.telegram.org/constructor/updateReadChannelInbox" />
///</summary>
[TlObject(0x922e6e10)]
public sealed class TUpdateReadChannelInbox : IUpdate
{
    public uint ConstructorId => 0x922e6e10;
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    public BitArray Flags { get; set; } = new BitArray(32);

    ///<summary>
    /// <a href="https://corefork.telegram.org/api/folders#peer-folders">Peer folder ID, for more info click here</a>
    ///</summary>
    public int? FolderId { get; set; }

    ///<summary>
    /// Channel/supergroup ID
    ///</summary>
    public long ChannelId { get; set; }

    ///<summary>
    /// Position up to which all incoming messages are read.
    ///</summary>
    public int MaxId { get; set; }

    ///<summary>
    /// Count of messages weren't read yet
    ///</summary>
    public int StillUnreadCount { get; set; }

    ///<summary>
    /// <a href="https://corefork.telegram.org/api/updates">Event count after generation</a>
    ///</summary>
    public int Pts { get; set; }

    public void ComputeFlag()
    {
        if (/*FolderId != 0 && */FolderId.HasValue) { Flags[0] = true; }

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Flags);
        if (Flags[0]) { writer.Write(FolderId.Value); }
        writer.Write(ChannelId);
        writer.Write(MaxId);
        writer.Write(StillUnreadCount);
        writer.Write(Pts);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Flags = reader.ReadBitArray();
        if (Flags[0]) { FolderId = reader.ReadInt32(); }
        ChannelId = reader.ReadInt64();
        MaxId = reader.ReadInt32();
        StillUnreadCount = reader.ReadInt32();
        Pts = reader.ReadInt32();
    }
}