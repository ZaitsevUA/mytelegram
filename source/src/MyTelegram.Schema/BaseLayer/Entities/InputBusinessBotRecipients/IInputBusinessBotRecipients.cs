// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/InputBusinessBotRecipients" />
///</summary>
[JsonDerivedType(typeof(TInputBusinessBotRecipients), nameof(TInputBusinessBotRecipients))]
public interface IInputBusinessBotRecipients : IObject
{
    BitArray Flags { get; set; }
    bool ExistingChats { get; set; }
    bool NewChats { get; set; }
    bool Contacts { get; set; }
    bool NonContacts { get; set; }
    bool ExcludeSelected { get; set; }
    TVector<MyTelegram.Schema.IInputUser>? Users { get; set; }
    TVector<MyTelegram.Schema.IInputUser>? ExcludeUsers { get; set; }
}
