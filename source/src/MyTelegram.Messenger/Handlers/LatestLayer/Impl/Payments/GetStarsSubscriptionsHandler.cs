// ReSharper disable All

namespace MyTelegram.Handlers.Payments;

///<summary>
/// See <a href="https://corefork.telegram.org/method/payments.getStarsSubscriptions" />
///</summary>
internal sealed class GetStarsSubscriptionsHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestGetStarsSubscriptions, MyTelegram.Schema.Payments.IStarsStatus>,
    Payments.IGetStarsSubscriptionsHandler
{
    protected override Task<MyTelegram.Schema.Payments.IStarsStatus> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Payments.RequestGetStarsSubscriptions obj)
    {
        throw new NotImplementedException();
    }
}
