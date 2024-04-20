// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/SponsoredMessageReportOption" />
///</summary>
[JsonDerivedType(typeof(TSponsoredMessageReportOption), nameof(TSponsoredMessageReportOption))]
public interface ISponsoredMessageReportOption : IObject
{
    string Text { get; set; }
    byte[] Option { get; set; }
}
