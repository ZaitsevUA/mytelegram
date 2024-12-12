// ReSharper disable All

using MyTelegram.Schema.Langpack;

namespace MyTelegram.Handlers.Langpack.LayerN;

///<summary>
/// Get strings from a language pack
/// <para>Possible errors</para>
/// Code Type Description
/// 400 LANG_CODE_NOT_SUPPORTED The specified language code is not supported.
/// 400 LANG_PACK_INVALID The provided language pack is invalid.
/// See <a href="https://corefork.telegram.org/method/langpack.getStrings" />
///</summary>
internal sealed class GetStringsHandler(IHandlerHelper handlerHelper) : ForwardRequestToNewHandler<
        MyTelegram.Schema.Langpack.LayerN.RequestGetStrings,
        MyTelegram.Schema.Langpack.RequestGetStrings>(handlerHelper),
    Langpack.LayerN.IGetStringsHandler
{
    protected override RequestGetStrings GetNewData(IRequestInput request, Schema.Langpack.LayerN.RequestGetStrings obj)
    {
        return new RequestGetStrings
        {
            LangCode = obj.LangCode,
            Keys = obj.Keys,
            LangPack = request.DeviceType.ToString().ToLower()
        };
    }
}
