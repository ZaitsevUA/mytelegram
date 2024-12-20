﻿// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Represents a possible option for the number of winners in a star giveaway
/// See <a href="https://corefork.telegram.org/constructor/StarsGiveawayWinnersOption" />
///</summary>
[JsonDerivedType(typeof(TStarsGiveawayWinnersOption), nameof(TStarsGiveawayWinnersOption))]
public interface IStarsGiveawayWinnersOption : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// If set, this option must be pre-selected by default in the option list.
    ///</summary>
    bool Default { get; set; }

    ///<summary>
    /// The number of users that will be randomly chosen as winners.
    ///</summary>
    int Users { get; set; }

    ///<summary>
    /// The number of <a href="https://corefork.telegram.org/api/stars">Telegram Stars</a> each winner will receive.
    ///</summary>
    long PerUserStars { get; set; }
}
