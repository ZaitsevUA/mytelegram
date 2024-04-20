// ReSharper disable All

namespace MyTelegram.Handlers.Fragment;

///<summary>
/// See <a href="https://corefork.telegram.org/method/fragment.getCollectibleInfo" />
///</summary>
internal sealed class GetCollectibleInfoHandler : RpcResultObjectHandler<MyTelegram.Schema.Fragment.RequestGetCollectibleInfo, MyTelegram.Schema.Fragment.ICollectibleInfo>,
    Fragment.IGetCollectibleInfoHandler
{
    protected override Task<MyTelegram.Schema.Fragment.ICollectibleInfo> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Fragment.RequestGetCollectibleInfo obj)
    {
        throw new NotImplementedException();
    }
}
