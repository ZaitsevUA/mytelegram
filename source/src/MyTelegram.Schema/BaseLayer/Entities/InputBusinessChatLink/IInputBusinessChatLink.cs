// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/InputBusinessChatLink" />
///</summary>
[JsonDerivedType(typeof(TInputBusinessChatLink), nameof(TInputBusinessChatLink))]
public interface IInputBusinessChatLink : IObject
{
    BitArray Flags { get; set; }
    string Message { get; set; }
    TVector<MyTelegram.Schema.IMessageEntity>? Entities { get; set; }
    string? Title { get; set; }
}
