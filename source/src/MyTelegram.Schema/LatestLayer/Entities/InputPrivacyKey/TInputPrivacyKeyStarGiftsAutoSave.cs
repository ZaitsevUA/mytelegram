﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/inputPrivacyKeyStarGiftsAutoSave" />
///</summary>
[TlObject(0xe1732341)]
public sealed class TInputPrivacyKeyStarGiftsAutoSave : IInputPrivacyKey
{
    public uint ConstructorId => 0xe1732341;


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