﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Group profile photo removed.
/// See <a href="https://corefork.telegram.org/constructor/messageActionChatDeletePhoto" />
///</summary>
[TlObject(0x95e3fbef)]
public sealed class TMessageActionChatDeletePhoto : IMessageAction
{
    public uint ConstructorId => 0x95e3fbef;


    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);

    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {

    }
}