// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/BusinessBotRecipients" />
///</summary>
[JsonDerivedType(typeof(TBusinessBotRecipients), nameof(TBusinessBotRecipients))]
public interface IBusinessBotRecipients : IObject
{
    BitArray Flags { get; set; }
    bool ExistingChats { get; set; }
    bool NewChats { get; set; }
    bool Contacts { get; set; }
    bool NonContacts { get; set; }
    bool ExcludeSelected { get; set; }
    TVector<long>? Users { get; set; }
    TVector<long>? ExcludeUsers { get; set; }
}
