﻿// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Object contains extended user info.
/// See <a href="https://corefork.telegram.org/constructor/UserFull" />
///</summary>
[JsonDerivedType(typeof(TUserFull), nameof(TUserFull))]
public interface IUserFull : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// Whether you have blocked this user
    ///</summary>
    bool Blocked { get; set; }

    ///<summary>
    /// Whether this user can make VoIP calls
    ///</summary>
    bool PhoneCallsAvailable { get; set; }

    ///<summary>
    /// Whether this user's privacy settings allow you to call them
    ///</summary>
    bool PhoneCallsPrivate { get; set; }

    ///<summary>
    /// Whether you can pin messages in the chat with this user, you can do this only for a chat with yourself
    ///</summary>
    bool CanPinMessage { get; set; }

    ///<summary>
    /// Whether <a href="https://corefork.telegram.org/api/scheduled-messages">scheduled messages</a> are available
    ///</summary>
    bool HasScheduled { get; set; }

    ///<summary>
    /// Whether the user can receive video calls
    ///</summary>
    bool VideoCallsAvailable { get; set; }

    ///<summary>
    /// Whether this user doesn't allow sending voice messages in a private chat with them
    ///</summary>
    bool VoiceMessagesForbidden { get; set; }

    ///<summary>
    /// Whether the <a href="https://corefork.telegram.org/api/translation">real-time chat translation popup</a> should be hidden.
    ///</summary>
    bool TranslationsDisabled { get; set; }

    ///<summary>
    /// Whether this user has some <a href="https://corefork.telegram.org/api/stories#pinned-or-archived-stories">pinned stories</a>.
    ///</summary>
    bool StoriesPinnedAvailable { get; set; }

    ///<summary>
    /// Whether we've <a href="https://corefork.telegram.org/api/block">blocked this user, preventing them from seeing our stories »</a>.
    ///</summary>
    bool BlockedMyStoriesFrom { get; set; }

    ///<summary>
    /// Whether the other user has chosen a custom wallpaper for us using <a href="https://corefork.telegram.org/method/messages.setChatWallPaper">messages.setChatWallPaper</a> and the <code>for_both</code> flag, see <a href="https://corefork.telegram.org/api/wallpapers#installing-wallpapers-in-a-specific-chat-or-channel">here »</a> for more info.
    ///</summary>
    bool WallpaperOverridden { get; set; }

    ///<summary>
    /// If set, we cannot write to this user: subscribe to <a href="https://corefork.telegram.org/api/premium">Telegram Premium</a> to get permission to write to this user. <br>To set this flag for ourselves invoke <a href="https://corefork.telegram.org/method/account.setGlobalPrivacySettings">account.setGlobalPrivacySettings</a>, setting the <code>settings.new_noncontact_peers_require_premium</code> flag, see <a href="https://corefork.telegram.org/api/privacy#require-premium-for-new-non-contact-users">here »</a> for more info.
    ///</summary>
    bool ContactRequirePremium { get; set; }

    ///<summary>
    /// If set, we cannot fetch the exact read date of messages we send to this user using <a href="https://corefork.telegram.org/method/messages.getOutboxReadDate">messages.getOutboxReadDate</a>.  <br>The exact read date of messages might still be unavailable for other reasons, see <a href="https://corefork.telegram.org/method/messages.getOutboxReadDate">here »</a> for more info.  <br>To set this flag for ourselves invoke <a href="https://corefork.telegram.org/method/account.setGlobalPrivacySettings">account.setGlobalPrivacySettings</a>, setting the <code>settings.hide_read_marks</code> flag.
    ///</summary>
    bool ReadDatesPrivate { get; set; }

    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags2 { get; set; }

    ///<summary>
    /// Whether ads were re-enabled for the current account (only accessible to the currently logged-in user), see <a href="https://corefork.telegram.org/api/business#re-enable-ads">here »</a> for more info.
    ///</summary>
    bool SponsoredEnabled { get; set; }

    ///<summary>
    /// If set, this user can view <a href="https://corefork.telegram.org/api/revenue#revenue-statistics">ad revenue statistics »</a> for this bot.
    ///</summary>
    bool CanViewRevenue { get; set; }

    ///<summary>
    /// &nbsp;
    ///</summary>
    bool BotCanManageEmojiStatus { get; set; }

    ///<summary>
    /// User ID
    ///</summary>
    long Id { get; set; }

    ///<summary>
    /// Bio of the user
    ///</summary>
    string? About { get; set; }

    ///<summary>
    /// Peer settings
    /// See <a href="https://corefork.telegram.org/type/PeerSettings" />
    ///</summary>
    MyTelegram.Schema.IPeerSettings Settings { get; set; }

    ///<summary>
    /// Personal profile photo, to be shown instead of <code>profile_photo</code>.
    /// See <a href="https://corefork.telegram.org/type/Photo" />
    ///</summary>
    MyTelegram.Schema.IPhoto? PersonalPhoto { get; set; }

    ///<summary>
    /// Profile photo
    /// See <a href="https://corefork.telegram.org/type/Photo" />
    ///</summary>
    MyTelegram.Schema.IPhoto? ProfilePhoto { get; set; }

    ///<summary>
    /// Fallback profile photo, displayed if no photo is present in <code>profile_photo</code> or <code>personal_photo</code>, due to privacy settings.
    /// See <a href="https://corefork.telegram.org/type/Photo" />
    ///</summary>
    MyTelegram.Schema.IPhoto? FallbackPhoto { get; set; }

    ///<summary>
    /// Notification settings
    /// See <a href="https://corefork.telegram.org/type/PeerNotifySettings" />
    ///</summary>
    MyTelegram.Schema.IPeerNotifySettings NotifySettings { get; set; }

    ///<summary>
    /// For bots, info about the bot (bot commands, etc)
    /// See <a href="https://corefork.telegram.org/type/BotInfo" />
    ///</summary>
    MyTelegram.Schema.IBotInfo? BotInfo { get; set; }

    ///<summary>
    /// Message ID of the last <a href="https://corefork.telegram.org/api/pin">pinned message</a>
    ///</summary>
    int? PinnedMsgId { get; set; }

    ///<summary>
    /// Chats in common with this user
    ///</summary>
    int CommonChatsCount { get; set; }

    ///<summary>
    /// <a href="https://corefork.telegram.org/api/folders#peer-folders">Peer folder ID, for more info click here</a>
    ///</summary>
    int? FolderId { get; set; }

    ///<summary>
    /// Time To Live of all messages in this chat; once a message is this many seconds old, it must be deleted.
    ///</summary>
    int? TtlPeriod { get; set; }

    ///<summary>
    /// Emoji associated with chat theme
    ///</summary>
    string? ThemeEmoticon { get; set; }

    ///<summary>
    /// Anonymized text to be shown instead of the user's name on forwarded messages
    ///</summary>
    string? PrivateForwardName { get; set; }

    ///<summary>
    /// A <a href="https://corefork.telegram.org/api/rights#suggested-bot-rights">suggested set of administrator rights</a> for the bot, to be shown when adding the bot as admin to a group, see <a href="https://corefork.telegram.org/api/rights#suggested-bot-rights">here for more info on how to handle them »</a>.
    /// See <a href="https://corefork.telegram.org/type/ChatAdminRights" />
    ///</summary>
    MyTelegram.Schema.IChatAdminRights? BotGroupAdminRights { get; set; }

    ///<summary>
    /// A <a href="https://corefork.telegram.org/api/rights#suggested-bot-rights">suggested set of administrator rights</a> for the bot, to be shown when adding the bot as admin to a channel, see <a href="https://corefork.telegram.org/api/rights#suggested-bot-rights">here for more info on how to handle them »</a>.
    /// See <a href="https://corefork.telegram.org/type/ChatAdminRights" />
    ///</summary>
    MyTelegram.Schema.IChatAdminRights? BotBroadcastAdminRights { get; set; }

    ///<summary>
    /// Telegram Premium subscriptions gift options
    /// See <a href="https://corefork.telegram.org/type/PremiumGiftOption" />
    ///</summary>
    TVector<MyTelegram.Schema.IPremiumGiftOption>? PremiumGifts { get; set; }

    ///<summary>
    /// <a href="https://corefork.telegram.org/api/wallpapers">Wallpaper</a> to use in the private chat with the user.
    /// See <a href="https://corefork.telegram.org/type/WallPaper" />
    ///</summary>
    MyTelegram.Schema.IWallPaper? Wallpaper { get; set; }

    ///<summary>
    /// Active <a href="https://corefork.telegram.org/api/stories">stories »</a>
    /// See <a href="https://corefork.telegram.org/type/PeerStories" />
    ///</summary>
    MyTelegram.Schema.IPeerStories? Stories { get; set; }

    ///<summary>
    /// <a href="https://corefork.telegram.org/api/business#opening-hours">Telegram Business working hours »</a>.
    /// See <a href="https://corefork.telegram.org/type/BusinessWorkHours" />
    ///</summary>
    MyTelegram.Schema.IBusinessWorkHours? BusinessWorkHours { get; set; }

    ///<summary>
    /// <a href="https://corefork.telegram.org/api/business#location">Telegram Business location »</a>.
    /// See <a href="https://corefork.telegram.org/type/BusinessLocation" />
    ///</summary>
    MyTelegram.Schema.IBusinessLocation? BusinessLocation { get; set; }

    ///<summary>
    /// <a href="https://corefork.telegram.org/api/business#greeting-messages">Telegram Business greeting message »</a>.
    /// See <a href="https://corefork.telegram.org/type/BusinessGreetingMessage" />
    ///</summary>
    MyTelegram.Schema.IBusinessGreetingMessage? BusinessGreetingMessage { get; set; }

    ///<summary>
    /// <a href="https://corefork.telegram.org/api/business#away-messages">Telegram Business away message »</a>.
    /// See <a href="https://corefork.telegram.org/type/BusinessAwayMessage" />
    ///</summary>
    MyTelegram.Schema.IBusinessAwayMessage? BusinessAwayMessage { get; set; }

    ///<summary>
    /// Specifies a custom <a href="https://corefork.telegram.org/api/business#business-introduction">Telegram Business profile introduction »</a>.
    /// See <a href="https://corefork.telegram.org/type/BusinessIntro" />
    ///</summary>
    MyTelegram.Schema.IBusinessIntro? BusinessIntro { get; set; }

    ///<summary>
    /// Contains info about the user's <a href="https://corefork.telegram.org/api/profile#birthday">birthday »</a>.
    /// See <a href="https://corefork.telegram.org/type/Birthday" />
    ///</summary>
    MyTelegram.Schema.IBirthday? Birthday { get; set; }

    ///<summary>
    /// ID of the associated personal <a href="https://corefork.telegram.org/api/channel">channel »</a>, that should be shown in the <a href="https://corefork.telegram.org/api/profile#personal-channel">profile page</a>.
    ///</summary>
    long? PersonalChannelId { get; set; }

    ///<summary>
    /// ID of the latest message of the associated personal <a href="https://corefork.telegram.org/api/channel">channel »</a>, that should be previewed in the <a href="https://corefork.telegram.org/api/profile#personal-channel">profile page</a>.
    ///</summary>
    int? PersonalChannelMessage { get; set; }

    ///<summary>
    /// Number of <a href="https://corefork.telegram.org/api/gifts">gifts</a> the user has chosen to display on their profile
    ///</summary>
    int? StargiftsCount { get; set; }

    ///<summary>
    /// &nbsp;
    /// See <a href="https://corefork.telegram.org/type/StarRefProgram" />
    ///</summary>
    MyTelegram.Schema.IStarRefProgram? StarrefProgram { get; set; }
}
