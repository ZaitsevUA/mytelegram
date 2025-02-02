﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// You gifted or were gifted some <a href="https://corefork.telegram.org/api/stars">Telegram Stars</a>.
/// See <a href="https://corefork.telegram.org/constructor/messageActionGiftStars" />
///</summary>
[TlObject(0x45d5b021)]
public sealed class TMessageActionGiftStars : IMessageAction
{
    public uint ConstructorId => 0x45d5b021;
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    public BitArray Flags { get; set; } = new BitArray(32);

    ///<summary>
    /// Three-letter ISO 4217 <a href="https://corefork.telegram.org/bots/payments#supported-currencies">currency</a> code
    ///</summary>
    public string Currency { get; set; }

    ///<summary>
    /// Price of the gift in the smallest units of the currency (integer, not float/double). For example, for a price of <code>US$ 1.45</code> pass <code>amount = 145</code>. See the exp parameter in <a href="https://corefork.telegram.org/bots/payments/currencies.json">currencies.json</a>, it shows the number of digits past the decimal point for each currency (2 for the majority of currencies).
    ///</summary>
    public long Amount { get; set; }

    ///<summary>
    /// Amount of gifted stars
    ///</summary>
    public long Stars { get; set; }

    ///<summary>
    /// If the gift was bought using a cryptocurrency, the cryptocurrency name.
    ///</summary>
    public string? CryptoCurrency { get; set; }

    ///<summary>
    /// If the gift was bought using a cryptocurrency, price of the gift in the smallest units of a cryptocurrency.
    ///</summary>
    public long? CryptoAmount { get; set; }

    ///<summary>
    /// Identifier of the transaction, only visible to the receiver of the gift.
    ///</summary>
    public string? TransactionId { get; set; }

    public void ComputeFlag()
    {
        if (CryptoCurrency != null) { Flags[0] = true; }
        if (/*CryptoAmount != 0 &&*/ CryptoAmount.HasValue) { Flags[0] = true; }
        if (TransactionId != null) { Flags[1] = true; }
    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Flags);
        writer.Write(Currency);
        writer.Write(Amount);
        writer.Write(Stars);
        if (Flags[0]) { writer.Write(CryptoCurrency); }
        if (Flags[0]) { writer.Write(CryptoAmount.Value); }
        if (Flags[1]) { writer.Write(TransactionId); }
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Flags = reader.ReadBitArray();
        Currency = reader.ReadString();
        Amount = reader.ReadInt64();
        Stars = reader.ReadInt64();
        if (Flags[0]) { CryptoCurrency = reader.ReadString(); }
        if (Flags[0]) { CryptoAmount = reader.ReadInt64(); }
        if (Flags[1]) { TransactionId = reader.ReadString(); }
    }
}