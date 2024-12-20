﻿// ReSharper disable All

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

    ///<summary>
    /// This transaction is a <a href="https://corefork.telegram.org/api/reactions#paid-reactions">paid reaction »</a>.
    ///</summary>
    bool Reaction { get; set; }

    ///<summary>
    /// Transaction ID.
    ///</summary>
    string Id { get; set; }

    ///<summary>
    /// Amount of Stars (negative for outgoing transactions).
    /// See <a href="https://corefork.telegram.org/type/StarsAmount" />
    ///</summary>
    MyTelegram.Schema.IStarsAmount Stars { get; set; }

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

    ///<summary>
    /// The number of seconds between consecutive Telegram Star debiting for <a href="https://corefork.telegram.org/api/stars#star-subscriptions">Telegram Star subscriptions »</a>.
    ///</summary>
    int? SubscriptionPeriod { get; set; }

    ///<summary>
    /// ID of the message containing the <a href="https://corefork.telegram.org/constructor/messageMediaGiveaway">messageMediaGiveaway</a>, for incoming <a href="https://corefork.telegram.org/api/giveaways#star-giveaways">star giveaway prizes</a>.
    ///</summary>
    int? GiveawayPostId { get; set; }

    ///<summary>
    /// This transaction indicates a purchase or a sale (conversion back to Stars) of a <a href="https://corefork.telegram.org/api/stars">gift »</a>.
    /// See <a href="https://corefork.telegram.org/type/StarGift" />
    ///</summary>
    MyTelegram.Schema.IStarGift? Stargift { get; set; }

    ///<summary>
    /// This transaction is payment for <a href="https://corefork.telegram.org/bots/faq#how-can-i-message-all-of-my-bot-39s-subscribers-at-once">paid bot broadcasts</a>.  <br>Paid broadcasts are only allowed if the <code>allow_paid_floodskip</code> parameter of <a href="https://corefork.telegram.org/method/messages.sendMessage">messages.sendMessage</a> and other message sending methods is set while trying to broadcast more than 30 messages per second to bot users. <br>The integer value returned by this flag indicates the number of billed API calls.
    ///</summary>
    int? FloodskipNumber { get; set; }

    ///<summary>
    /// &nbsp;
    ///</summary>
    int? StarrefCommissionPermille { get; set; }

    ///<summary>
    /// &nbsp;
    /// See <a href="https://corefork.telegram.org/type/Peer" />
    ///</summary>
    MyTelegram.Schema.IPeer? StarrefPeer { get; set; }

    ///<summary>
    /// &nbsp;
    /// See <a href="https://corefork.telegram.org/type/StarsAmount" />
    ///</summary>
    MyTelegram.Schema.IStarsAmount? StarrefAmount { get; set; }
}
