// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/Birthday" />
///</summary>
[JsonDerivedType(typeof(TBirthday), nameof(TBirthday))]
public interface IBirthday : IObject
{
    BitArray Flags { get; set; }
    int Day { get; set; }
    int Month { get; set; }
    int? Year { get; set; }
}
