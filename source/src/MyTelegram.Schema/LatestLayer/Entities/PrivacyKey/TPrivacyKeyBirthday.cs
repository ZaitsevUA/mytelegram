﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Whether the user can see our birthday.
/// See <a href="https://corefork.telegram.org/constructor/privacyKeyBirthday" />
///</summary>
[TlObject(0x2000a518)]
public sealed class TPrivacyKeyBirthday : IPrivacyKey
{
    public uint ConstructorId => 0x2000a518;


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