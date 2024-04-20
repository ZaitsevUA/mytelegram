// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/InputBusinessGreetingMessage" />
///</summary>
[JsonDerivedType(typeof(TInputBusinessGreetingMessage), nameof(TInputBusinessGreetingMessage))]
public interface IInputBusinessGreetingMessage : IObject
{
    int ShortcutId { get; set; }
    MyTelegram.Schema.IInputBusinessRecipients Recipients { get; set; }
    int NoActivityDays { get; set; }
}
