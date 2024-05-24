// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Webpage attributes
/// See <a href="https://corefork.telegram.org/constructor/WebPageAttribute" />
///</summary>
[JsonDerivedType(typeof(TWebPageAttributeTheme), nameof(TWebPageAttributeTheme))]
[JsonDerivedType(typeof(TWebPageAttributeStory), nameof(TWebPageAttributeStory))]
[JsonDerivedType(typeof(TWebPageAttributeStickerSet), nameof(TWebPageAttributeStickerSet))]
public interface IWebPageAttribute : IObject
{
    BitArray Flags { get; set; }
}
