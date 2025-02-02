﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Describes a <a href="https://corefork.telegram.org/api/stars">Telegram Star</a> transaction used to pay for <a href="https://corefork.telegram.org/api/stars#paying-for-ads">Telegram ads as specified here »</a>.
/// See <a href="https://corefork.telegram.org/constructor/starsTransactionPeerAds" />
///</summary>
[TlObject(0x60682812)]
public sealed class TStarsTransactionPeerAds : IStarsTransactionPeer
{
    public uint ConstructorId => 0x60682812;


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