// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/InputStarsTransaction" />
///</summary>
[JsonDerivedType(typeof(TInputStarsTransaction), nameof(TInputStarsTransaction))]
public interface IInputStarsTransaction : IObject
{
    BitArray Flags { get; set; }
    bool Refund { get; set; }
    string Id { get; set; }
}
