namespace MyTelegram.Messenger.Services.Interfaces;

public interface ICountryHelper
{
    bool TryGetCountryCodeItem(string countryCode, [NotNullWhen(true)] out CountryCodeItem? countryCodeItem);
    List<CountryItem> GetAllCountryList();
    void InitAllCountries();
}


public record CountryItem(bool Hidden, string Iso2, string DefaultName, string? Name, List<CountryCodeItem> CountryCodes);
public record CountryCodeItem(string CountryCode, List<string>? Prefixes, List<string>? Patterns, List<int>? PhoneNumberLengths);