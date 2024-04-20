// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/InputCollectible" />
///</summary>
[JsonDerivedType(typeof(TInputCollectibleUsername), nameof(TInputCollectibleUsername))]
[JsonDerivedType(typeof(TInputCollectiblePhone), nameof(TInputCollectiblePhone))]
public interface IInputCollectible : IObject
{

}
