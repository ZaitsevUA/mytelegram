// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/InputBusinessIntro" />
///</summary>
[JsonDerivedType(typeof(TInputBusinessIntro), nameof(TInputBusinessIntro))]
public interface IInputBusinessIntro : IObject
{
    BitArray Flags { get; set; }
    string Title { get; set; }
    string Description { get; set; }
    MyTelegram.Schema.IInputDocument? Sticker { get; set; }
}
