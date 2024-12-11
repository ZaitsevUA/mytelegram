// ReSharper disable All

using MyTelegram.Schema.Langpack;

namespace MyTelegram.Handlers.Langpack.LayerN;

///<summary>
/// Get information about all languages in a localization pack
/// <para>Possible errors</para>
/// Code Type Description
/// 400 LANG_PACK_INVALID The provided language pack is invalid.
/// See <a href="https://corefork.telegram.org/method/langpack.getLanguages" />
///</summary>
internal sealed class GetLanguagesHandler(IHandlerHelper handlerHelper) : ForwardRequestToNewHandler<
        MyTelegram.Schema.Langpack.LayerN.RequestGetLanguages,
        MyTelegram.Schema.Langpack.RequestGetLanguages,
        TVector<ILangPackLanguage>>(handlerHelper),
    Langpack.LayerN.IGetLanguagesHandler
{
    protected override RequestGetLanguages GetNewData(IRequestInput request, Schema.Langpack.LayerN.RequestGetLanguages obj)
    {
        return new RequestGetLanguages
        {
            LangPack = request.DeviceType.ToString().ToLower()
        };
    }
}
