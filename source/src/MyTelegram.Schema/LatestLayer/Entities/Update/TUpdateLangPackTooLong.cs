﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// A language pack has changed, the client should manually fetch the changed strings using <a href="https://corefork.telegram.org/method/langpack.getDifference">langpack.getDifference</a>
/// See <a href="https://corefork.telegram.org/constructor/updateLangPackTooLong" />
///</summary>
[TlObject(0x46560264)]
public sealed class TUpdateLangPackTooLong : IUpdate
{
    public uint ConstructorId => 0x46560264;
    ///<summary>
    /// Language code
    ///</summary>
    public string LangCode { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(LangCode);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        LangCode = reader.ReadString();
    }
}