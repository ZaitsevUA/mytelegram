// ReSharper disable All

namespace MyTelegram.Schema.Smsjobs;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/smsjobs.Status" />
///</summary>
[JsonDerivedType(typeof(TStatus), nameof(TStatus))]
public interface IStatus : IObject
{
    BitArray Flags { get; set; }
    bool AllowInternational { get; set; }
    int RecentSent { get; set; }
    int RecentSince { get; set; }
    int RecentRemains { get; set; }
    int TotalSent { get; set; }
    int TotalSince { get; set; }
    string? LastGiftSlug { get; set; }
    string TermsUrl { get; set; }
}
