// ReSharper disable All

namespace MyTelegram.Handlers.Account;

///<summary>
/// See <a href="https://corefork.telegram.org/method/account.getBotBusinessConnection" />
///</summary>
internal sealed class GetBotBusinessConnectionHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestGetBotBusinessConnection, MyTelegram.Schema.IUpdates>,
    Account.IGetBotBusinessConnectionHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestGetBotBusinessConnection obj)
    {
        throw new NotImplementedException();
    }
}
