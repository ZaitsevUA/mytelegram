// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/InputQuickReplyShortcut" />
///</summary>
[JsonDerivedType(typeof(TInputQuickReplyShortcut), nameof(TInputQuickReplyShortcut))]
[JsonDerivedType(typeof(TInputQuickReplyShortcutId), nameof(TInputQuickReplyShortcutId))]
public interface IInputQuickReplyShortcut : IObject
{

}
