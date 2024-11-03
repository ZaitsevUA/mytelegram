// ReSharper disable All

namespace MyTelegram.Handlers.Payments;

///<summary>
/// See <a href="https://corefork.telegram.org/method/payments.getStarGifts" />
///</summary>
internal sealed class GetStarGiftsHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestGetStarGifts, MyTelegram.Schema.Payments.IStarGifts>,
    Payments.IGetStarGiftsHandler
{
    protected override Task<MyTelegram.Schema.Payments.IStarGifts> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Payments.RequestGetStarGifts obj)
    {
        throw new NotImplementedException();
    }
}
