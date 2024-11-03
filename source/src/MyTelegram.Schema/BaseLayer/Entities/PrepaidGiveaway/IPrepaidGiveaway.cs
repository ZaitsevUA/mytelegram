// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Contains info about a <a href="https://corefork.telegram.org/api/giveaways">prepaid giveaway »</a>.
/// See <a href="https://corefork.telegram.org/constructor/PrepaidGiveaway" />
///</summary>
[JsonDerivedType(typeof(TPrepaidGiveaway), nameof(TPrepaidGiveaway))]
[JsonDerivedType(typeof(TPrepaidStarsGiveaway), nameof(TPrepaidStarsGiveaway))]
public interface IPrepaidGiveaway : IObject
{
    long Id { get; set; }
    int Quantity { get; set; }
    int Date { get; set; }
}
