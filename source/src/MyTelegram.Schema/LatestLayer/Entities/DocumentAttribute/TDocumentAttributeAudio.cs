﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Represents an audio file
/// See <a href="https://corefork.telegram.org/constructor/documentAttributeAudio" />
///</summary>
[TlObject(0x9852f9c6)]
public sealed class TDocumentAttributeAudio : IDocumentAttribute
{
    public uint ConstructorId => 0x9852f9c6;
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    public BitArray Flags { get; set; } = new BitArray(32);

    ///<summary>
    /// Whether this is a voice message
    /// See <a href="https://corefork.telegram.org/type/true" />
    ///</summary>
    public bool Voice { get; set; }

    ///<summary>
    /// Duration in seconds
    ///</summary>
    public int Duration { get; set; }

    ///<summary>
    /// Name of song
    ///</summary>
    public string? Title { get; set; }

    ///<summary>
    /// Performer
    ///</summary>
    public string? Performer { get; set; }

    ///<summary>
    /// Waveform: consists in a series of bitpacked 5-bit values. <br>Example implementation: <a href="https://github.com/DrKLO/Telegram/blob/96dce2c9aabc33b87db61d830aa087b6b03fe397/TMessagesProj/jni/audio.c#L546">android</a>.
    ///</summary>
    public byte[]? Waveform { get; set; }

    public void ComputeFlag()
    {
        if (Voice) { Flags[10] = true; }
        if (Title != null) { Flags[0] = true; }
        if (Performer != null) { Flags[1] = true; }
        if (Waveform != null) { Flags[2] = true; }
    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Flags);
        writer.Write(Duration);
        if (Flags[0]) { writer.Write(Title); }
        if (Flags[1]) { writer.Write(Performer); }
        if (Flags[2]) { writer.Write(Waveform); }
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Flags = reader.ReadBitArray();
        if (Flags[10]) { Voice = true; }
        Duration = reader.ReadInt32();
        if (Flags[0]) { Title = reader.ReadString(); }
        if (Flags[1]) { Performer = reader.ReadString(); }
        if (Flags[2]) { Waveform = reader.ReadBytes(); }
    }
}