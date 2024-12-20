﻿// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Contains info about a <a href="https://corefork.telegram.org/api/giveaways#star-giveaways">Telegram Star giveaway</a> option.
/// See <a href="https://corefork.telegram.org/constructor/StarsGiveawayOption" />
///</summary>
[JsonDerivedType(typeof(TStarsGiveawayOption), nameof(TStarsGiveawayOption))]
public interface IStarsGiveawayOption : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// If set, this option must only be shown in the full list of giveaway options (i.e. they must be added to the list only when the user clicks on the expand button).
    ///</summary>
    bool Extended { get; set; }

    ///<summary>
    /// If set, this option must be pre-selected by default in the option list.
    ///</summary>
    bool Default { get; set; }

    ///<summary>
    /// The number of Telegram Stars that will be distributed among winners
    ///</summary>
    long Stars { get; set; }

    ///<summary>
    /// Number of times the chat will be boosted for one year if the <a href="https://corefork.telegram.org/constructor/inputStorePaymentStarsGiveaway">inputStorePaymentStarsGiveaway</a>.<code>boost_peer</code> flag is populated
    ///</summary>
    int YearlyBoosts { get; set; }

    ///<summary>
    /// Identifier of the store product associated with the option, official apps only.
    ///</summary>
    string? StoreProduct { get; set; }

    ///<summary>
    /// Three-letter ISO 4217 <a href="https://corefork.telegram.org/bots/payments#supported-currencies">currency</a> code
    ///</summary>
    string Currency { get; set; }

    ///<summary>
    /// Total price in the smallest units of the currency (integer, not float/double). For example, for a price of <code>US$ 1.45</code> pass <code>amount = 145</code>. See the exp parameter in <a href="https://corefork.telegram.org/bots/payments/currencies.json">currencies.json</a>, it shows the number of digits past the decimal point for each currency (2 for the majority of currencies).
    ///</summary>
    long Amount { get; set; }

    ///<summary>
    /// Allowed options for the number of giveaway winners.
    /// See <a href="https://corefork.telegram.org/type/StarsGiveawayWinnersOption" />
    ///</summary>
    TVector<MyTelegram.Schema.IStarsGiveawayWinnersOption> Winners { get; set; }
}
