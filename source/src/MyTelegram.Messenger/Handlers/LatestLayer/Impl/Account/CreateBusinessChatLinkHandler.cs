// ReSharper disable All

namespace MyTelegram.Handlers.Account;

///<summary>
/// See <a href="https://corefork.telegram.org/method/account.createBusinessChatLink" />
///</summary>
internal sealed class CreateBusinessChatLinkHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestCreateBusinessChatLink, MyTelegram.Schema.IBusinessChatLink>,
    Account.ICreateBusinessChatLinkHandler
{
    protected override Task<MyTelegram.Schema.IBusinessChatLink> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestCreateBusinessChatLink obj)
    {
        throw new NotImplementedException();
    }
}
