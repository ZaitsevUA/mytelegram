﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Whether messages forwarded from the user will be <a href="https://telegram.org/blog/unsend-privacy-emoji#anonymous-forwarding">anonymously forwarded</a>
/// See <a href="https://corefork.telegram.org/constructor/privacyKeyForwards" />
///</summary>
[TlObject(0x69ec56a3)]
public sealed class TPrivacyKeyForwards : IPrivacyKey
{
    public uint ConstructorId => 0x69ec56a3;


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