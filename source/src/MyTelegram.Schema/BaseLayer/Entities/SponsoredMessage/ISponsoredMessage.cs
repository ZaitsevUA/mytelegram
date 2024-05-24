// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// A sponsored message
/// See <a href="https://corefork.telegram.org/constructor/SponsoredMessage" />
///</summary>
[JsonDerivedType(typeof(TSponsoredMessage), nameof(TSponsoredMessage))]
public interface ISponsoredMessage : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// Whether the message needs to be labeled as "recommended" instead of "sponsored"
    ///</summary>
    bool Recommended { get; set; }
    bool CanReport { get; set; }

    ///<summary>
    /// Message ID
    ///</summary>
    byte[] RandomId { get; set; }
    string Url { get; set; }
    string Title { get; set; }

    ///<summary>
    /// Sponsored message
    ///</summary>
    string Message { get; set; }

    ///<summary>
    /// <a href="https://corefork.telegram.org/api/entities">Message entities for styled text</a>
    /// See <a href="https://corefork.telegram.org/type/MessageEntity" />
    ///</summary>
    TVector<MyTelegram.Schema.IMessageEntity>? Entities { get; set; }
    MyTelegram.Schema.IPhoto? Photo { get; set; }
    MyTelegram.Schema.IPeerColor? Color { get; set; }

    ///<summary>
    /// Text of the sponsored message button.
    ///</summary>
    string ButtonText { get; set; }

    ///<summary>
    /// If set, contains additional information about the sponsor to be shown along with the message.
    ///</summary>
    string? SponsorInfo { get; set; }

    ///<summary>
    /// If set, contains additional information about the sponsored message to be shown along with the message.
    ///</summary>
    string? AdditionalInfo { get; set; }
}
