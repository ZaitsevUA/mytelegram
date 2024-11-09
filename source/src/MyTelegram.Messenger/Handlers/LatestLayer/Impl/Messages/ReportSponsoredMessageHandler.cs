// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// See <a href="https://corefork.telegram.org/method/messages.reportSponsoredMessage" />
///</summary>
internal sealed class ReportSponsoredMessageHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestReportSponsoredMessage, MyTelegram.Schema.Channels.ISponsoredMessageReportResult>,
    Messages.IReportSponsoredMessageHandler
{
    protected override Task<MyTelegram.Schema.Channels.ISponsoredMessageReportResult> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestReportSponsoredMessage obj)
    {
        throw new NotImplementedException();
    }
}
