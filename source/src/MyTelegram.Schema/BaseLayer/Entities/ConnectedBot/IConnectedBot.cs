// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/ConnectedBot" />
///</summary>
[JsonDerivedType(typeof(TConnectedBot), nameof(TConnectedBot))]
public interface IConnectedBot : IObject
{
    BitArray Flags { get; set; }
    bool CanReply { get; set; }
    long BotId { get; set; }
    MyTelegram.Schema.IBusinessBotRecipients Recipients { get; set; }
}
