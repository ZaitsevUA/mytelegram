// ReSharper disable All

namespace MyTelegram.Schema.Account;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/account.BusinessChatLinks" />
///</summary>
[JsonDerivedType(typeof(TBusinessChatLinks), nameof(TBusinessChatLinks))]
public interface IBusinessChatLinks : IObject
{
    TVector<MyTelegram.Schema.IBusinessChatLink> Links { get; set; }
    TVector<MyTelegram.Schema.IChat> Chats { get; set; }
    TVector<MyTelegram.Schema.IUser> Users { get; set; }
}
