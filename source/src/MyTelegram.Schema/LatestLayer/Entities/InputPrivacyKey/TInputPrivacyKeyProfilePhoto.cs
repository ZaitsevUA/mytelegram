﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Whether people will be able to see your profile picture
/// See <a href="https://corefork.telegram.org/constructor/inputPrivacyKeyProfilePhoto" />
///</summary>
[TlObject(0x5719bacc)]
public sealed class TInputPrivacyKeyProfilePhoto : IInputPrivacyKey
{
    public uint ConstructorId => 0x5719bacc;


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