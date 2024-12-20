﻿// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Info about bots (available bot commands, etc)
/// See <a href="https://corefork.telegram.org/constructor/BotInfo" />
///</summary>
[JsonDerivedType(typeof(TBotInfo), nameof(TBotInfo))]
public interface IBotInfo : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// If set, the bot has some <a href="https://corefork.telegram.org/api/bots/webapps#main-mini-app-previews">preview medias for the configured Main Mini App, see here »</a> for more info on Main Mini App preview medias.
    ///</summary>
    bool HasPreviewMedias { get; set; }

    ///<summary>
    /// ID of the bot
    ///</summary>
    long? UserId { get; set; }

    ///<summary>
    /// Description of the bot
    ///</summary>
    string? Description { get; set; }

    ///<summary>
    /// Description photo
    /// See <a href="https://corefork.telegram.org/type/Photo" />
    ///</summary>
    MyTelegram.Schema.IPhoto? DescriptionPhoto { get; set; }

    ///<summary>
    /// Description animation in MPEG4 format
    /// See <a href="https://corefork.telegram.org/type/Document" />
    ///</summary>
    MyTelegram.Schema.IDocument? DescriptionDocument { get; set; }

    ///<summary>
    /// Bot commands that can be used in the chat
    /// See <a href="https://corefork.telegram.org/type/BotCommand" />
    ///</summary>
    TVector<MyTelegram.Schema.IBotCommand>? Commands { get; set; }

    ///<summary>
    /// Indicates the action to execute when pressing the in-UI menu button for bots
    /// See <a href="https://corefork.telegram.org/type/BotMenuButton" />
    ///</summary>
    MyTelegram.Schema.IBotMenuButton? MenuButton { get; set; }

    ///<summary>
    /// The HTTP link to the privacy policy of the bot. If not set, then the <code>/privacy</code> command must be used, if supported by the bot (i.e. if it's present in the <code>commands</code> vector). If it isn't supported, then <a href="https://telegram.org/privacy-tpa">https://telegram.org/privacy-tpa</a> must be opened, instead.
    ///</summary>
    string? PrivacyPolicyUrl { get; set; }

    ///<summary>
    /// &nbsp;
    /// See <a href="https://corefork.telegram.org/type/BotAppSettings" />
    ///</summary>
    MyTelegram.Schema.IBotAppSettings? AppSettings { get; set; }
}
