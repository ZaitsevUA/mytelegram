// ReSharper disable All

namespace MyTelegram.Handlers.Smsjobs;

///<summary>
/// See <a href="https://corefork.telegram.org/method/smsjobs.finishJob" />
///</summary>
internal sealed class FinishJobHandler : RpcResultObjectHandler<MyTelegram.Schema.Smsjobs.RequestFinishJob, IBool>,
    Smsjobs.IFinishJobHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Smsjobs.RequestFinishJob obj)
    {
        throw new NotImplementedException();
    }
}
