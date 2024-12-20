﻿// ReSharper disable All

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

    ///<summary>
    /// Whether this message can be <a href="https://corefork.telegram.org/api/sponsored-messages#reporting-sponsored-messages">reported as specified here »</a>.
    ///</summary>
    bool CanReport { get; set; }

    ///<summary>
    /// Message ID
    ///</summary>
    byte[] RandomId { get; set; }

    ///<summary>
    /// Contains the URL to open when the user clicks on the sponsored message.
    ///</summary>
    string Url { get; set; }

    ///<summary>
    /// Contains the title of the sponsored message.
    ///</summary>
    string Title { get; set; }

    ///<summary>
    /// Sponsored message
    ///</summary>
    string Message { get; set; }

    ///<summary>
    /// <a href="https://corefork.telegram.org/api/entities">Message entities for styled text</a> in <code>message</code>.
    /// See <a href="https://corefork.telegram.org/type/MessageEntity" />
    ///</summary>
    TVector<MyTelegram.Schema.IMessageEntity>? Entities { get; set; }

    ///<summary>
    /// If set, contains a custom profile photo bubble that should be displayed for the sponsored message, like for messages sent in groups.
    /// See <a href="https://corefork.telegram.org/type/Photo" />
    ///</summary>
    MyTelegram.Schema.IPhoto? Photo { get; set; }

    ///<summary>
    /// If set, contains some media.
    /// See <a href="https://corefork.telegram.org/type/MessageMedia" />
    ///</summary>
    MyTelegram.Schema.IMessageMedia? Media { get; set; }

    ///<summary>
    /// If set, the sponsored message should use the <a href="https://corefork.telegram.org/api/colors">message accent color »</a> specified in <code>color</code>.
    /// See <a href="https://corefork.telegram.org/type/PeerColor" />
    ///</summary>
    MyTelegram.Schema.IPeerColor? Color { get; set; }

    ///<summary>
    /// Label of the sponsored message button.
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
