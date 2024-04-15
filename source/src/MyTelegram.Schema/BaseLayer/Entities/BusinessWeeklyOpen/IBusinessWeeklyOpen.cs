// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/BusinessWeeklyOpen" />
///</summary>
[JsonDerivedType(typeof(TBusinessWeeklyOpen), nameof(TBusinessWeeklyOpen))]
public interface IBusinessWeeklyOpen : IObject
{
    int StartMinute { get; set; }
    int EndMinute { get; set; }
}
