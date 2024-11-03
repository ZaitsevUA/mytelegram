// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/StarsSubscription" />
///</summary>
[JsonDerivedType(typeof(TStarsSubscription), nameof(TStarsSubscription))]
public interface IStarsSubscription : IObject
{
    BitArray Flags { get; set; }
    bool Canceled { get; set; }
    bool CanRefulfill { get; set; }
    bool MissingBalance { get; set; }
    string Id { get; set; }
    MyTelegram.Schema.IPeer Peer { get; set; }
    int UntilDate { get; set; }
    MyTelegram.Schema.IStarsSubscriptionPricing Pricing { get; set; }
    string? ChatInviteHash { get; set; }
}
