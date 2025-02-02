﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// A poll was stopped
/// See <a href="https://corefork.telegram.org/constructor/channelAdminLogEventActionStopPoll" />
///</summary>
[TlObject(0x8f079643)]
public sealed class TChannelAdminLogEventActionStopPoll : IChannelAdminLogEventAction
{
    public uint ConstructorId => 0x8f079643;
    ///<summary>
    /// The poll that was stopped
    /// See <a href="https://corefork.telegram.org/type/Message" />
    ///</summary>
    public MyTelegram.Schema.IMessage Message { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Message);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Message = reader.Read<MyTelegram.Schema.IMessage>();
    }
}