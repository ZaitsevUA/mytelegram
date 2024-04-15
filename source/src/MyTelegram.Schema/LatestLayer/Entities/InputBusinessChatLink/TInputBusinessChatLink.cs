﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/inputBusinessChatLink" />
///</summary>
[TlObject(0x11679fa7)]
public sealed class TInputBusinessChatLink : IInputBusinessChatLink
{
    public uint ConstructorId => 0x11679fa7;
    public BitArray Flags { get; set; } = new BitArray(32);
    public string Message { get; set; }
    public TVector<MyTelegram.Schema.IMessageEntity>? Entities { get; set; }
    public string? Title { get; set; }

    public void ComputeFlag()
    {
        if (Entities?.Count > 0) { Flags[0] = true; }
        if (Title != null) { Flags[1] = true; }
    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Flags);
        writer.Write(Message);
        if (Flags[0]) { writer.Write(Entities); }
        if (Flags[1]) { writer.Write(Title); }
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Flags = reader.ReadBitArray();
        Message = reader.ReadString();
        if (Flags[0]) { Entities = reader.Read<TVector<MyTelegram.Schema.IMessageEntity>>(); }
        if (Flags[1]) { Title = reader.ReadString(); }
    }
}