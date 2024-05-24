// ReSharper disable All

namespace MyTelegram.Handlers.Auth;

///<summary>
/// See <a href="https://corefork.telegram.org/method/auth.reportMissingCode" />
///</summary>
internal sealed class ReportMissingCodeHandler : RpcResultObjectHandler<MyTelegram.Schema.Auth.RequestReportMissingCode, IBool>,
    Auth.IReportMissingCodeHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Auth.RequestReportMissingCode obj)
    {
        throw new NotImplementedException();
    }
}
