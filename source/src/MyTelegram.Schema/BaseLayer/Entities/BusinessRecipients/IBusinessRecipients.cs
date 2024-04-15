// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/BusinessRecipients" />
///</summary>
[JsonDerivedType(typeof(TBusinessRecipients), nameof(TBusinessRecipients))]
public interface IBusinessRecipients : IObject
{
    BitArray Flags { get; set; }
    bool ExistingChats { get; set; }
    bool NewChats { get; set; }
    bool Contacts { get; set; }
    bool NonContacts { get; set; }
    bool ExcludeSelected { get; set; }
    TVector<long>? Users { get; set; }
}
