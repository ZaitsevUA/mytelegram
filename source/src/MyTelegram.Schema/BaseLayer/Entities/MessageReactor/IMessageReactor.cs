// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/MessageReactor" />
///</summary>
[JsonDerivedType(typeof(TMessageReactor), nameof(TMessageReactor))]
public interface IMessageReactor : IObject
{
    BitArray Flags { get; set; }
    bool Top { get; set; }
    bool My { get; set; }
    bool Anonymous { get; set; }
    MyTelegram.Schema.IPeer? PeerId { get; set; }
    int Count { get; set; }
}
