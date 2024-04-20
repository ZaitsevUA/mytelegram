// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/BotBusinessConnection" />
///</summary>
[JsonDerivedType(typeof(TBotBusinessConnection), nameof(TBotBusinessConnection))]
public interface IBotBusinessConnection : IObject
{
    BitArray Flags { get; set; }
    bool CanReply { get; set; }
    bool Disabled { get; set; }
    string ConnectionId { get; set; }
    long UserId { get; set; }
    int DcId { get; set; }
    int Date { get; set; }
}
