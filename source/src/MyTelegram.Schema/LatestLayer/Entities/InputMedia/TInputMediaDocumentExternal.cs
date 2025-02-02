﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Document that will be downloaded by the telegram servers
/// See <a href="https://corefork.telegram.org/constructor/inputMediaDocumentExternal" />
///</summary>
[TlObject(0xfb52dc99)]
public sealed class TInputMediaDocumentExternal : IInputMedia
{
    public uint ConstructorId => 0xfb52dc99;
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
    /// URL of the document
    ///</summary>
    public string Url { get; set; }

    ///<summary>
    /// Self-destruct time to live of document
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
        writer.Write(Url);
        if (Flags[0]) { writer.Write(TtlSeconds.Value); }
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Flags = reader.ReadBitArray();
        if (Flags[1]) { Spoiler = true; }
        Url = reader.ReadString();
        if (Flags[0]) { TtlSeconds = reader.ReadInt32(); }
    }
}