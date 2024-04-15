// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/BusinessWorkHours" />
///</summary>
[JsonDerivedType(typeof(TBusinessWorkHours), nameof(TBusinessWorkHours))]
public interface IBusinessWorkHours : IObject
{
    BitArray Flags { get; set; }
    bool OpenNow { get; set; }
    string TimezoneId { get; set; }
    TVector<MyTelegram.Schema.IBusinessWeeklyOpen> WeeklyOpen { get; set; }
}
