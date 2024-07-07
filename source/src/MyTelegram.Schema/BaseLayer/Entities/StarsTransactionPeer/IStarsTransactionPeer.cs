// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/StarsTransactionPeer" />
///</summary>
[JsonDerivedType(typeof(TStarsTransactionPeerUnsupported), nameof(TStarsTransactionPeerUnsupported))]
[JsonDerivedType(typeof(TStarsTransactionPeerAppStore), nameof(TStarsTransactionPeerAppStore))]
[JsonDerivedType(typeof(TStarsTransactionPeerPlayMarket), nameof(TStarsTransactionPeerPlayMarket))]
[JsonDerivedType(typeof(TStarsTransactionPeerPremiumBot), nameof(TStarsTransactionPeerPremiumBot))]
[JsonDerivedType(typeof(TStarsTransactionPeerFragment), nameof(TStarsTransactionPeerFragment))]
[JsonDerivedType(typeof(TStarsTransactionPeer), nameof(TStarsTransactionPeer))]
[JsonDerivedType(typeof(TStarsTransactionPeerAds), nameof(TStarsTransactionPeerAds))]
public interface IStarsTransactionPeer : IObject
{

}
