// ReSharper disable All

namespace MyTelegram.Schema.Messages;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/messages.InvitedUsers" />
///</summary>
[JsonDerivedType(typeof(TInvitedUsers), nameof(TInvitedUsers))]
public interface IInvitedUsers : IObject
{
    MyTelegram.Schema.IUpdates Updates { get; set; }
    TVector<MyTelegram.Schema.IMissingInvitee> MissingInvitees { get; set; }
}
