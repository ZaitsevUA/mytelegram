﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema.Bots;

///<summary>
/// Fetch popular <a href="https://corefork.telegram.org/api/bots/webapps#main-mini-apps">Main Mini Apps</a>, to be used in the <a href="https://corefork.telegram.org/api/search#apps-tab">apps tab of global search »</a>.
/// See <a href="https://corefork.telegram.org/method/bots.getPopularAppBots" />
///</summary>
[TlObject(0xc2510192)]
public sealed class RequestGetPopularAppBots : IRequest<MyTelegram.Schema.Bots.IPopularAppBots>
{
    public uint ConstructorId => 0xc2510192;
    ///<summary>
    /// Offset for <a href="https://corefork.telegram.org/api/offsets">pagination</a>, initially an empty string, then re-use the <code>next_offset</code> returned by the previous query.
    ///</summary>
    public string Offset { get; set; }

    ///<summary>
    /// Maximum number of results to return, <a href="https://corefork.telegram.org/api/offsets">see pagination</a>
    ///</summary>
    public int Limit { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Offset);
        writer.Write(Limit);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Offset = reader.ReadString();
        Limit = reader.ReadInt32();
    }
}
