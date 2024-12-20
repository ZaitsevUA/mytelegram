﻿// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// <a href="https://corefork.telegram.org/api/reactions">Message reactions »</a>
/// See <a href="https://corefork.telegram.org/constructor/MessageReactions" />
///</summary>
[JsonDerivedType(typeof(TMessageReactions), nameof(TMessageReactions))]
public interface IMessageReactions : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// Similar to <a href="https://corefork.telegram.org/api/min">min</a> objects, used for <a href="https://corefork.telegram.org/api/reactions">message reaction »</a> constructors that are the same for all users so they don't have the reactions sent by the current user (you can use <a href="https://corefork.telegram.org/method/messages.getMessagesReactions">messages.getMessagesReactions</a> to get the full reaction info).
    ///</summary>
    bool Min { get; set; }

    ///<summary>
    /// Whether <a href="https://corefork.telegram.org/method/messages.getMessageReactionsList">messages.getMessageReactionsList</a> can be used to see how each specific peer reacted to the message
    ///</summary>
    bool CanSeeList { get; set; }

    ///<summary>
    /// If set or if there are no reactions, all present and future reactions should be treated as <a href="https://corefork.telegram.org/api/saved-messages#tags">message tags, see here » for more info</a>.
    ///</summary>
    bool ReactionsAsTags { get; set; }

    ///<summary>
    /// Reactions
    /// See <a href="https://corefork.telegram.org/type/ReactionCount" />
    ///</summary>
    TVector<MyTelegram.Schema.IReactionCount> Results { get; set; }

    ///<summary>
    /// List of recent peers and their reactions
    /// See <a href="https://corefork.telegram.org/type/MessagePeerReaction" />
    ///</summary>
    TVector<MyTelegram.Schema.IMessagePeerReaction>? RecentReactions { get; set; }

    ///<summary>
    /// <a href="https://corefork.telegram.org/api/reactions#paid-reactions">Paid Telegram Star reactions leaderboard »</a> for this message.
    /// See <a href="https://corefork.telegram.org/type/MessageReactor" />
    ///</summary>
    TVector<MyTelegram.Schema.IMessageReactor>? TopReactors { get; set; }
}
