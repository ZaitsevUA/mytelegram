﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Indicates that <a href="https://corefork.telegram.org/api/stories#stealth-mode">stories stealth mode</a> was activated.
/// See <a href="https://corefork.telegram.org/constructor/updateStoriesStealthMode" />
///</summary>
[TlObject(0x2c084dc1)]
public sealed class TUpdateStoriesStealthMode : IUpdate
{
    public uint ConstructorId => 0x2c084dc1;
    ///<summary>
    /// Information about the current <a href="https://corefork.telegram.org/api/stories#stealth-mode">stealth mode</a> session.
    /// See <a href="https://corefork.telegram.org/type/StoriesStealthMode" />
    ///</summary>
    public MyTelegram.Schema.IStoriesStealthMode StealthMode { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(StealthMode);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        StealthMode = reader.Read<MyTelegram.Schema.IStoriesStealthMode>();
    }
}