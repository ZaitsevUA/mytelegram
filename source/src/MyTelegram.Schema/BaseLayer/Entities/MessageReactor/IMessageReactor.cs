﻿// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Info about a user in the <a href="https://corefork.telegram.org/api/reactions#paid-reactions">paid Star reactions leaderboard</a> for a message.
/// See <a href="https://corefork.telegram.org/constructor/MessageReactor" />
///</summary>
[JsonDerivedType(typeof(TMessageReactor), nameof(TMessageReactor))]
public interface IMessageReactor : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// If set, the reactor is one of the most active reactors; may be unset if the reactor is the current user.
    ///</summary>
    bool Top { get; set; }

    ///<summary>
    /// If set, this reactor is the current user.
    ///</summary>
    bool My { get; set; }

    ///<summary>
    /// If set, the reactor is anonymous.
    ///</summary>
    bool Anonymous { get; set; }

    ///<summary>
    /// Identifier of the peer that reacted: may be unset for anonymous reactors different from the current user (i.e. if the current user sent an anonymous reaction <code>anonymous</code> will be set but this field will also be set).
    /// See <a href="https://corefork.telegram.org/type/Peer" />
    ///</summary>
    MyTelegram.Schema.IPeer? PeerId { get; set; }

    ///<summary>
    /// The number of sent Telegram Stars.
    ///</summary>
    int Count { get; set; }
}
