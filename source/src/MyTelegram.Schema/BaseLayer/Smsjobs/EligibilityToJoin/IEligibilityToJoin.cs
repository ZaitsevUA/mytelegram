// ReSharper disable All

namespace MyTelegram.Schema.Smsjobs;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/smsjobs.EligibilityToJoin" />
///</summary>
[JsonDerivedType(typeof(TEligibleToJoin), nameof(TEligibleToJoin))]
public interface IEligibilityToJoin : IObject
{
    string TermsUrl { get; set; }
    int MonthlySentSms { get; set; }
}
