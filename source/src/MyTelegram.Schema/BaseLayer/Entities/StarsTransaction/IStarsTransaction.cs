// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
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
    /// &nbsp;
    ///</summary>
    bool Refund { get; set; }
    bool Pending { get; set; }
    bool Failed { get; set; }

    ///<summary>
    /// &nbsp;
    ///</summary>
    string Id { get; set; }

    ///<summary>
    /// &nbsp;
    ///</summary>
    long Stars { get; set; }

    ///<summary>
    /// &nbsp;
    ///</summary>
    int Date { get; set; }

    ///<summary>
    /// &nbsp;
    /// See <a href="https://corefork.telegram.org/type/StarsTransactionPeer" />
    ///</summary>
    MyTelegram.Schema.IStarsTransactionPeer Peer { get; set; }

    ///<summary>
    /// &nbsp;
    ///</summary>
    string? Title { get; set; }

    ///<summary>
    /// &nbsp;
    ///</summary>
    string? Description { get; set; }

    ///<summary>
    /// &nbsp;
    /// See <a href="https://corefork.telegram.org/type/WebDocument" />
    ///</summary>
    MyTelegram.Schema.IWebDocument? Photo { get; set; }
    int? TransactionDate { get; set; }
    string? TransactionUrl { get; set; }
    byte[]? BotPayload { get; set; }
    int? MsgId { get; set; }
    TVector<MyTelegram.Schema.IMessageMedia>? ExtendedMedia { get; set; }
}
