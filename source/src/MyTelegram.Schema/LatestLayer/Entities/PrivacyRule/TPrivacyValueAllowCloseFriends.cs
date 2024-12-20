﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Allow only <a href="https://corefork.telegram.org/api/privacy">close friends »</a>
/// See <a href="https://corefork.telegram.org/constructor/privacyValueAllowCloseFriends" />
///</summary>
[TlObject(0xf7e8d89b)]
public sealed class TPrivacyValueAllowCloseFriends : IPrivacyRule
{
    public uint ConstructorId => 0xf7e8d89b;


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