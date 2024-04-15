// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/RequestedPeer" />
///</summary>
[JsonDerivedType(typeof(TRequestedPeerUser), nameof(TRequestedPeerUser))]
[JsonDerivedType(typeof(TRequestedPeerChat), nameof(TRequestedPeerChat))]
[JsonDerivedType(typeof(TRequestedPeerChannel), nameof(TRequestedPeerChannel))]
public interface IRequestedPeer : IObject
{
    BitArray Flags { get; set; }
    MyTelegram.Schema.IPhoto? Photo { get; set; }
}
