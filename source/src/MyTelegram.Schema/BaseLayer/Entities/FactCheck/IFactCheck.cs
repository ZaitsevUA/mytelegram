// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/FactCheck" />
///</summary>
[JsonDerivedType(typeof(TFactCheck), nameof(TFactCheck))]
public interface IFactCheck : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// &nbsp;
    ///</summary>
    bool NeedCheck { get; set; }

    ///<summary>
    /// &nbsp;
    ///</summary>
    string? Country { get; set; }

    ///<summary>
    /// &nbsp;
    /// See <a href="https://corefork.telegram.org/type/TextWithEntities" />
    ///</summary>
    MyTelegram.Schema.ITextWithEntities? Text { get; set; }

    ///<summary>
    /// <a href="https://corefork.telegram.org/api/offsets#hash-generation">Hash for pagination, for more info click here</a>
    ///</summary>
    long Hash { get; set; }
}
