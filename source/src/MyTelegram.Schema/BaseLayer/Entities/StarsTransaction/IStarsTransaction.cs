// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Represents a <a href="https://corefork.telegram.org/api/stars">Telegram Stars transaction »</a>.
/// See <a href="https://corefork.telegram.org/constructor/StarsTransaction" />
///</summary>
[JsonDerivedType(typeof(TStarsTransaction), nameof(TStarsTransaction))]
public interface IStarsTransaction : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// Whether this transaction is a refund.
    ///</summary>
    bool Refund { get; set; }

    ///<summary>
    /// The transaction is currently pending.
    ///</summary>
    bool Pending { get; set; }

    ///<summary>
    /// This transaction has failed.
    ///</summary>
    bool Failed { get; set; }

    ///<summary>
    /// This transaction was a gift from the user in <code>peer.peer</code>.
    ///</summary>
    bool Gift { get; set; }
    bool Reaction { get; set; }

    ///<summary>
    /// Transaction ID.
    ///</summary>
    string Id { get; set; }

    ///<summary>
    /// Amount of Stars (negative for outgoing transactions).
    ///</summary>
    long Stars { get; set; }

    ///<summary>
    /// Date of the transaction (unixtime).
    ///</summary>
    int Date { get; set; }

    ///<summary>
    /// Source of the incoming transaction, or its recipient for outgoing transactions.
    /// See <a href="https://corefork.telegram.org/type/StarsTransactionPeer" />
    ///</summary>
    MyTelegram.Schema.IStarsTransactionPeer Peer { get; set; }

    ///<summary>
    /// For transactions with bots, title of the bought product.
    ///</summary>
    string? Title { get; set; }

    ///<summary>
    /// For transactions with bots, description of the bought product.
    ///</summary>
    string? Description { get; set; }

    ///<summary>
    /// For transactions with bots, photo of the bought product.
    /// See <a href="https://corefork.telegram.org/type/WebDocument" />
    ///</summary>
    MyTelegram.Schema.IWebDocument? Photo { get; set; }

    ///<summary>
    /// If neither <code>pending</code> nor <code>failed</code> are set, the transaction was completed successfully, and this field will contain the point in time (Unix timestamp) when the withdrawal was completed successfully.
    ///</summary>
    int? TransactionDate { get; set; }

    ///<summary>
    /// If neither <code>pending</code> nor <code>failed</code> are set, the transaction was completed successfully, and this field will contain a URL where the withdrawal transaction can be viewed.
    ///</summary>
    string? TransactionUrl { get; set; }

    ///<summary>
    /// Bot specified invoice payload (i.e. the <code>payload</code> passed to <a href="https://corefork.telegram.org/constructor/inputMediaInvoice">inputMediaInvoice</a> when <a href="https://corefork.telegram.org/api/payments">creating the invoice</a>).
    ///</summary>
    byte[]? BotPayload { get; set; }

    ///<summary>
    /// For <a href="https://corefork.telegram.org/api/paid-media">paid media transactions »</a>, message ID of the paid media posted to <code>peer.peer</code> (can point to a deleted message; either way, <code>extended_media</code> will always contain the bought media).
    ///</summary>
    int? MsgId { get; set; }

    ///<summary>
    /// The purchased <a href="https://corefork.telegram.org/api/paid-media">paid media »</a>.
    /// See <a href="https://corefork.telegram.org/type/MessageMedia" />
    ///</summary>
    TVector<MyTelegram.Schema.IMessageMedia>? ExtendedMedia { get; set; }
    int? SubscriptionPeriod { get; set; }
    int? GiveawayPostId { get; set; }
    MyTelegram.Schema.IStarGift? Stargift { get; set; }
    int? FloodskipNumber { get; set; }
}
