﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Whether people can send you voice messages or round videos (<a href="https://corefork.telegram.org/api/premium">Premium</a> users only).
/// See <a href="https://corefork.telegram.org/constructor/inputPrivacyKeyVoiceMessages" />
///</summary>
[TlObject(0xaee69d68)]
public sealed class TInputPrivacyKeyVoiceMessages : IInputPrivacyKey
{
    public uint ConstructorId => 0xaee69d68;


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