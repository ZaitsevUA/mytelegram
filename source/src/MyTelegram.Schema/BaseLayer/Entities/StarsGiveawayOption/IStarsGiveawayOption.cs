// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/StarsGiveawayOption" />
///</summary>
[JsonDerivedType(typeof(TStarsGiveawayOption), nameof(TStarsGiveawayOption))]
public interface IStarsGiveawayOption : IObject
{
    BitArray Flags { get; set; }
    bool Extended { get; set; }
    bool Default { get; set; }
    long Stars { get; set; }
    int YearlyBoosts { get; set; }
    string? StoreProduct { get; set; }
    string Currency { get; set; }
    long Amount { get; set; }
    TVector<MyTelegram.Schema.IStarsGiveawayWinnersOption> Winners { get; set; }
}
