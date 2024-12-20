﻿// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// <a href="https://corefork.telegram.org/api/stars">Telegram Stars topup option</a>.
/// See <a href="https://corefork.telegram.org/constructor/StarsTopupOption" />
///</summary>
[JsonDerivedType(typeof(TStarsTopupOption), nameof(TStarsTopupOption))]
public interface IStarsTopupOption : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// If set, the option must only be shown in the full list of topup options.
    ///</summary>
    bool Extended { get; set; }

    ///<summary>
    /// Amount of Telegram stars.
    ///</summary>
    long Stars { get; set; }

    ///<summary>
    /// Identifier of the store product associated with the option, official apps only.
    ///</summary>
    string? StoreProduct { get; set; }

    ///<summary>
    /// Three-letter ISO 4217 <a href="https://corefork.telegram.org/bots/payments#supported-currencies">currency</a> code
    ///</summary>
    string Currency { get; set; }

    ///<summary>
    /// Price of the product in the smallest units of the currency (integer, not float/double). For example, for a price of <code>US$ 1.45</code> pass <code>amount = 145</code>. See the exp parameter in <a href="https://corefork.telegram.org/bots/payments/currencies.json">currencies.json</a>, it shows the number of digits past the decimal point for each currency (2 for the majority of currencies).
    ///</summary>
    long Amount { get; set; }
}
