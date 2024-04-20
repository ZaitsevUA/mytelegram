// ReSharper disable All

namespace MyTelegram.Handlers.Account;

///<summary>
/// See <a href="https://corefork.telegram.org/method/account.disablePeerConnectedBot" />
///</summary>
internal sealed class DisablePeerConnectedBotHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestDisablePeerConnectedBot, IBool>,
    Account.IDisablePeerConnectedBotHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestDisablePeerConnectedBot obj)
    {
        throw new NotImplementedException();
    }
}
