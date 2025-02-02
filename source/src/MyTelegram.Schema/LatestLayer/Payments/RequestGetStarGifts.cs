﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema.Payments;

///<summary>
/// Get a list of available <a href="https://corefork.telegram.org/api/gifts">gifts, see here »</a> for more info.
/// See <a href="https://corefork.telegram.org/method/payments.getStarGifts" />
///</summary>
[TlObject(0xc4563590)]
public sealed class RequestGetStarGifts : IRequest<MyTelegram.Schema.Payments.IStarGifts>
{
    public uint ConstructorId => 0xc4563590;
    ///<summary>
    /// <a href="https://corefork.telegram.org/api/offsets#hash-generation">Hash used for caching, for more info click here</a>.
    ///</summary>
    public int Hash { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Hash);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Hash = reader.ReadInt32();
    }
}
