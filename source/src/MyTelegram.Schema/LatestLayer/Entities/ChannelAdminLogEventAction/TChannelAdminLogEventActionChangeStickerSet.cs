﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// The supergroup's stickerset was changed
/// See <a href="https://corefork.telegram.org/constructor/channelAdminLogEventActionChangeStickerSet" />
///</summary>
[TlObject(0xb1c3caa7)]
public sealed class TChannelAdminLogEventActionChangeStickerSet : IChannelAdminLogEventAction
{
    public uint ConstructorId => 0xb1c3caa7;
    ///<summary>
    /// Previous stickerset
    /// See <a href="https://corefork.telegram.org/type/InputStickerSet" />
    ///</summary>
    public MyTelegram.Schema.IInputStickerSet PrevStickerset { get; set; }

    ///<summary>
    /// New stickerset
    /// See <a href="https://corefork.telegram.org/type/InputStickerSet" />
    ///</summary>
    public MyTelegram.Schema.IInputStickerSet NewStickerset { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(PrevStickerset);
        writer.Write(NewStickerset);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        PrevStickerset = reader.Read<MyTelegram.Schema.IInputStickerSet>();
        NewStickerset = reader.Read<MyTelegram.Schema.IInputStickerSet>();
    }
}