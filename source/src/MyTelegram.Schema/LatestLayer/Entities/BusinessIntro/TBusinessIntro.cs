﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/businessIntro" />
///</summary>
[TlObject(0x5a0a066d)]
public sealed class TBusinessIntro : IBusinessIntro
{
    public uint ConstructorId => 0x5a0a066d;
    public BitArray Flags { get; set; } = new BitArray(32);
    public string Title { get; set; }
    public string Description { get; set; }
    public MyTelegram.Schema.IDocument? Sticker { get; set; }

    public void ComputeFlag()
    {
        if (Sticker != null) { Flags[0] = true; }
    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Flags);
        writer.Write(Title);
        writer.Write(Description);
        if (Flags[0]) { writer.Write(Sticker); }
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Flags = reader.ReadBitArray();
        Title = reader.ReadString();
        Description = reader.ReadString();
        if (Flags[0]) { Sticker = reader.Read<MyTelegram.Schema.IDocument>(); }
    }
}