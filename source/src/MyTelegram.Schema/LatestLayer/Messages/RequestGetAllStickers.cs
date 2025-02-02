﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema.Messages;

///<summary>
/// Get all installed stickers
/// See <a href="https://corefork.telegram.org/method/messages.getAllStickers" />
///</summary>
[TlObject(0xb8a0a1a8)]
public sealed class RequestGetAllStickers : IRequest<MyTelegram.Schema.Messages.IAllStickers>
{
    public uint ConstructorId => 0xb8a0a1a8;
    ///<summary>
    /// <a href="https://corefork.telegram.org/api/offsets#hash-generation">Hash used for caching, for more info click here</a>.
    ///</summary>
    public long Hash { get; set; }

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
        Hash = reader.ReadInt64();
    }
}
