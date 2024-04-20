// ReSharper disable All

namespace MyTelegram.Handlers.Channels;

///<summary>
/// See <a href="https://corefork.telegram.org/method/channels.reportSponsoredMessage" />
///</summary>
internal sealed class ReportSponsoredMessageHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestReportSponsoredMessage, MyTelegram.Schema.Channels.ISponsoredMessageReportResult>,
    Channels.IReportSponsoredMessageHandler
{
    protected override Task<MyTelegram.Schema.Channels.ISponsoredMessageReportResult> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.RequestReportSponsoredMessage obj)
    {
        throw new NotImplementedException();
    }
}
