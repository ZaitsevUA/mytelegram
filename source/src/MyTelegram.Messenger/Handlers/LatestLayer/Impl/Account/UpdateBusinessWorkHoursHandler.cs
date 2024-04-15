// ReSharper disable All

namespace MyTelegram.Handlers.Account;

///<summary>
/// See <a href="https://corefork.telegram.org/method/account.updateBusinessWorkHours" />
///</summary>
internal sealed class UpdateBusinessWorkHoursHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestUpdateBusinessWorkHours, IBool>,
    Account.IUpdateBusinessWorkHoursHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestUpdateBusinessWorkHours obj)
    {
        throw new NotImplementedException();
    }
}
