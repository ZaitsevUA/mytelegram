// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/GeoPointAddress" />
///</summary>
[JsonDerivedType(typeof(TGeoPointAddress), nameof(TGeoPointAddress))]
public interface IGeoPointAddress : IObject
{
    BitArray Flags { get; set; }
    string CountryIso2 { get; set; }
    string? State { get; set; }
    string? City { get; set; }
    string? Street { get; set; }
}
