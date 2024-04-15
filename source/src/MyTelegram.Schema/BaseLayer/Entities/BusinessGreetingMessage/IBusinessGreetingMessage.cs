// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/BusinessGreetingMessage" />
///</summary>
[JsonDerivedType(typeof(TBusinessGreetingMessage), nameof(TBusinessGreetingMessage))]
public interface IBusinessGreetingMessage : IObject
{
    int ShortcutId { get; set; }
    MyTelegram.Schema.IBusinessRecipients Recipients { get; set; }
    int NoActivityDays { get; set; }
}
