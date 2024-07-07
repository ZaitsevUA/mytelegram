// ReSharper disable All

namespace MyTelegram.Schema.Messages;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/messages.AvailableEffects" />
///</summary>
[JsonDerivedType(typeof(TAvailableEffectsNotModified), nameof(TAvailableEffectsNotModified))]
[JsonDerivedType(typeof(TAvailableEffects), nameof(TAvailableEffects))]
public interface IAvailableEffects : IObject
{

}
