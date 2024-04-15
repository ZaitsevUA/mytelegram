﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema.Smsjobs;

///<summary>
/// See <a href="https://corefork.telegram.org/method/smsjobs.updateSettings" />
///</summary>
[TlObject(0x93fa0bf)]
public sealed class RequestUpdateSettings : IRequest<IBool>
{
    public uint ConstructorId => 0x93fa0bf;
    public BitArray Flags { get; set; } = new BitArray(32);
    public bool AllowInternational { get; set; }

    public void ComputeFlag()
    {
        if (AllowInternational) { Flags[0] = true; }
    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Flags);

    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Flags = reader.ReadBitArray();
        if (Flags[0]) { AllowInternational = true; }
    }
}
