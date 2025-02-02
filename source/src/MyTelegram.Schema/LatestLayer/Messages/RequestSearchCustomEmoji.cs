﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema.Messages;

///<summary>
/// Look for <a href="https://corefork.telegram.org/api/custom-emoji">custom emojis</a> associated to a UTF8 emoji
/// <para>Possible errors</para>
/// Code Type Description
/// 400 EMOTICON_EMPTY The emoji is empty.
/// See <a href="https://corefork.telegram.org/method/messages.searchCustomEmoji" />
///</summary>
[TlObject(0x2c11c0d7)]
public sealed class RequestSearchCustomEmoji : IRequest<MyTelegram.Schema.IEmojiList>
{
    public uint ConstructorId => 0x2c11c0d7;
    ///<summary>
    /// The emoji
    ///</summary>
    public string Emoticon { get; set; }

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
        writer.Write(Emoticon);
        writer.Write(Hash);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Emoticon = reader.ReadString();
        Hash = reader.ReadInt64();
    }
}
