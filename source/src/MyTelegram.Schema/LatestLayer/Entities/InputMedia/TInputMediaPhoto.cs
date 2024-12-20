﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Forwarded photo
/// See <a href="https://corefork.telegram.org/constructor/inputMediaPhoto" />
///</summary>
[TlObject(0xb3ba0635)]
public sealed class TInputMediaPhoto : IInputMedia
{
    public uint ConstructorId => 0xb3ba0635;
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    public BitArray Flags { get; set; } = new BitArray(32);

    ///<summary>
    /// Whether this media should be hidden behind a spoiler warning
    /// See <a href="https://corefork.telegram.org/type/true" />
    ///</summary>
    public bool Spoiler { get; set; }

    ///<summary>
    /// Photo to be forwarded
    /// See <a href="https://corefork.telegram.org/type/InputPhoto" />
    ///</summary>
    public MyTelegram.Schema.IInputPhoto Id { get; set; }

    ///<summary>
    /// Time to live in seconds of self-destructing photo
    ///</summary>
    public int? TtlSeconds { get; set; }

    public void ComputeFlag()
    {
        if (Spoiler) { Flags[1] = true; }
        if (/*TtlSeconds != 0 && */TtlSeconds.HasValue) { Flags[0] = true; }
    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Flags);
        writer.Write(Id);
        if (Flags[0]) { writer.Write(TtlSeconds.Value); }
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Flags = reader.ReadBitArray();
        if (Flags[1]) { Spoiler = true; }
        Id = reader.Read<MyTelegram.Schema.IInputPhoto>();
        if (Flags[0]) { TtlSeconds = reader.ReadInt32(); }
    }
}