﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Contains info about a <a href="https://corefork.telegram.org/api/giveaways#star-giveaways">Telegram Star giveaway</a> option.
/// See <a href="https://corefork.telegram.org/constructor/starsGiveawayOption" />
///</summary>
[TlObject(0x94ce852a)]
public sealed class TStarsGiveawayOption : IStarsGiveawayOption
{
    public uint ConstructorId => 0x94ce852a;
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    public BitArray Flags { get; set; } = new BitArray(32);

    ///<summary>
    /// If set, this option must only be shown in the full list of giveaway options (i.e. they must be added to the list only when the user clicks on the expand button).
    /// See <a href="https://corefork.telegram.org/type/true" />
    ///</summary>
    public bool Extended { get; set; }

    ///<summary>
    /// If set, this option must be pre-selected by default in the option list.
    /// See <a href="https://corefork.telegram.org/type/true" />
    ///</summary>
    public bool Default { get; set; }

    ///<summary>
    /// The number of Telegram Stars that will be distributed among winners
    ///</summary>
    public long Stars { get; set; }

    ///<summary>
    /// Number of times the chat will be boosted for one year if the <a href="https://corefork.telegram.org/constructor/inputStorePaymentStarsGiveaway">inputStorePaymentStarsGiveaway</a>.<code>boost_peer</code> flag is populated
    ///</summary>
    public int YearlyBoosts { get; set; }

    ///<summary>
    /// Identifier of the store product associated with the option, official apps only.
    ///</summary>
    public string? StoreProduct { get; set; }

    ///<summary>
    /// Three-letter ISO 4217 <a href="https://corefork.telegram.org/bots/payments#supported-currencies">currency</a> code
    ///</summary>
    public string Currency { get; set; }

    ///<summary>
    /// Total price in the smallest units of the currency (integer, not float/double). For example, for a price of <code>US$ 1.45</code> pass <code>amount = 145</code>. See the exp parameter in <a href="https://corefork.telegram.org/bots/payments/currencies.json">currencies.json</a>, it shows the number of digits past the decimal point for each currency (2 for the majority of currencies).
    ///</summary>
    public long Amount { get; set; }

    ///<summary>
    /// Allowed options for the number of giveaway winners.
    ///</summary>
    public TVector<MyTelegram.Schema.IStarsGiveawayWinnersOption> Winners { get; set; }

    public void ComputeFlag()
    {
        if (Extended) { Flags[0] = true; }
        if (Default) { Flags[1] = true; }
        if (StoreProduct != null) { Flags[2] = true; }

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Flags);
        writer.Write(Stars);
        writer.Write(YearlyBoosts);
        if (Flags[2]) { writer.Write(StoreProduct); }
        writer.Write(Currency);
        writer.Write(Amount);
        writer.Write(Winners);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Flags = reader.ReadBitArray();
        if (Flags[0]) { Extended = true; }
        if (Flags[1]) { Default = true; }
        Stars = reader.ReadInt64();
        YearlyBoosts = reader.ReadInt32();
        if (Flags[2]) { StoreProduct = reader.ReadString(); }
        Currency = reader.ReadString();
        Amount = reader.ReadInt64();
        Winners = reader.Read<TVector<MyTelegram.Schema.IStarsGiveawayWinnersOption>>();
    }
}