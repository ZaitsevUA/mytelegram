// ReSharper disable All

using System.Text.Json;

namespace MyTelegram.Handlers.Help;

///<summary>
/// Get name, ISO code, localized name and phone codes/patterns of all available countries
/// See <a href="https://corefork.telegram.org/method/help.getCountriesList" />
///</summary>
internal sealed class GetCountriesListHandler(ICountryHelper countryHelper) : RpcResultObjectHandler<MyTelegram.Schema.Help.RequestGetCountriesList, MyTelegram.Schema.Help.ICountriesList>,
    Help.IGetCountriesListHandler
{

    private static ICountriesList? _countriesList;

    protected override Task<ICountriesList> HandleCoreAsync(IRequestInput input,
        RequestGetCountriesList obj)
    {
        InitCountriesListIfNeed();

        return Task.FromResult<ICountriesList>(_countriesList!);
    }

    private void InitCountriesListIfNeed()
    {
        if (_countriesList == null)
        {
            var countriesList =
                countryHelper.GetAllCountryList();

            _countriesList = new TCountriesList
            {
                Countries = new TVector<ICountry>(countriesList!.Select(p => new TCountry
                {
                    Hidden = p.Hidden,
                    Iso2 = p.Iso2,
                    DefaultName = p.DefaultName,
                    Name = p.Name,
                    CountryCodes = new TVector<ICountryCode>(p.CountryCodes.Select(p => new TCountryCode
                    {
                        CountryCode = p.CountryCode,
                        Patterns = p.Patterns == null ? null : new TVector<string>(p.Patterns),
                        Prefixes = p.Prefixes == null ? null : new TVector<string>(p.Prefixes)
                    }))
                })),
                Hash = -432121763
            };
        }
    }
}
