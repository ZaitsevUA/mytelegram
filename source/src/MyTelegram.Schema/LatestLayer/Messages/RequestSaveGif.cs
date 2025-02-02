﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema.Messages;

///<summary>
/// Add GIF to saved gifs list
/// <para>Possible errors</para>
/// Code Type Description
/// 400 GIF_ID_INVALID The provided GIF ID is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.saveGif" />
///</summary>
[TlObject(0x327a30cb)]
public sealed class RequestSaveGif : IRequest<IBool>
{
    public uint ConstructorId => 0x327a30cb;
    ///<summary>
    /// GIF to save
    /// See <a href="https://corefork.telegram.org/type/InputDocument" />
    ///</summary>
    public MyTelegram.Schema.IInputDocument Id { get; set; }

    ///<summary>
    /// Whether to remove GIF from saved gifs list
    /// See <a href="https://corefork.telegram.org/type/Bool" />
    ///</summary>
    public bool Unsave { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Id);
        writer.Write(Unsave);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Id = reader.Read<MyTelegram.Schema.IInputDocument>();
        Unsave = reader.Read();
    }
}
