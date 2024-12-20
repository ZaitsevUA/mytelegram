﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// List of peers near you was updated
/// See <a href="https://corefork.telegram.org/constructor/updatePeerLocated" />
///</summary>
[TlObject(0xb4afcfb0)]
public sealed class TUpdatePeerLocated : IUpdate
{
    public uint ConstructorId => 0xb4afcfb0;
    ///<summary>
    /// Geolocated peer list update
    ///</summary>
    public TVector<MyTelegram.Schema.IPeerLocated> Peers { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Peers);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Peers = reader.Read<TVector<MyTelegram.Schema.IPeerLocated>>();
    }
}