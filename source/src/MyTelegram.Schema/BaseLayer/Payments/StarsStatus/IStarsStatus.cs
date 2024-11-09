// ReSharper disable All

namespace MyTelegram.Schema.Payments;

///<summary>
/// Info about the current <a href="https://corefork.telegram.org/api/stars#balance-and-transaction-history">Telegram Star balance and transaction history »</a>.
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
    ///</summary>
    long Balance { get; set; }
    TVector<MyTelegram.Schema.IStarsSubscription>? Subscriptions { get; set; }
    string? SubscriptionsNextOffset { get; set; }
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
