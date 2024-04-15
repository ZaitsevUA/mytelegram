// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/InputBusinessRecipients" />
///</summary>
[JsonDerivedType(typeof(TInputBusinessRecipients), nameof(TInputBusinessRecipients))]
public interface IInputBusinessRecipients : IObject
{
    BitArray Flags { get; set; }
    bool ExistingChats { get; set; }
    bool NewChats { get; set; }
    bool Contacts { get; set; }
    bool NonContacts { get; set; }
    bool ExcludeSelected { get; set; }
    TVector<MyTelegram.Schema.IInputUser>? Users { get; set; }
}
