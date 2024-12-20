﻿// ReSharper disable All

namespace MyTelegram.Schema.Payments;

///<summary>
/// Info about the current <a href="https://corefork.telegram.org/api/stars#balance-and-transaction-history">Telegram Star subscriptions, balance and transaction history »</a>.
/// See <a href="https://corefork.telegram.org/constructor/payments.StarsStatus" />
///</summary>
[JsonDerivedType(typeof(TStarsStatus), nameof(TStarsStatus))]
public interface IStarsStatus : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// Current Telegram Star balance.
    /// See <a href="https://corefork.telegram.org/type/StarsAmount" />
    ///</summary>
    MyTelegram.Schema.IStarsAmount Balance { get; set; }

    ///<summary>
    /// Info about current Telegram Star subscriptions, only returned when invoking <a href="https://corefork.telegram.org/method/payments.getStarsTransactions">payments.getStarsTransactions</a> and <a href="https://corefork.telegram.org/method/payments.getStarsSubscriptions">payments.getStarsSubscriptions</a>.
    /// See <a href="https://corefork.telegram.org/type/StarsSubscription" />
    ///</summary>
    TVector<MyTelegram.Schema.IStarsSubscription>? Subscriptions { get; set; }

    ///<summary>
    /// Offset for pagination of subscriptions: only usable and returned when invoking <a href="https://corefork.telegram.org/method/payments.getStarsSubscriptions">payments.getStarsSubscriptions</a>.
    ///</summary>
    string? SubscriptionsNextOffset { get; set; }

    ///<summary>
    /// The number of Telegram Stars the user should buy to be able to extend expired subscriptions soon (i.e. the current balance is not enough to extend all expired subscriptions).
    ///</summary>
    long? SubscriptionsMissingBalance { get; set; }

    ///<summary>
    /// List of Telegram Star transactions (partial if <code>next_offset</code> is set).
    /// See <a href="https://corefork.telegram.org/type/StarsTransaction" />
    ///</summary>
    TVector<MyTelegram.Schema.IStarsTransaction>? History { get; set; }

    ///<summary>
    /// Offset to use to fetch more transactions from the transaction history using <a href="https://corefork.telegram.org/method/payments.getStarsTransactions">payments.getStarsTransactions</a>.
    ///</summary>
    string? NextOffset { get; set; }

    ///<summary>
    /// Chats mentioned in <code>history</code>.
    /// See <a href="https://corefork.telegram.org/type/Chat" />
    ///</summary>
    TVector<MyTelegram.Schema.IChat> Chats { get; set; }

    ///<summary>
    /// Users mentioned in <code>history</code>.
    /// See <a href="https://corefork.telegram.org/type/User" />
    ///</summary>
    TVector<MyTelegram.Schema.IUser> Users { get; set; }
}
