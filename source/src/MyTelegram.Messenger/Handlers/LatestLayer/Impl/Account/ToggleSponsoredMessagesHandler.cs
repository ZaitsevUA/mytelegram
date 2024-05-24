// ReSharper disable All

namespace MyTelegram.Handlers.Account;

///<summary>
/// See <a href="https://corefork.telegram.org/method/account.toggleSponsoredMessages" />
///</summary>
internal sealed class ToggleSponsoredMessagesHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestToggleSponsoredMessages, IBool>,
    Account.IToggleSponsoredMessagesHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestToggleSponsoredMessages obj)
    {
        throw new NotImplementedException();
    }
}
