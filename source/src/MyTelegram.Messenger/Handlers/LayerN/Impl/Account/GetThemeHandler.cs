// ReSharper disable All

namespace MyTelegram.Handlers.Account.LayerN;

/// <summary>
///     Create a theme
///     <para>Possible errors</para>
///     Code Type Description
///     400 THEME_MIME_INVALID The theme's MIME type is invalid.
///     400 THEME_TITLE_INVALID The specified theme title is invalid.
///     See <a href="https://corefork.telegram.org/method/account.createTheme" />
/// </summary>
internal sealed class GetThemeHandler(IHandlerHelper handlerHelper) :
    ForwardRequestToNewHandler<
        MyTelegram.Schema.Account.LayerN.RequestGetTheme,
        MyTelegram.Schema.Account.RequestGetTheme>(handlerHelper),
    LayerN.IGetThemeHandler
{
    protected override Schema.Account.RequestGetTheme GetNewData(IRequestInput input, MyTelegram.Schema.Account.LayerN.RequestGetTheme obj)
    {
        return new Schema.Account.RequestGetTheme
        {
            Format = obj.Format,
            Theme = obj.Theme
        };
    }
}