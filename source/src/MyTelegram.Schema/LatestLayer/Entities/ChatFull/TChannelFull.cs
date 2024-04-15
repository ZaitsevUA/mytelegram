﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Full info about a <a href="https://corefork.telegram.org/api/channel#channels">channel</a>, <a href="https://corefork.telegram.org/api/channel#supergroups">supergroup</a> or <a href="https://corefork.telegram.org/api/channel#gigagroups">gigagroup</a>.
/// See <a href="https://corefork.telegram.org/constructor/channelFull" />
///</summary>
[TlObject(0x44c054a7)]
public sealed class TChannelFull : MyTelegram.Schema.IChatFull, ILayeredChannelFull
{
    public uint ConstructorId => 0x44c054a7;
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    public BitArray Flags { get; set; } = new BitArray(32);

    ///<summary>
    /// Can we view the participant list?
    /// See <a href="https://corefork.telegram.org/type/true" />
    ///</summary>
    public bool CanViewParticipants { get; set; }

    ///<summary>
    /// Can we set the channel's username?
    /// See <a href="https://corefork.telegram.org/type/true" />
    ///</summary>
    public bool CanSetUsername { get; set; }

    ///<summary>
    /// Can we <a href="https://corefork.telegram.org/method/channels.setStickers">associate</a> a stickerpack to the supergroup?
    /// See <a href="https://corefork.telegram.org/type/true" />
    ///</summary>
    public bool CanSetStickers { get; set; }

    ///<summary>
    /// Is the history before we joined hidden to us?
    /// See <a href="https://corefork.telegram.org/type/true" />
    ///</summary>
    public bool HiddenPrehistory { get; set; }

    ///<summary>
    /// Can we set the geolocation of this group (for geogroups)
    /// See <a href="https://corefork.telegram.org/type/true" />
    ///</summary>
    public bool CanSetLocation { get; set; }

    ///<summary>
    /// Whether scheduled messages are available
    /// See <a href="https://corefork.telegram.org/type/true" />
    ///</summary>
    public bool HasScheduled { get; set; }

    ///<summary>
    /// Can the user view <a href="https://corefork.telegram.org/api/stats">channel/supergroup statistics</a>
    /// See <a href="https://corefork.telegram.org/type/true" />
    ///</summary>
    public bool CanViewStats { get; set; }

    ///<summary>
    /// Whether any anonymous admin of this supergroup was blocked: if set, you won't receive messages from anonymous group admins in <a href="https://corefork.telegram.org/api/discussion">discussion replies via @replies</a>
    /// See <a href="https://corefork.telegram.org/type/true" />
    ///</summary>
    public bool Blocked { get; set; }

    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    public BitArray Flags2 { get; set; } = new BitArray(32);

    ///<summary>
    /// Can we delete this channel?
    /// See <a href="https://corefork.telegram.org/type/true" />
    ///</summary>
    public bool CanDeleteChannel { get; set; }

    ///<summary>
    /// Whether <a href="https://corefork.telegram.org/api/antispam">native antispam</a> functionality is enabled in this supergroup.
    /// See <a href="https://corefork.telegram.org/type/true" />
    ///</summary>
    public bool Antispam { get; set; }

    ///<summary>
    /// Whether the participant list is hidden.
    /// See <a href="https://corefork.telegram.org/type/true" />
    ///</summary>
    public bool ParticipantsHidden { get; set; }

    ///<summary>
    /// Whether the <a href="https://corefork.telegram.org/api/translation">real-time chat translation popup</a> should be hidden.
    /// See <a href="https://corefork.telegram.org/type/true" />
    ///</summary>
    public bool TranslationsDisabled { get; set; }

    ///<summary>
    /// Whether this user has some <a href="https://corefork.telegram.org/api/stories#pinned-or-archived-stories">pinned stories</a>.
    /// See <a href="https://corefork.telegram.org/type/true" />
    ///</summary>
    public bool StoriesPinnedAvailable { get; set; }

    ///<summary>
    /// Users may also choose to display messages from all topics of a <a href="https://corefork.telegram.org/api/forum">forum</a> as if they were sent to a normal group, using a "View as messages" setting in the local client.  <br>This setting only affects the current account, and is synced to other logged in sessions using the <a href="https://corefork.telegram.org/method/channels.toggleViewForumAsMessages">channels.toggleViewForumAsMessages</a> method; invoking this method will update the value of this flag.
    /// See <a href="https://corefork.telegram.org/type/true" />
    ///</summary>
    public bool ViewForumAsMessages { get; set; }
    public bool RestrictedSponsored { get; set; }
    public bool CanViewRevenue { get; set; }

    ///<summary>
    /// ID of the channel
    ///</summary>
    public long Id { get; set; }

    ///<summary>
    /// Info about the channel
    ///</summary>
    public string About { get; set; }

    ///<summary>
    /// Number of participants of the channel
    ///</summary>
    public int? ParticipantsCount { get; set; }

    ///<summary>
    /// Number of channel admins
    ///</summary>
    public int? AdminsCount { get; set; }

    ///<summary>
    /// Number of users <a href="https://corefork.telegram.org/api/rights">kicked</a> from the channel
    ///</summary>
    public int? KickedCount { get; set; }

    ///<summary>
    /// Number of users <a href="https://corefork.telegram.org/api/rights">banned</a> from the channel
    ///</summary>
    public int? BannedCount { get; set; }

    ///<summary>
    /// Number of users currently online
    ///</summary>
    public int? OnlineCount { get; set; }

    ///<summary>
    /// Position up to which all incoming messages are read.
    ///</summary>
    public int ReadInboxMaxId { get; set; }

    ///<summary>
    /// Position up to which all outgoing messages are read.
    ///</summary>
    public int ReadOutboxMaxId { get; set; }

    ///<summary>
    /// Count of unread messages
    ///</summary>
    public int UnreadCount { get; set; }

    ///<summary>
    /// Channel picture
    /// See <a href="https://corefork.telegram.org/type/Photo" />
    ///</summary>
    public MyTelegram.Schema.IPhoto ChatPhoto { get; set; }

    ///<summary>
    /// Notification settings
    /// See <a href="https://corefork.telegram.org/type/PeerNotifySettings" />
    ///</summary>
    public MyTelegram.Schema.IPeerNotifySettings NotifySettings { get; set; }

    ///<summary>
    /// Invite link
    /// See <a href="https://corefork.telegram.org/type/ExportedChatInvite" />
    ///</summary>
    public MyTelegram.Schema.IExportedChatInvite? ExportedInvite { get; set; }

    ///<summary>
    /// Info about bots in the channel/supergroup
    ///</summary>
    public TVector<MyTelegram.Schema.IBotInfo> BotInfo { get; set; }

    ///<summary>
    /// The chat ID from which this group was <a href="https://corefork.telegram.org/api/channel">migrated</a>
    ///</summary>
    public long? MigratedFromChatId { get; set; }

    ///<summary>
    /// The message ID in the original chat at which this group was <a href="https://corefork.telegram.org/api/channel">migrated</a>
    ///</summary>
    public int? MigratedFromMaxId { get; set; }

    ///<summary>
    /// Message ID of the last <a href="https://corefork.telegram.org/api/pin">pinned message</a>
    ///</summary>
    public int? PinnedMsgId { get; set; }

    ///<summary>
    /// Associated stickerset
    /// See <a href="https://corefork.telegram.org/type/StickerSet" />
    ///</summary>
    public MyTelegram.Schema.IStickerSet? Stickerset { get; set; }

    ///<summary>
    /// Identifier of a maximum unavailable message in a channel due to hidden history.
    ///</summary>
    public int? AvailableMinId { get; set; }

    ///<summary>
    /// <a href="https://corefork.telegram.org/api/folders#peer-folders">Peer folder ID, for more info click here</a>
    ///</summary>
    public int? FolderId { get; set; }

    ///<summary>
    /// ID of the linked <a href="https://corefork.telegram.org/api/discussion">discussion chat</a> for channels
    ///</summary>
    public long? LinkedChatId { get; set; }

    ///<summary>
    /// Location of the geogroup
    /// See <a href="https://corefork.telegram.org/type/ChannelLocation" />
    ///</summary>
    public MyTelegram.Schema.IChannelLocation? Location { get; set; }

    ///<summary>
    /// If specified, users in supergroups will only be able to send one message every <code>slowmode_seconds</code> seconds
    ///</summary>
    public int? SlowmodeSeconds { get; set; }

    ///<summary>
    /// Indicates when the user will be allowed to send another message in the supergroup (unixtime)
    ///</summary>
    public int? SlowmodeNextSendDate { get; set; }

    ///<summary>
    /// If set, specifies the DC to use for fetching channel statistics
    ///</summary>
    public int? StatsDc { get; set; }

    ///<summary>
    /// Latest <a href="https://corefork.telegram.org/api/updates">PTS</a> for this channel
    ///</summary>
    public int Pts { get; set; }

    ///<summary>
    /// Livestream or group call information
    /// See <a href="https://corefork.telegram.org/type/InputGroupCall" />
    ///</summary>
    public MyTelegram.Schema.IInputGroupCall? Call { get; set; }

    ///<summary>
    /// Time-To-Live of messages in this channel or supergroup
    ///</summary>
    public int? TtlPeriod { get; set; }

    ///<summary>
    /// A list of <a href="https://corefork.telegram.org/api/config#suggestions">suggested actions</a> for the supergroup admin, <a href="https://corefork.telegram.org/api/config#suggestions">see here for more info »</a>.
    ///</summary>
    public TVector<string>? PendingSuggestions { get; set; }

    ///<summary>
    /// When using <a href="https://corefork.telegram.org/method/phone.getGroupCallJoinAs">phone.getGroupCallJoinAs</a> to get a list of peers that can be used to join a group call, this field indicates the peer that should be selected by default.
    /// See <a href="https://corefork.telegram.org/type/Peer" />
    ///</summary>
    public MyTelegram.Schema.IPeer? GroupcallDefaultJoinAs { get; set; }

    ///<summary>
    /// Emoji representing a specific chat theme
    ///</summary>
    public string? ThemeEmoticon { get; set; }

    ///<summary>
    /// Pending <a href="https://corefork.telegram.org/api/invites#join-requests">join requests »</a>
    ///</summary>
    public int? RequestsPending { get; set; }

    ///<summary>
    /// IDs of users who requested to join recently
    ///</summary>
    public TVector<long>? RecentRequesters { get; set; }

    ///<summary>
    /// Default peer used for sending messages to this channel
    /// See <a href="https://corefork.telegram.org/type/Peer" />
    ///</summary>
    public MyTelegram.Schema.IPeer? DefaultSendAs { get; set; }

    ///<summary>
    /// Allowed <a href="https://corefork.telegram.org/api/reactions">message reactions »</a>
    /// See <a href="https://corefork.telegram.org/type/ChatReactions" />
    ///</summary>
    public MyTelegram.Schema.IChatReactions? AvailableReactions { get; set; }

    ///<summary>
    /// Channel <a href="https://corefork.telegram.org/api/stories">stories</a>
    /// See <a href="https://corefork.telegram.org/type/PeerStories" />
    ///</summary>
    public MyTelegram.Schema.IPeerStories? Stories { get; set; }

    ///<summary>
    /// <a href="https://corefork.telegram.org/api/wallpapers">Wallpaper</a>
    /// See <a href="https://corefork.telegram.org/type/WallPaper" />
    ///</summary>
    public MyTelegram.Schema.IWallPaper? Wallpaper { get; set; }
    public int? BoostsApplied { get; set; }
    public int? BoostsUnrestrict { get; set; }
    public MyTelegram.Schema.IStickerSet? Emojiset { get; set; }

    public void ComputeFlag()
    {
        if (CanViewParticipants) { Flags[3] = true; }
        if (CanSetUsername) { Flags[6] = true; }
        if (CanSetStickers) { Flags[7] = true; }
        if (HiddenPrehistory) { Flags[10] = true; }
        if (CanSetLocation) { Flags[16] = true; }
        if (HasScheduled) { Flags[19] = true; }
        if (CanViewStats) { Flags[20] = true; }
        if (Blocked) { Flags[22] = true; }
        if (CanDeleteChannel) { Flags2[0] = true; }
        if (Antispam) { Flags2[1] = true; }
        if (ParticipantsHidden) { Flags2[2] = true; }
        if (TranslationsDisabled) { Flags2[3] = true; }
        if (StoriesPinnedAvailable) { Flags2[5] = true; }
        if (ViewForumAsMessages) { Flags2[6] = true; }
        if (RestrictedSponsored) { Flags2[11] = true; }
        if (CanViewRevenue) { Flags2[12] = true; }
        if (/*ParticipantsCount != 0 && */ParticipantsCount.HasValue) { Flags[0] = true; }
        if (/*AdminsCount != 0 && */AdminsCount.HasValue) { Flags[1] = true; }
        if (/*KickedCount != 0 && */KickedCount.HasValue) { Flags[2] = true; }
        if (/*BannedCount != 0 && */BannedCount.HasValue) { Flags[2] = true; }
        if (/*OnlineCount != 0 && */OnlineCount.HasValue) { Flags[13] = true; }
        if (ExportedInvite != null) { Flags[23] = true; }
        if (/*MigratedFromChatId != 0 &&*/ MigratedFromChatId.HasValue) { Flags[4] = true; }
        if (/*MigratedFromMaxId != 0 && */MigratedFromMaxId.HasValue) { Flags[4] = true; }
        if (/*PinnedMsgId != 0 && */PinnedMsgId.HasValue) { Flags[5] = true; }
        if (Stickerset != null) { Flags[8] = true; }
        if (/*AvailableMinId != 0 && */AvailableMinId.HasValue) { Flags[9] = true; }
        if (/*FolderId != 0 && */FolderId.HasValue) { Flags[11] = true; }
        if (/*LinkedChatId != 0 &&*/ LinkedChatId.HasValue) { Flags[14] = true; }
        if (Location != null) { Flags[15] = true; }
        if (/*SlowmodeSeconds != 0 && */SlowmodeSeconds.HasValue) { Flags[17] = true; }
        if (/*SlowmodeNextSendDate != 0 && */SlowmodeNextSendDate.HasValue) { Flags[18] = true; }
        if (/*StatsDc != 0 && */StatsDc.HasValue) { Flags[12] = true; }
        if (Call != null) { Flags[21] = true; }
        if (/*TtlPeriod != 0 && */TtlPeriod.HasValue) { Flags[24] = true; }
        if (PendingSuggestions?.Count > 0) { Flags[25] = true; }
        if (GroupcallDefaultJoinAs != null) { Flags[26] = true; }
        if (ThemeEmoticon != null) { Flags[27] = true; }
        if (/*RequestsPending != 0 && */RequestsPending.HasValue) { Flags[28] = true; }
        if (RecentRequesters?.Count > 0) { Flags[28] = true; }
        if (DefaultSendAs != null) { Flags[29] = true; }
        if (AvailableReactions != null) { Flags[30] = true; }
        if (Stories != null) { Flags2[4] = true; }
        if (Wallpaper != null) { Flags2[7] = true; }
        if (/*BoostsApplied != 0 && */BoostsApplied.HasValue) { Flags2[8] = true; }
        if (/*BoostsUnrestrict != 0 && */BoostsUnrestrict.HasValue) { Flags2[9] = true; }
        if (Emojiset != null) { Flags2[10] = true; }
    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Flags);
        writer.Write(Flags2);
        writer.Write(Id);
        writer.Write(About);
        if (Flags[0]) { writer.Write(ParticipantsCount.Value); }
        if (Flags[1]) { writer.Write(AdminsCount.Value); }
        if (Flags[2]) { writer.Write(KickedCount.Value); }
        if (Flags[2]) { writer.Write(BannedCount.Value); }
        if (Flags[13]) { writer.Write(OnlineCount.Value); }
        writer.Write(ReadInboxMaxId);
        writer.Write(ReadOutboxMaxId);
        writer.Write(UnreadCount);
        writer.Write(ChatPhoto);
        writer.Write(NotifySettings);
        if (Flags[23]) { writer.Write(ExportedInvite); }
        writer.Write(BotInfo);
        if (Flags[4]) { writer.Write(MigratedFromChatId.Value); }
        if (Flags[4]) { writer.Write(MigratedFromMaxId.Value); }
        if (Flags[5]) { writer.Write(PinnedMsgId.Value); }
        if (Flags[8]) { writer.Write(Stickerset); }
        if (Flags[9]) { writer.Write(AvailableMinId.Value); }
        if (Flags[11]) { writer.Write(FolderId.Value); }
        if (Flags[14]) { writer.Write(LinkedChatId.Value); }
        if (Flags[15]) { writer.Write(Location); }
        if (Flags[17]) { writer.Write(SlowmodeSeconds.Value); }
        if (Flags[18]) { writer.Write(SlowmodeNextSendDate.Value); }
        if (Flags[12]) { writer.Write(StatsDc.Value); }
        writer.Write(Pts);
        if (Flags[21]) { writer.Write(Call); }
        if (Flags[24]) { writer.Write(TtlPeriod.Value); }
        if (Flags[25]) { writer.Write(PendingSuggestions); }
        if (Flags[26]) { writer.Write(GroupcallDefaultJoinAs); }
        if (Flags[27]) { writer.Write(ThemeEmoticon); }
        if (Flags[28]) { writer.Write(RequestsPending.Value); }
        if (Flags[28]) { writer.Write(RecentRequesters); }
        if (Flags[29]) { writer.Write(DefaultSendAs); }
        if (Flags[30]) { writer.Write(AvailableReactions); }
        if (Flags2[4]) { writer.Write(Stories); }
        if (Flags2[7]) { writer.Write(Wallpaper); }
        if (Flags2[8]) { writer.Write(BoostsApplied.Value); }
        if (Flags2[9]) { writer.Write(BoostsUnrestrict.Value); }
        if (Flags2[10]) { writer.Write(Emojiset); }
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Flags = reader.ReadBitArray();
        if (Flags[3]) { CanViewParticipants = true; }
        if (Flags[6]) { CanSetUsername = true; }
        if (Flags[7]) { CanSetStickers = true; }
        if (Flags[10]) { HiddenPrehistory = true; }
        if (Flags[16]) { CanSetLocation = true; }
        if (Flags[19]) { HasScheduled = true; }
        if (Flags[20]) { CanViewStats = true; }
        if (Flags[22]) { Blocked = true; }
        Flags2 = reader.ReadBitArray();
        if (Flags2[0]) { CanDeleteChannel = true; }
        if (Flags2[1]) { Antispam = true; }
        if (Flags2[2]) { ParticipantsHidden = true; }
        if (Flags2[3]) { TranslationsDisabled = true; }
        if (Flags2[5]) { StoriesPinnedAvailable = true; }
        if (Flags2[6]) { ViewForumAsMessages = true; }
        if (Flags2[11]) { RestrictedSponsored = true; }
        if (Flags2[12]) { CanViewRevenue = true; }
        Id = reader.ReadInt64();
        About = reader.ReadString();
        if (Flags[0]) { ParticipantsCount = reader.ReadInt32(); }
        if (Flags[1]) { AdminsCount = reader.ReadInt32(); }
        if (Flags[2]) { KickedCount = reader.ReadInt32(); }
        if (Flags[2]) { BannedCount = reader.ReadInt32(); }
        if (Flags[13]) { OnlineCount = reader.ReadInt32(); }
        ReadInboxMaxId = reader.ReadInt32();
        ReadOutboxMaxId = reader.ReadInt32();
        UnreadCount = reader.ReadInt32();
        ChatPhoto = reader.Read<MyTelegram.Schema.IPhoto>();
        NotifySettings = reader.Read<MyTelegram.Schema.IPeerNotifySettings>();
        if (Flags[23]) { ExportedInvite = reader.Read<MyTelegram.Schema.IExportedChatInvite>(); }
        BotInfo = reader.Read<TVector<MyTelegram.Schema.IBotInfo>>();
        if (Flags[4]) { MigratedFromChatId = reader.ReadInt64(); }
        if (Flags[4]) { MigratedFromMaxId = reader.ReadInt32(); }
        if (Flags[5]) { PinnedMsgId = reader.ReadInt32(); }
        if (Flags[8]) { Stickerset = reader.Read<MyTelegram.Schema.IStickerSet>(); }
        if (Flags[9]) { AvailableMinId = reader.ReadInt32(); }
        if (Flags[11]) { FolderId = reader.ReadInt32(); }
        if (Flags[14]) { LinkedChatId = reader.ReadInt64(); }
        if (Flags[15]) { Location = reader.Read<MyTelegram.Schema.IChannelLocation>(); }
        if (Flags[17]) { SlowmodeSeconds = reader.ReadInt32(); }
        if (Flags[18]) { SlowmodeNextSendDate = reader.ReadInt32(); }
        if (Flags[12]) { StatsDc = reader.ReadInt32(); }
        Pts = reader.ReadInt32();
        if (Flags[21]) { Call = reader.Read<MyTelegram.Schema.IInputGroupCall>(); }
        if (Flags[24]) { TtlPeriod = reader.ReadInt32(); }
        if (Flags[25]) { PendingSuggestions = reader.Read<TVector<string>>(); }
        if (Flags[26]) { GroupcallDefaultJoinAs = reader.Read<MyTelegram.Schema.IPeer>(); }
        if (Flags[27]) { ThemeEmoticon = reader.ReadString(); }
        if (Flags[28]) { RequestsPending = reader.ReadInt32(); }
        if (Flags[28]) { RecentRequesters = reader.Read<TVector<long>>(); }
        if (Flags[29]) { DefaultSendAs = reader.Read<MyTelegram.Schema.IPeer>(); }
        if (Flags[30]) { AvailableReactions = reader.Read<MyTelegram.Schema.IChatReactions>(); }
        if (Flags2[4]) { Stories = reader.Read<MyTelegram.Schema.IPeerStories>(); }
        if (Flags2[7]) { Wallpaper = reader.Read<MyTelegram.Schema.IWallPaper>(); }
        if (Flags2[8]) { BoostsApplied = reader.ReadInt32(); }
        if (Flags2[9]) { BoostsUnrestrict = reader.ReadInt32(); }
        if (Flags2[10]) { Emojiset = reader.Read<MyTelegram.Schema.IStickerSet>(); }
    }
}