// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Represents an <a href="https://corefork.telegram.org/api/custom-emoji#emoji-categories">emoji category</a>.
/// See <a href="https://corefork.telegram.org/constructor/EmojiGroup" />
///</summary>
[JsonDerivedType(typeof(TEmojiGroup), nameof(TEmojiGroup))]
[JsonDerivedType(typeof(TEmojiGroupGreeting), nameof(TEmojiGroupGreeting))]
[JsonDerivedType(typeof(TEmojiGroupPremium), nameof(TEmojiGroupPremium))]
public interface IEmojiGroup : IObject
{
    string Title { get; set; }
    long IconEmojiId { get; set; }
}
