// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/StarGift" />
///</summary>
[JsonDerivedType(typeof(TStarGift), nameof(TStarGift))]
public interface IStarGift : IObject
{
    BitArray Flags { get; set; }
    bool Limited { get; set; }
    bool SoldOut { get; set; }
    long Id { get; set; }
    MyTelegram.Schema.IDocument Sticker { get; set; }
    long Stars { get; set; }
    int? AvailabilityRemains { get; set; }
    int? AvailabilityTotal { get; set; }
    long ConvertStars { get; set; }
    int? FirstSaleDate { get; set; }
    int? LastSaleDate { get; set; }
}
