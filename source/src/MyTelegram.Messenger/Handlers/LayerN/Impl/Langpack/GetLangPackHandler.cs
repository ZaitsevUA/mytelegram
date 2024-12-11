// ReSharper disable All

using MyTelegram.Schema.Langpack;

namespace MyTelegram.Handlers.Langpack.LayerN;

///<summary>
/// Get localization pack strings
/// <para>Possible errors</para>
/// Code Type Description
/// 400 LANG_CODE_NOT_SUPPORTED The specified language code is not supported.
/// 400 LANG_PACK_INVALID The provided language pack is invalid.
/// See <a href="https://corefork.telegram.org/method/langpack.getLangPack" />
///</summary>
internal sealed class GetLangPackHandler(IHandlerHelper handlerHelper) : ForwardRequestToNewHandler<
        MyTelegram.Schema.Langpack.LayerN.RequestGetLangPack,
        MyTelegram.Schema.Langpack.RequestGetLangPack,
        ILangPackDifference>(handlerHelper),
    Langpack.LayerN.IGetLangPackHandler
{
    protected override RequestGetLangPack GetNewData(IRequestInput input, Schema.Langpack.LayerN.RequestGetLangPack obj)
    {
        return new RequestGetLangPack
        {
            LangCode = obj.LangCode,
            LangPack = input.DeviceType.ToString().ToLower()
        };
    }
}
