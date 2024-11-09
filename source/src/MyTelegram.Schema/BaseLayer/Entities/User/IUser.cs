// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Object defines a user.
/// See <a href="https://corefork.telegram.org/constructor/User" />
///</summary>
[JsonDerivedType(typeof(TUserEmpty), nameof(TUserEmpty))]
[JsonDerivedType(typeof(TUser), nameof(TUser))]
public interface IUser : IObject
{
    ///<summary>
    /// ID of the user, see <a href="https://corefork.telegram.org/api/peers#peer-id">here »</a> for more info.
    ///</summary>
    long Id { get; set; }
}
