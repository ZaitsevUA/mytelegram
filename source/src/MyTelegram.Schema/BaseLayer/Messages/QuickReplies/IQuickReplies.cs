// ReSharper disable All

namespace MyTelegram.Schema.Messages;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/messages.QuickReplies" />
///</summary>
[JsonDerivedType(typeof(TQuickReplies), nameof(TQuickReplies))]
[JsonDerivedType(typeof(TQuickRepliesNotModified), nameof(TQuickRepliesNotModified))]
public interface IQuickReplies : IObject
{

}
