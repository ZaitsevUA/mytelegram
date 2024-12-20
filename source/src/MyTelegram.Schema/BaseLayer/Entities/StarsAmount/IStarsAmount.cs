// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/StarsAmount" />
///</summary>
[JsonDerivedType(typeof(TStarsAmount), nameof(TStarsAmount))]
public interface IStarsAmount : IObject
{
    ///<summary>
    /// &nbsp;
    ///</summary>
    long Amount { get; set; }

    ///<summary>
    /// &nbsp;
    ///</summary>
    int Nanos { get; set; }
}
