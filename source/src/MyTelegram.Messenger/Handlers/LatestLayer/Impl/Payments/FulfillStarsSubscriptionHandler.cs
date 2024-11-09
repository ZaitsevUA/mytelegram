// ReSharper disable All

namespace MyTelegram.Handlers.Payments;

///<summary>
/// See <a href="https://corefork.telegram.org/method/payments.fulfillStarsSubscription" />
///</summary>
internal sealed class FulfillStarsSubscriptionHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestFulfillStarsSubscription, IBool>,
    Payments.IFulfillStarsSubscriptionHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Payments.RequestFulfillStarsSubscription obj)
    {
        throw new NotImplementedException();
    }
}
