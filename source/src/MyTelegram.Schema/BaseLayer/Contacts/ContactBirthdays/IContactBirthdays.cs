// ReSharper disable All

namespace MyTelegram.Schema.Contacts;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/contacts.ContactBirthdays" />
///</summary>
[JsonDerivedType(typeof(TContactBirthdays), nameof(TContactBirthdays))]
public interface IContactBirthdays : IObject
{
    TVector<MyTelegram.Schema.IContactBirthday> Contacts { get; set; }
    TVector<MyTelegram.Schema.IUser> Users { get; set; }
}
