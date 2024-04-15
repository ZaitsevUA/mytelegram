// ReSharper disable All

namespace MyTelegram.Schema.Messages;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/messages.MyStickers" />
///</summary>
[JsonDerivedType(typeof(TMyStickers), nameof(TMyStickers))]
public interface IMyStickers : IObject
{
    int Count { get; set; }
    TVector<MyTelegram.Schema.IStickerSetCovered> Sets { get; set; }
}
