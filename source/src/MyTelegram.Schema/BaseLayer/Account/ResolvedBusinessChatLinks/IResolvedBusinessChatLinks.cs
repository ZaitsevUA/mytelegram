// ReSharper disable All

namespace MyTelegram.Schema.Account;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/account.ResolvedBusinessChatLinks" />
///</summary>
[JsonDerivedType(typeof(TResolvedBusinessChatLinks), nameof(TResolvedBusinessChatLinks))]
public interface IResolvedBusinessChatLinks : IObject
{
    BitArray Flags { get; set; }
    MyTelegram.Schema.IPeer Peer { get; set; }
    string Message { get; set; }
    TVector<MyTelegram.Schema.IMessageEntity>? Entities { get; set; }
    TVector<MyTelegram.Schema.IChat> Chats { get; set; }
    TVector<MyTelegram.Schema.IUser> Users { get; set; }
}
