// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/MessageReportOption" />
///</summary>
[JsonDerivedType(typeof(TMessageReportOption), nameof(TMessageReportOption))]
public interface IMessageReportOption : IObject
{
    string Text { get; set; }
    byte[] Option { get; set; }
}
