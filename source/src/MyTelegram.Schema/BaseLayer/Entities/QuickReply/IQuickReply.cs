// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/QuickReply" />
///</summary>
[JsonDerivedType(typeof(TQuickReply), nameof(TQuickReply))]
public interface IQuickReply : IObject
{
    int ShortcutId { get; set; }
    string Shortcut { get; set; }
    int TopMessage { get; set; }
    int Count { get; set; }
}
