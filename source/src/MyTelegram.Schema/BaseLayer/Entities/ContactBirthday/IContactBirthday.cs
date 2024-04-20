// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/ContactBirthday" />
///</summary>
[JsonDerivedType(typeof(TContactBirthday), nameof(TContactBirthday))]
public interface IContactBirthday : IObject
{
    long ContactId { get; set; }
    MyTelegram.Schema.IBirthday Birthday { get; set; }
}
