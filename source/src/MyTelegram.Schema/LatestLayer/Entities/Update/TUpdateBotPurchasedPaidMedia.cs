﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Bots only: a user has purchased a <a href="https://corefork.telegram.org/api/paid-media">paid media</a>.
/// See <a href="https://corefork.telegram.org/constructor/updateBotPurchasedPaidMedia" />
///</summary>
[TlObject(0x283bd312)]
public sealed class TUpdateBotPurchasedPaidMedia : IUpdate
{
    public uint ConstructorId => 0x283bd312;
    ///<summary>
    /// The user that bought the media
    ///</summary>
    public long UserId { get; set; }

    ///<summary>
    /// Payload passed by the bot in <a href="https://corefork.telegram.org/constructor/inputMediaPaidMedia">inputMediaPaidMedia</a>.<code>payload</code>
    ///</summary>
    public string Payload { get; set; }

    ///<summary>
    /// New <strong>qts</strong> value, see <a href="https://corefork.telegram.org/api/updates">updates »</a> for more info.
    ///</summary>
    public int Qts { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(UserId);
        writer.Write(Payload);
        writer.Write(Qts);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        UserId = reader.ReadInt64();
        Payload = reader.ReadString();
        Qts = reader.ReadInt32();
    }
}