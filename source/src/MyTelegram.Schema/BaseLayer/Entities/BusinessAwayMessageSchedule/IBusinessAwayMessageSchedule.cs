// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/BusinessAwayMessageSchedule" />
///</summary>
[JsonDerivedType(typeof(TBusinessAwayMessageScheduleAlways), nameof(TBusinessAwayMessageScheduleAlways))]
[JsonDerivedType(typeof(TBusinessAwayMessageScheduleOutsideWorkHours), nameof(TBusinessAwayMessageScheduleOutsideWorkHours))]
[JsonDerivedType(typeof(TBusinessAwayMessageScheduleCustom), nameof(TBusinessAwayMessageScheduleCustom))]
public interface IBusinessAwayMessageSchedule : IObject
{

}
