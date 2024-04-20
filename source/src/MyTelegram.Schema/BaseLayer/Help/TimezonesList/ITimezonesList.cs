// ReSharper disable All

namespace MyTelegram.Schema.Help;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/help.TimezonesList" />
///</summary>
[JsonDerivedType(typeof(TTimezonesListNotModified), nameof(TTimezonesListNotModified))]
[JsonDerivedType(typeof(TTimezonesList), nameof(TTimezonesList))]
public interface ITimezonesList : IObject
{

}
