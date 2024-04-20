// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/BusinessChatLink" />
///</summary>
[JsonDerivedType(typeof(TBusinessChatLink), nameof(TBusinessChatLink))]
public interface IBusinessChatLink : IObject
{
    BitArray Flags { get; set; }
    string Link { get; set; }
    string Message { get; set; }
    TVector<MyTelegram.Schema.IMessageEntity>? Entities { get; set; }
    string? Title { get; set; }
    int Views { get; set; }
}
