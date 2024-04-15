// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/InputBusinessAwayMessage" />
///</summary>
[JsonDerivedType(typeof(TInputBusinessAwayMessage), nameof(TInputBusinessAwayMessage))]
public interface IInputBusinessAwayMessage : IObject
{
    BitArray Flags { get; set; }
    bool OfflineOnly { get; set; }
    int ShortcutId { get; set; }
    MyTelegram.Schema.IBusinessAwayMessageSchedule Schedule { get; set; }
    MyTelegram.Schema.IInputBusinessRecipients Recipients { get; set; }
}
