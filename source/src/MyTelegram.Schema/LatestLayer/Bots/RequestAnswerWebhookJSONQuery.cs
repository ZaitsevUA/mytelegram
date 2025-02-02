﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema.Bots;

///<summary>
/// Answers a custom query; for bots only
/// <para>Possible errors</para>
/// Code Type Description
/// 400 DATA_JSON_INVALID The provided JSON data is invalid.
/// 400 QUERY_ID_INVALID The query ID is invalid.
/// 403 USER_BOT_INVALID User accounts must provide the <code>bot</code> method parameter when calling this method. If there is no such method parameter, this method can only be invoked by bot accounts.
/// 400 USER_BOT_REQUIRED This method can only be called by a bot.
/// See <a href="https://corefork.telegram.org/method/bots.answerWebhookJSONQuery" />
///</summary>
[TlObject(0xe6213f4d)]
public sealed class RequestAnswerWebhookJSONQuery : IRequest<IBool>
{
    public uint ConstructorId => 0xe6213f4d;
    ///<summary>
    /// Identifier of a custom query
    ///</summary>
    public long QueryId { get; set; }

    ///<summary>
    /// JSON-serialized answer to the query
    /// See <a href="https://corefork.telegram.org/type/DataJSON" />
    ///</summary>
    public MyTelegram.Schema.IDataJSON Data { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(QueryId);
        writer.Write(Data);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        QueryId = reader.ReadInt64();
        Data = reader.Read<MyTelegram.Schema.IDataJSON>();
    }
}
