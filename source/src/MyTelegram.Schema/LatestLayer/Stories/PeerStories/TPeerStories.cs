﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema.Stories;

///<summary>
/// <a href="https://corefork.telegram.org/api/stories#watching-stories">Active story list</a> of a specific peer.
/// See <a href="https://corefork.telegram.org/constructor/stories.peerStories" />
///</summary>
[TlObject(0xcae68768)]
public sealed class TPeerStories : IPeerStories
{
    public uint ConstructorId => 0xcae68768;
    ///<summary>
    /// Stories
    /// See <a href="https://corefork.telegram.org/type/PeerStories" />
    ///</summary>
    public MyTelegram.Schema.IPeerStories Stories { get; set; }

    ///<summary>
    /// Mentioned chats
    ///</summary>
    public TVector<MyTelegram.Schema.IChat> Chats { get; set; }

    ///<summary>
    /// Mentioned users
    ///</summary>
    public TVector<MyTelegram.Schema.IUser> Users { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Stories);
        writer.Write(Chats);
        writer.Write(Users);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Stories = reader.Read<MyTelegram.Schema.IPeerStories>();
        Chats = reader.Read<TVector<MyTelegram.Schema.IChat>>();
        Users = reader.Read<TVector<MyTelegram.Schema.IUser>>();
    }
}