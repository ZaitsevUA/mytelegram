﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Notifies a change of a message <a href="https://corefork.telegram.org/api/drafts">draft</a>.
/// See <a href="https://corefork.telegram.org/constructor/updateDraftMessage" />
///</summary>
[TlObject(0x1b49ec6d)]
public sealed class TUpdateDraftMessage : IUpdate
{
    public uint ConstructorId => 0x1b49ec6d;
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    public BitArray Flags { get; set; } = new BitArray(32);

    ///<summary>
    /// The peer to which the draft is associated
    /// See <a href="https://corefork.telegram.org/type/Peer" />
    ///</summary>
    public MyTelegram.Schema.IPeer Peer { get; set; }

    ///<summary>
    /// ID of the <a href="https://corefork.telegram.org/api/forum#forum-topics">forum topic</a> to which the draft is associated
    ///</summary>
    public int? TopMsgId { get; set; }

    ///<summary>
    /// The draft
    /// See <a href="https://corefork.telegram.org/type/DraftMessage" />
    ///</summary>
    public MyTelegram.Schema.IDraftMessage Draft { get; set; }

    public void ComputeFlag()
    {
        if (/*TopMsgId != 0 && */TopMsgId.HasValue) { Flags[0] = true; }

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Flags);
        writer.Write(Peer);
        if (Flags[0]) { writer.Write(TopMsgId.Value); }
        writer.Write(Draft);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Flags = reader.ReadBitArray();
        Peer = reader.Read<MyTelegram.Schema.IPeer>();
        if (Flags[0]) { TopMsgId = reader.ReadInt32(); }
        Draft = reader.Read<MyTelegram.Schema.IDraftMessage>();
    }
}