// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/StarsTopupOption" />
///</summary>
[JsonDerivedType(typeof(TStarsTopupOption), nameof(TStarsTopupOption))]
public interface IStarsTopupOption : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// &nbsp;
    ///</summary>
    bool Extended { get; set; }

    ///<summary>
    /// &nbsp;
    ///</summary>
    long Stars { get; set; }

    ///<summary>
    /// &nbsp;
    ///</summary>
    string? StoreProduct { get; set; }

    ///<summary>
    /// &nbsp;
    ///</summary>
    string Currency { get; set; }

    ///<summary>
    /// &nbsp;
    ///</summary>
    long Amount { get; set; }
}
