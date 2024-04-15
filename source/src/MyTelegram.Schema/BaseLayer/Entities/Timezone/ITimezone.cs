// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/Timezone" />
///</summary>
[JsonDerivedType(typeof(TTimezone), nameof(TTimezone))]
public interface ITimezone : IObject
{
    string Id { get; set; }
    string Name { get; set; }
    int UtcOffset { get; set; }
}
