﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Default <a href="https://corefork.telegram.org/api/emoji-status">custom emoji status</a> stickerset for channel statuses
/// See <a href="https://corefork.telegram.org/constructor/inputStickerSetEmojiChannelDefaultStatuses" />
///</summary>
[TlObject(0x49748553)]
public sealed class TInputStickerSetEmojiChannelDefaultStatuses : IInputStickerSet
{
    public uint ConstructorId => 0x49748553;


    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);

    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {

    }
}