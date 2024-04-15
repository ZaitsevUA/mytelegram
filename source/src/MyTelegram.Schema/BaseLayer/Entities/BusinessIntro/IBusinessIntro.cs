// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/BusinessIntro" />
///</summary>
[JsonDerivedType(typeof(TBusinessIntro), nameof(TBusinessIntro))]
public interface IBusinessIntro : IObject
{
    BitArray Flags { get; set; }
    string Title { get; set; }
    string Description { get; set; }
    MyTelegram.Schema.IDocument? Sticker { get; set; }
}
