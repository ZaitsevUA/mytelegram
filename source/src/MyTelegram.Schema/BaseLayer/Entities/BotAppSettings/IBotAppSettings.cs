// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/BotAppSettings" />
///</summary>
[JsonDerivedType(typeof(TBotAppSettings), nameof(TBotAppSettings))]
public interface IBotAppSettings : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// &nbsp;
    ///</summary>
    byte[]? PlaceholderPath { get; set; }

    ///<summary>
    /// &nbsp;
    ///</summary>
    int? BackgroundColor { get; set; }

    ///<summary>
    /// &nbsp;
    ///</summary>
    int? BackgroundDarkColor { get; set; }

    ///<summary>
    /// &nbsp;
    ///</summary>
    int? HeaderColor { get; set; }

    ///<summary>
    /// &nbsp;
    ///</summary>
    int? HeaderDarkColor { get; set; }
}
