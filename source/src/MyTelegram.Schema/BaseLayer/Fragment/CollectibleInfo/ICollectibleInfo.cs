// ReSharper disable All

namespace MyTelegram.Schema.Fragment;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/fragment.CollectibleInfo" />
///</summary>
[JsonDerivedType(typeof(TCollectibleInfo), nameof(TCollectibleInfo))]
public interface ICollectibleInfo : IObject
{
    int PurchaseDate { get; set; }
    string Currency { get; set; }
    long Amount { get; set; }
    string CryptoCurrency { get; set; }
    long CryptoAmount { get; set; }
    string Url { get; set; }
}
