// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/AvailableEffect" />
///</summary>
[JsonDerivedType(typeof(TAvailableEffect), nameof(TAvailableEffect))]
public interface IAvailableEffect : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// &nbsp;
    ///</summary>
    bool PremiumRequired { get; set; }

    ///<summary>
    /// &nbsp;
    ///</summary>
    long Id { get; set; }

    ///<summary>
    /// &nbsp;
    ///</summary>
    string Emoticon { get; set; }

    ///<summary>
    /// &nbsp;
    ///</summary>
    long? StaticIconId { get; set; }

    ///<summary>
    /// &nbsp;
    ///</summary>
    long EffectStickerId { get; set; }

    ///<summary>
    /// &nbsp;
    ///</summary>
    long? EffectAnimationId { get; set; }
}
