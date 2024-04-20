// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/BusinessAwayMessage" />
///</summary>
[JsonDerivedType(typeof(TBusinessAwayMessage), nameof(TBusinessAwayMessage))]
public interface IBusinessAwayMessage : IObject
{
    BitArray Flags { get; set; }
    bool OfflineOnly { get; set; }
    int ShortcutId { get; set; }
    MyTelegram.Schema.IBusinessAwayMessageSchedule Schedule { get; set; }
    MyTelegram.Schema.IBusinessRecipients Recipients { get; set; }
}
