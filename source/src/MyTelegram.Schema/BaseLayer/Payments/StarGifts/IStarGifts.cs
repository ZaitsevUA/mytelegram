// ReSharper disable All

namespace MyTelegram.Schema.Payments;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/payments.StarGifts" />
///</summary>
[JsonDerivedType(typeof(TStarGiftsNotModified), nameof(TStarGiftsNotModified))]
[JsonDerivedType(typeof(TStarGifts), nameof(TStarGifts))]
public interface IStarGifts : IObject
{

}
