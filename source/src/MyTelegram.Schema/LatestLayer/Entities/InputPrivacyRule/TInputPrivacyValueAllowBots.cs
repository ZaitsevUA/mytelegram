﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/inputPrivacyValueAllowBots" />
///</summary>
[TlObject(0x5a4fcce5)]
public sealed class TInputPrivacyValueAllowBots : IInputPrivacyRule
{
    public uint ConstructorId => 0x5a4fcce5;


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