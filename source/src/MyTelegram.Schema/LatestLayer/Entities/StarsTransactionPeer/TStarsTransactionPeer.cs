﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Describes a <a href="https://corefork.telegram.org/api/stars">Telegram Star</a> transaction with another peer.
/// See <a href="https://corefork.telegram.org/constructor/starsTransactionPeer" />
///</summary>
[TlObject(0xd80da15d)]
public sealed class TStarsTransactionPeer : IStarsTransactionPeer
{
    public uint ConstructorId => 0xd80da15d;
    ///<summary>
    /// The peer.
    /// See <a href="https://corefork.telegram.org/type/Peer" />
    ///</summary>
    public MyTelegram.Schema.IPeer Peer { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Peer);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Peer = reader.Read<MyTelegram.Schema.IPeer>();
    }
}