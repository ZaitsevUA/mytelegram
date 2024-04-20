// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/BusinessLocation" />
///</summary>
[JsonDerivedType(typeof(TBusinessLocation), nameof(TBusinessLocation))]
public interface IBusinessLocation : IObject
{
    BitArray Flags { get; set; }
    MyTelegram.Schema.IGeoPoint? GeoPoint { get; set; }
    string Address { get; set; }
}
