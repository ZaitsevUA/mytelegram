﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema.Payments;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/payments.connectedStarRefBots" />
///</summary>
[TlObject(0x98d5ea1d)]
public sealed class TConnectedStarRefBots : IConnectedStarRefBots
{
    public uint ConstructorId => 0x98d5ea1d;
    ///<summary>
    /// &nbsp;
    ///</summary>
    public int Count { get; set; }

    ///<summary>
    /// &nbsp;
    ///</summary>
    public TVector<MyTelegram.Schema.IConnectedBotStarRef> ConnectedBots { get; set; }

    ///<summary>
    /// &nbsp;
    ///</summary>
    public TVector<MyTelegram.Schema.IUser> Users { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Count);
        writer.Write(ConnectedBots);
        writer.Write(Users);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Count = reader.ReadInt32();
        ConnectedBots = reader.Read<TVector<MyTelegram.Schema.IConnectedBotStarRef>>();
        Users = reader.Read<TVector<MyTelegram.Schema.IUser>>();
    }
}