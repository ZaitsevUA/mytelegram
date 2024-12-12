﻿namespace MyTelegram.Messenger.Services.Caching;

public class LanguageTextItem(string key, string value, int languageVersion)
{
    public string Key { get; init; } = key;
    public string Value { get; set; } = value;
    public int LanguageVersion { get; } = languageVersion;
}

public interface ILanguageCacheService
{
    Task LoadAllLanguagesAsync();
    Task LoadAllLanguageTextAsync();
    Task<IReadOnlyCollection<ILanguageReadModel>> GetAllLanguagesAsync();

    Task<IReadOnlyCollection<LanguageTextItem>> GetLanguageTextAsync(string languageCode,
        string languagePack);

    Task UpdateLanguageTextAsync(string languageCode, string languagePack, string key, string value);
    Task DeleteLanguageTextAsync(string languageCode, string languagePack, string key);

    Task<List<LanguageTextItem>> GetLanguageTextsAsync(string languageCode, string languagePack, IEnumerable<string> keys);

    Task<ILanguageReadModel?> GetLanguageAsync(string languageCode, string languagePack);

    Task<List<LanguageTextItem>> GetLanguageDifferenceAsync(string languageCode, string languagePack, int fromVersion);
}

public class LanguageCacheService(IQueryProcessor queryProcessor, ILogger<LanguageCacheService> logger) : ILanguageCacheService, ISingletonDependency
{
    private Dictionary<string, ILanguageReadModel> _languageReadModels = [];
    private Dictionary<string, Dictionary<string, LanguageTextItem>> _languageTexts = [];

    public async Task LoadAllLanguagesAsync()
    {
        var languages = await queryProcessor.ProcessAsync(new GetAllLanguagesQuery());
        _languageReadModels = languages.ToDictionary(k => GetLanguageTextKey(k.LanguageCode, k.Platform), v => v);
        logger.LogInformation("Loading all languages completed, count: {Count}", languages.Count);
    }

    public async Task LoadAllLanguageTextAsync()
    {
        var sw = Stopwatch.StartNew();
        var languageTexts = await queryProcessor.ProcessAsync(new GetAllLanguageTextsQuery());
        _languageTexts = languageTexts.GroupBy(p => new { p.LanguageCode, p.Platform },
                v => new LanguageTextItem(v.Key, v.Value, v.LanguageVersion))
            .ToDictionary(k => GetLanguageTextKey(k.Key.LanguageCode, k.Key.Platform),
                v => v.ToDictionary(k1 => k1.Key, v1 => v1));
        sw.Stop();
        logger.LogInformation("Loading all language texts completed, count: {Count}, timespan: {TimeSpan}", languageTexts.Count, sw.Elapsed);
    }

    private string GetLanguageTextKey(string languageCode, string languagePack) => $"{languageCode}_{languagePack}";

    private string GetLanguageTextKey(string languageCode, DeviceType deviceType)
    {
        var langPack = deviceType.ToString().ToLower();
        switch (deviceType)
        {
            case DeviceType.Desktop:
                langPack = "tdesktop";
                break;
            case DeviceType.AndroidX:
                langPack = "android_x";
                break;
        }

        return GetLanguageTextKey(languageCode, langPack);
    }

    public async Task<IReadOnlyCollection<ILanguageReadModel>> GetAllLanguagesAsync()
    {
        if (!_languageReadModels.Any())
        {
            await LoadAllLanguagesAsync();
        }

        return _languageReadModels.Values;
    }

    public async Task<IReadOnlyCollection<LanguageTextItem>> GetLanguageTextAsync(string languageCode,
         string languagePack)
    {
        if (!_languageTexts.Any())
        {
            await LoadAllLanguageTextAsync();
        }
        var key = GetLanguageTextKey(languageCode, languagePack);

        if (_languageTexts.TryGetValue(key, out var languageTextItems))
        {
            return languageTextItems.Values;
        }

        return [];
    }

    public Task UpdateLanguageTextAsync(string languageCode, string languagePack, string key, string value)
    {
        var languageKey = GetLanguageTextKey(languageCode, languagePack);
        if (_languageTexts.TryGetValue(languageKey, out var texts))
        {
            if (texts.TryGetValue(key, out var item))
            {
                item.Value = value;
                logger.LogInformation("Language text updated, languageCode: {Code}, platform: {Platform}, key: {Key}", languageCode, languagePack, key);
            }
        }

        return Task.CompletedTask;
    }

    public Task DeleteLanguageTextAsync(string languageCode, string languagePack, string key)
    {
        var languageKey = GetLanguageTextKey(languageCode, languagePack);
        if (_languageTexts.TryGetValue(languageKey, out var texts))
        {
            texts.Remove(key);
            logger.LogInformation("Language text deleted, languageCode: {Code}, platform: {Platform}, key: {Key}", languageCode, languagePack, key);
        }

        return Task.CompletedTask;
    }

    public Task<List<LanguageTextItem>> GetLanguageTextsAsync(string languageCode, string languagePack, IEnumerable<string> keys)
    {
        var languageTexts = new List<LanguageTextItem>();

        var languageKey = GetLanguageTextKey(languageCode, languagePack);
        if (_languageTexts.TryGetValue(languageKey, out var texts))
        {
            foreach (var key in keys)
            {
                if (texts.TryGetValue(key, out var item))
                {
                    languageTexts.Add(item);
                }
            }
        }

        return Task.FromResult(languageTexts);
    }

    public Task<ILanguageReadModel?> GetLanguageAsync(string languageCode, string languagePack)
    {
        _languageReadModels.TryGetValue(GetLanguageTextKey(languageCode, languagePack), out var languageReadModel);

        return Task.FromResult(languageReadModel);
    }

    public async Task<List<LanguageTextItem>> GetLanguageDifferenceAsync(string languageCode, string languagePack, int fromVersion)
    {
        var languageKey = GetLanguageTextKey(languageCode, languagePack);
        if (_languageTexts.TryGetValue(languageKey, out var texts))
        {
            return texts.Values.Where(p => p.LanguageVersion > fromVersion).ToList();
        }

        return [];
    }
}
