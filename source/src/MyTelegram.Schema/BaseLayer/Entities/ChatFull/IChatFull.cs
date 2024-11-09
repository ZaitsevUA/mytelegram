// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Full info about a <a href="https://corefork.telegram.org/api/channel#channels">channel</a>, <a href="https://corefork.telegram.org/api/channel#supergroups">supergroup</a>, <a href="https://corefork.telegram.org/api/channel#gigagroups">gigagroup</a> or <a href="https://corefork.telegram.org/api/channel#basic-groups">basic group</a>.
/// See <a href="https://corefork.telegram.org/constructor/ChatFull" />
///</summary>
[JsonDerivedType(typeof(TChatFull), nameof(TChatFull))]
[JsonDerivedType(typeof(TChannelFull), nameof(TChannelFull))]
public interface IChatFull : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// ID of the channel
    ///</summary>
    long Id { get; set; }

    ///<summary>
    /// Info about the channel
    ///</summary>
    string About { get; set; }

    ///<summary>
    /// Notification settings
    /// See <a href="https://corefork.telegram.org/type/PeerNotifySettings" />
    ///</summary>
    MyTelegram.Schema.IPeerNotifySettings NotifySettings { get; set; }

    ///<summary>
    /// <a href="https://corefork.telegram.org/api/folders#peer-folders">Peer folder ID, for more info click here</a>
    ///</summary>
    int? FolderId { get; set; }

    TVector<MyTelegram.Schema.IBotInfo> BotInfo { get; set; }

    ///<summary>
    /// Group call information
    /// See <a href="https://corefork.telegram.org/type/InputGroupCall" />
    ///</summary>
    MyTelegram.Schema.IInputGroupCall? Call { get; set; }

    ///<summary>
    /// When using <a href="https://corefork.telegram.org/method/phone.getGroupCallJoinAs">phone.getGroupCallJoinAs</a> to get a list of peers that can be used to join a group call, this field indicates the peer that should be selected by default.
    /// See <a href="https://corefork.telegram.org/type/Peer" />
    ///</summary>
    MyTelegram.Schema.IPeer? GroupcallDefaultJoinAs { get; set; }
}
