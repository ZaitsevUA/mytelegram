namespace MyTelegram.Messenger.DataConverters;

public interface ILayeredDataConverterFactory
{
    ILayeredDataConverter<TInput, TOutput>
        CreateConverter<TInput, TOutput>(string converterName, int layer = 0);

    ILayeredDataConverter CreateConverter(string converterName, int layer = 0);
}