﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema.Messages;

///<summary>
/// See <a href="https://corefork.telegram.org/method/messages.getQuickReplies" />
///</summary>
[TlObject(0xd483f2a8)]
public sealed class RequestGetQuickReplies : IRequest<MyTelegram.Schema.Messages.IQuickReplies>
{
    public uint ConstructorId => 0xd483f2a8;
    public long Hash { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Hash);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Hash = reader.ReadInt64();
    }
}
