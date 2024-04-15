// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/SmsJob" />
///</summary>
[JsonDerivedType(typeof(TSmsJob), nameof(TSmsJob))]
public interface ISmsJob : IObject
{
    string JobId { get; set; }
    string PhoneNumber { get; set; }
    string Text { get; set; }
}
