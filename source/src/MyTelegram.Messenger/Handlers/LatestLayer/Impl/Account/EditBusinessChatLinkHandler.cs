// ReSharper disable All

namespace MyTelegram.Handlers.Account;

///<summary>
/// See <a href="https://corefork.telegram.org/method/account.editBusinessChatLink" />
///</summary>
internal sealed class EditBusinessChatLinkHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestEditBusinessChatLink, MyTelegram.Schema.IBusinessChatLink>,
    Account.IEditBusinessChatLinkHandler
{
    protected override Task<MyTelegram.Schema.IBusinessChatLink> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestEditBusinessChatLink obj)
    {
        throw new NotImplementedException();
    }
}
