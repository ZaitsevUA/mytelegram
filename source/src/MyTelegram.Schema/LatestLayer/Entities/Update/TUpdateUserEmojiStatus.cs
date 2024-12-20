﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// The <a href="https://corefork.telegram.org/api/emoji-status">emoji status</a> of a certain user has changed
/// See <a href="https://corefork.telegram.org/constructor/updateUserEmojiStatus" />
///</summary>
[TlObject(0x28373599)]
public sealed class TUpdateUserEmojiStatus : IUpdate
{
    public uint ConstructorId => 0x28373599;
    ///<summary>
    /// User ID
    ///</summary>
    public long UserId { get; set; }

    ///<summary>
    /// New <a href="https://corefork.telegram.org/api/emoji-status">emoji status</a>
    /// See <a href="https://corefork.telegram.org/type/EmojiStatus" />
    ///</summary>
    public MyTelegram.Schema.IEmojiStatus EmojiStatus { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(UserId);
        writer.Write(EmojiStatus);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        UserId = reader.ReadInt64();
        EmojiStatus = reader.Read<MyTelegram.Schema.IEmojiStatus>();
    }
}