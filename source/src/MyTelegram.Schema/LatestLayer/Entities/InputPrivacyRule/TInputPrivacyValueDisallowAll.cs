﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Disallow all
/// See <a href="https://corefork.telegram.org/constructor/inputPrivacyValueDisallowAll" />
///</summary>
[TlObject(0xd66b66c9)]
public sealed class TInputPrivacyValueDisallowAll : IInputPrivacyRule
{
    public uint ConstructorId => 0xd66b66c9;


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