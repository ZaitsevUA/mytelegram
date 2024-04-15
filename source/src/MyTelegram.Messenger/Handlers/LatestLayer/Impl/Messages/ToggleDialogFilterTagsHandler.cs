// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// See <a href="https://corefork.telegram.org/method/messages.toggleDialogFilterTags" />
///</summary>
internal sealed class ToggleDialogFilterTagsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestToggleDialogFilterTags, IBool>,
    Messages.IToggleDialogFilterTagsHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestToggleDialogFilterTags obj)
    {
        throw new NotImplementedException();
    }
}
