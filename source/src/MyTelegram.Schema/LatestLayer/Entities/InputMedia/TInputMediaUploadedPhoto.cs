﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Photo
/// See <a href="https://corefork.telegram.org/constructor/inputMediaUploadedPhoto" />
///</summary>
[TlObject(0x1e287d04)]
public sealed class TInputMediaUploadedPhoto : IInputMedia
{
    public uint ConstructorId => 0x1e287d04;
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
    /// The <a href="https://corefork.telegram.org/api/files">uploaded file</a>
    /// See <a href="https://corefork.telegram.org/type/InputFile" />
    ///</summary>
    public MyTelegram.Schema.IInputFile File { get; set; }

    ///<summary>
    /// Attached mask stickers
    ///</summary>
    public TVector<MyTelegram.Schema.IInputDocument>? Stickers { get; set; }

    ///<summary>
    /// Time to live in seconds of self-destructing photo
    ///</summary>
    public int? TtlSeconds { get; set; }

    public void ComputeFlag()
    {
        if (Spoiler) { Flags[2] = true; }
        if (Stickers?.Count > 0) { Flags[0] = true; }
        if (/*TtlSeconds != 0 && */TtlSeconds.HasValue) { Flags[1] = true; }
    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Flags);
        writer.Write(File);
        if (Flags[0]) { writer.Write(Stickers); }
        if (Flags[1]) { writer.Write(TtlSeconds.Value); }
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Flags = reader.ReadBitArray();
        if (Flags[2]) { Spoiler = true; }
        File = reader.Read<MyTelegram.Schema.IInputFile>();
        if (Flags[0]) { Stickers = reader.Read<TVector<MyTelegram.Schema.IInputDocument>>(); }
        if (Flags[1]) { TtlSeconds = reader.ReadInt32(); }
    }
}