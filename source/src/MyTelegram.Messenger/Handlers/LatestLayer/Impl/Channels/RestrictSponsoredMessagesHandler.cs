// ReSharper disable All

namespace MyTelegram.Handlers.Channels;

///<summary>
/// See <a href="https://corefork.telegram.org/method/channels.restrictSponsoredMessages" />
///</summary>
internal sealed class RestrictSponsoredMessagesHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestRestrictSponsoredMessages, MyTelegram.Schema.IUpdates>,
    Channels.IRestrictSponsoredMessagesHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.RequestRestrictSponsoredMessages obj)
    {
        throw new NotImplementedException();
    }
}
