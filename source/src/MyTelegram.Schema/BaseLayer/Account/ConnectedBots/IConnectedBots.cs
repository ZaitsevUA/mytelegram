// ReSharper disable All

namespace MyTelegram.Schema.Account;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/account.ConnectedBots" />
///</summary>
[JsonDerivedType(typeof(TConnectedBots), nameof(TConnectedBots))]
public interface IConnectedBots : IObject
{
    TVector<MyTelegram.Schema.IConnectedBot> ConnectedBots { get; set; }
    TVector<MyTelegram.Schema.IUser> Users { get; set; }
}
