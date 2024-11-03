// ReSharper disable All

namespace MyTelegram.Schema.Payments;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/payments.UserStarGifts" />
///</summary>
[JsonDerivedType(typeof(TUserStarGifts), nameof(TUserStarGifts))]
public interface IUserStarGifts : IObject
{
    BitArray Flags { get; set; }
    int Count { get; set; }
    TVector<MyTelegram.Schema.IUserStarGift> Gifts { get; set; }
    string? NextOffset { get; set; }
    TVector<MyTelegram.Schema.IUser> Users { get; set; }
}
