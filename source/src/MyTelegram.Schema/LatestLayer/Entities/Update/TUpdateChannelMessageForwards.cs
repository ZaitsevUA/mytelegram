﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// The forward counter of a message in a channel has changed
/// See <a href="https://corefork.telegram.org/constructor/updateChannelMessageForwards" />
///</summary>
[TlObject(0xd29a27f4)]
public sealed class TUpdateChannelMessageForwards : IUpdate
{
    public uint ConstructorId => 0xd29a27f4;
    ///<summary>
    /// Channel ID
    ///</summary>
    public long ChannelId { get; set; }

    ///<summary>
    /// ID of the message
    ///</summary>
    public int Id { get; set; }

    ///<summary>
    /// New forward counter
    ///</summary>
    public int Forwards { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(ChannelId);
        writer.Write(Id);
        writer.Write(Forwards);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        ChannelId = reader.ReadInt64();
        Id = reader.ReadInt32();
        Forwards = reader.ReadInt32();
    }
}