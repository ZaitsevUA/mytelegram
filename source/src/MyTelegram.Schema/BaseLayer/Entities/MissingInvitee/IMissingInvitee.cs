// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/MissingInvitee" />
///</summary>
[JsonDerivedType(typeof(TMissingInvitee), nameof(TMissingInvitee))]
public interface IMissingInvitee : IObject
{
    BitArray Flags { get; set; }
    bool PremiumWouldAllowInvite { get; set; }
    bool PremiumRequiredForPm { get; set; }
    long UserId { get; set; }
}
