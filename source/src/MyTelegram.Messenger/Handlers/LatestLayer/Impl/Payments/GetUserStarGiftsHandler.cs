// ReSharper disable All

namespace MyTelegram.Handlers.Payments;

///<summary>
/// See <a href="https://corefork.telegram.org/method/payments.getUserStarGifts" />
///</summary>
internal sealed class GetUserStarGiftsHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestGetUserStarGifts, MyTelegram.Schema.Payments.IUserStarGifts>,
    Payments.IGetUserStarGiftsHandler
{
    protected override Task<MyTelegram.Schema.Payments.IUserStarGifts> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Payments.RequestGetUserStarGifts obj)
    {
        throw new NotImplementedException();
    }
}
