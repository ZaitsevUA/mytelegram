// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/StarsGiveawayWinnersOption" />
///</summary>
[JsonDerivedType(typeof(TStarsGiveawayWinnersOption), nameof(TStarsGiveawayWinnersOption))]
public interface IStarsGiveawayWinnersOption : IObject
{
    BitArray Flags { get; set; }
    bool Default { get; set; }
    int Users { get; set; }
    long PerUserStars { get; set; }
}
