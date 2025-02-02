﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema.Help;

///<summary>
/// Get name, ISO code, localized name and phone codes/patterns of all available countries
/// See <a href="https://corefork.telegram.org/method/help.getCountriesList" />
///</summary>
[TlObject(0x735787a8)]
public sealed class RequestGetCountriesList : IRequest<MyTelegram.Schema.Help.ICountriesList>
{
    public uint ConstructorId => 0x735787a8;
    ///<summary>
    /// Language code of the current user
    ///</summary>
    public string LangCode { get; set; }

    ///<summary>
    /// <a href="https://corefork.telegram.org/api/offsets#hash-generation">Hash used for caching, for more info click here</a>.
    ///</summary>
    public int Hash { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(LangCode);
        writer.Write(Hash);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        LangCode = reader.ReadString();
        Hash = reader.ReadInt32();
    }
}
