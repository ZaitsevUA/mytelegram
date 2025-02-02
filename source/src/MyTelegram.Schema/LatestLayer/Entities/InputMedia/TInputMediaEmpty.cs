﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Empty media content of a message.
/// See <a href="https://corefork.telegram.org/constructor/inputMediaEmpty" />
///</summary>
[TlObject(0x9664f57f)]
public sealed class TInputMediaEmpty : IInputMedia,IEmpty
{
    public uint ConstructorId => 0x9664f57f;


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