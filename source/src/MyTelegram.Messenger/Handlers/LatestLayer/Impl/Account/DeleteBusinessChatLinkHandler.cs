// ReSharper disable All

namespace MyTelegram.Handlers.Account;

///<summary>
/// See <a href="https://corefork.telegram.org/method/account.deleteBusinessChatLink" />
///</summary>
internal sealed class DeleteBusinessChatLinkHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestDeleteBusinessChatLink, IBool>,
    Account.IDeleteBusinessChatLinkHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestDeleteBusinessChatLink obj)
    {
        throw new NotImplementedException();
    }
}
