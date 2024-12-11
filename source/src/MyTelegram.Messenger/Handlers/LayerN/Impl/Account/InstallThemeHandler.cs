// ReSharper disable All
namespace MyTelegram.Handlers.Account.LayerN;

///<summary>
/// Install a theme
/// See <a href="https://corefork.telegram.org/method/account.installTheme" />
///</summary>
internal sealed class InstallThemeHandler(IHandlerHelper handlerHelper) :
    ForwardRequestToNewHandler<MyTelegram.Schema.Account.LayerN.RequestInstallTheme,
        MyTelegram.Schema.Account.RequestInstallTheme,
        IBool
    >(handlerHelper),
    LayerN.IInstallThemeHandler
{
    protected override RequestInstallTheme GetNewData(IRequestInput input, Schema.Account.LayerN.RequestInstallTheme obj)
    {
        return new RequestInstallTheme
        {
            Dark = obj.Dark,
            Format = obj.Format,
            Theme = obj.Theme,
        };
    }
}