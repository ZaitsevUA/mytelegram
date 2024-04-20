// ReSharper disable All

namespace MyTelegram.Handlers.Account;

///<summary>
/// See <a href="https://corefork.telegram.org/method/account.updateBusinessAwayMessage" />
///</summary>
internal sealed class UpdateBusinessAwayMessageHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestUpdateBusinessAwayMessage, IBool>,
    Account.IUpdateBusinessAwayMessageHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestUpdateBusinessAwayMessage obj)
    {
        throw new NotImplementedException();
    }
}
