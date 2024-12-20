// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Represents a <a href="https://corefork.telegram.org/api/gifts">star gift, see here »</a> for more info.
/// See <a href="https://corefork.telegram.org/constructor/StarGift" />
///</summary>
[JsonDerivedType(typeof(TStarGift), nameof(TStarGift))]
public interface IStarGift : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// Whether this is a limited-supply gift.
    ///</summary>
    bool Limited { get; set; }

    ///<summary>
    /// Whether this gift sold out and cannot be bought anymore.
    ///</summary>
    bool SoldOut { get; set; }

    ///<summary>
    /// &nbsp;
    ///</summary>
    bool Birthday { get; set; }

    ///<summary>
    /// Identifier of the gift
    ///</summary>
    long Id { get; set; }

    ///<summary>
    /// <a href="https://corefork.telegram.org/api/stickers">Sticker</a> that represents the gift.
    /// See <a href="https://corefork.telegram.org/type/Document" />
    ///</summary>
    MyTelegram.Schema.IDocument Sticker { get; set; }

    ///<summary>
    /// Price of the gift in <a href="https://corefork.telegram.org/api/stars">Telegram Stars</a>.
    ///</summary>
    long Stars { get; set; }

    ///<summary>
    /// For limited-supply gifts: the remaining number of gifts that may be bought.
    ///</summary>
    int? AvailabilityRemains { get; set; }

    ///<summary>
    /// For limited-supply gifts: the total number of gifts that was available in the initial supply.
    ///</summary>
    int? AvailabilityTotal { get; set; }

    ///<summary>
    /// The receiver of this gift may convert it to this many Telegram Stars, instead of displaying it on their profile page.<br><code>convert_stars</code> will be equal to <code>stars</code> only if the gift was bought using recently bought Telegram Stars, otherwise it will be less than <code>stars</code>.
    ///</summary>
    long ConvertStars { get; set; }

    ///<summary>
    /// For sold out gifts only: when was the gift first bought.
    ///</summary>
    int? FirstSaleDate { get; set; }

    ///<summary>
    /// For sold out gifts only: when was the gift last bought.
    ///</summary>
    int? LastSaleDate { get; set; }
}
