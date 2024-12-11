// ReSharper disable All

namespace MyTelegram.Handlers.Account.LayerN;

///<summary>
/// Create a theme
/// <para>Possible errors</para>
/// Code Type Description
/// 400 THEME_MIME_INVALID The theme's MIME type is invalid.
/// 400 THEME_TITLE_INVALID The specified theme title is invalid.
/// See <a href="https://corefork.telegram.org/method/account.createTheme" />
///</summary>
internal sealed class CreateThemeHandler(IHandlerHelper handlerHelper) :
    ForwardRequestToNewHandler<MyTelegram.Schema.Account.LayerN.RequestCreateTheme,
        MyTelegram.Schema.Account.RequestCreateTheme,
        MyTelegram.Schema.ITheme>(handlerHelper),
    LayerN.ICreateThemeHandler
{
    protected override RequestCreateTheme GetNewData(IRequestInput input, Schema.Account.LayerN.RequestCreateTheme obj)
    {
        return new RequestCreateTheme
        {
            Slug = obj.Slug,
            Document = obj.Document,
            Title = obj.Title,
            Settings = obj.Settings == null ? null : [obj.Settings]
        };
    }
}
