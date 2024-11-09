namespace MyTelegram.Messenger.DataConverters;

public class LayeredDataConverterFactory : ILayeredDataConverterFactory
{
    private readonly Dictionary<string, ILayeredDataConverter> _converters;

    private readonly Dictionary<int, Dictionary<string, ILayeredDataConverter>> _layeredConverters;
    public LayeredDataConverterFactory(IEnumerable<ILayeredDataConverter> converters, ILogger<LayeredDataConverterFactory> logger)
    {
        //_converters = converters.ToDictionary(k => k.Name, v => v);
        _layeredConverters = converters.GroupBy(p => p.Layer)
            .ToDictionary(k => k.Key, v => v.ToDictionary(k1 => k1.Name, v1 => v1));
        _converters = _layeredConverters.TryGetValue(Layers.LayerLatest, out var tempConverters) ? tempConverters : [];
        logger.LogInformation("Create {Count} data converters", _converters.Count);
    }

    public ILayeredDataConverter<TInput, TOutput> CreateConverter<TInput, TOutput>(string converterName, int layer = 0)
    {
        if (layer != 0)
        {
            if (_layeredConverters.TryGetValue(layer, out var converters))
            {
                if (converters.TryGetValue(converterName, out var converter))
                {
                    return (ILayeredDataConverter<TInput, TOutput>)converter;
                }
            }
        }
        else
        {
            if (_converters.TryGetValue(converterName, out var converter))
            {
                return (ILayeredDataConverter<TInput, TOutput>)converter;
            }
        }

        throw new NotSupportedException();
    }

    public ILayeredDataConverter CreateConverter(string converterName, int layer = 0)
    {
        if (layer != 0)
        {
            if (_layeredConverters.TryGetValue(layer, out var converters))
            {
                if (converters.TryGetValue(converterName, out var converter))
                {
                    return converter;
                }
            }
        }
        else
        {
            if (_converters.TryGetValue(converterName, out var converter))
            {
                return converter;
            }
        }

        throw new NotSupportedException();
    }
}