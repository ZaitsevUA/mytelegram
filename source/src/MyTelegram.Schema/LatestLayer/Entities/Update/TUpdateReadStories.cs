﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Stories of a specific peer were marked as read.
/// See <a href="https://corefork.telegram.org/constructor/updateReadStories" />
///</summary>
[TlObject(0xf74e932b)]
public sealed class TUpdateReadStories : IUpdate
{
    public uint ConstructorId => 0xf74e932b;
    ///<summary>
    /// The peer
    /// See <a href="https://corefork.telegram.org/type/Peer" />
    ///</summary>
    public MyTelegram.Schema.IPeer Peer { get; set; }

    ///<summary>
    /// ID of the last story that was marked as read
    ///</summary>
    public int MaxId { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Peer);
        writer.Write(MaxId);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Peer = reader.Read<MyTelegram.Schema.IPeer>();
        MaxId = reader.ReadInt32();
    }
}