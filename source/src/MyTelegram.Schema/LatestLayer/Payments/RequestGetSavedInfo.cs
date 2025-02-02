﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema.Payments;

///<summary>
/// Get saved payment information
/// See <a href="https://corefork.telegram.org/method/payments.getSavedInfo" />
///</summary>
[TlObject(0x227d824b)]
public sealed class RequestGetSavedInfo : IRequest<MyTelegram.Schema.Payments.ISavedInfo>
{
    public uint ConstructorId => 0x227d824b;

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
