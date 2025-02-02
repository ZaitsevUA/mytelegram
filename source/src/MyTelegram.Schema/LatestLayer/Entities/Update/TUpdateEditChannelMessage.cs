﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// A message was edited in a <a href="https://corefork.telegram.org/api/channel">channel/supergroup</a>
/// See <a href="https://corefork.telegram.org/constructor/updateEditChannelMessage" />
///</summary>
[TlObject(0x1b3f4df7)]
public sealed class TUpdateEditChannelMessage : IUpdate
{
    public uint ConstructorId => 0x1b3f4df7;
    ///<summary>
    /// The new message
    /// See <a href="https://corefork.telegram.org/type/Message" />
    ///</summary>
    public MyTelegram.Schema.IMessage Message { get; set; }

    ///<summary>
    /// <a href="https://corefork.telegram.org/api/updates">Event count after generation</a>
    ///</summary>
    public int Pts { get; set; }

    ///<summary>
    /// <a href="https://corefork.telegram.org/api/updates">Number of events that were generated</a>
    ///</summary>
    public int PtsCount { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Message);
        writer.Write(Pts);
        writer.Write(PtsCount);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Message = reader.Read<MyTelegram.Schema.IMessage>();
        Pts = reader.ReadInt32();
        PtsCount = reader.ReadInt32();
    }
}