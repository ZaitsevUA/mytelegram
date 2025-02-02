﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Whether messages forwarded from you will be <a href="https://telegram.org/blog/unsend-privacy-emoji#anonymous-forwarding">anonymous</a>
/// See <a href="https://corefork.telegram.org/constructor/inputPrivacyKeyForwards" />
///</summary>
[TlObject(0xa4dd4c08)]
public sealed class TInputPrivacyKeyForwards : IInputPrivacyKey
{
    public uint ConstructorId => 0xa4dd4c08;


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