﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// A login token (for login via QR code) was accepted.
/// See <a href="https://corefork.telegram.org/constructor/updateLoginToken" />
///</summary>
[TlObject(0x564fe691)]
public sealed class TUpdateLoginToken : IUpdate
{
    public uint ConstructorId => 0x564fe691;


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