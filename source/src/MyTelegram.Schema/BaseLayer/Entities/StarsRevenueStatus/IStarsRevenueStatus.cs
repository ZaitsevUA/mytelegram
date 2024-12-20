﻿// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Describes <a href="https://corefork.telegram.org/api/stars">Telegram Star revenue balances »</a>.
/// See <a href="https://corefork.telegram.org/constructor/StarsRevenueStatus" />
///</summary>
[JsonDerivedType(typeof(TStarsRevenueStatus), nameof(TStarsRevenueStatus))]
public interface IStarsRevenueStatus : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// If set, the user may <a href="https://corefork.telegram.org/api/stars#withdrawing-stars">withdraw</a> up to <code>available_balance</code> stars.
    ///</summary>
    bool WithdrawalEnabled { get; set; }

    ///<summary>
    /// Amount of not-yet-withdrawn Telegram Stars.
    /// See <a href="https://corefork.telegram.org/type/StarsAmount" />
    ///</summary>
    MyTelegram.Schema.IStarsAmount CurrentBalance { get; set; }

    ///<summary>
    /// Amount of withdrawable Telegram Stars.
    /// See <a href="https://corefork.telegram.org/type/StarsAmount" />
    ///</summary>
    MyTelegram.Schema.IStarsAmount AvailableBalance { get; set; }

    ///<summary>
    /// Total amount of earned Telegram Stars.
    /// See <a href="https://corefork.telegram.org/type/StarsAmount" />
    ///</summary>
    MyTelegram.Schema.IStarsAmount OverallRevenue { get; set; }

    ///<summary>
    /// Unixtime indicating when will withdrawal be available to the user. If not set, withdrawal can be started now.
    ///</summary>
    int? NextWithdrawalAt { get; set; }
}
