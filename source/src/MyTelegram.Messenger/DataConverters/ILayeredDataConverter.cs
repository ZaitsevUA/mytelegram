namespace MyTelegram.Messenger.DataConverters;

public interface ILayeredDataConverter
{
    string Name { get; }
    int Layer => Layers.LayerLatest;
    //TOutput Convert<TInput, TOutput>(TInput input);
}

public interface ILayeredDataConverter<in TInput, out TOutput> : ILayeredDataConverter
{
    TOutput Convert(TInput data);
}
