// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/UserStarGift" />
///</summary>
[JsonDerivedType(typeof(TUserStarGift), nameof(TUserStarGift))]
public interface IUserStarGift : IObject
{
    BitArray Flags { get; set; }
    bool NameHidden { get; set; }
    bool Unsaved { get; set; }
    long? FromId { get; set; }
    int Date { get; set; }
    MyTelegram.Schema.IStarGift Gift { get; set; }
    MyTelegram.Schema.ITextWithEntities? Message { get; set; }
    int? MsgId { get; set; }
    long? ConvertStars { get; set; }
}
