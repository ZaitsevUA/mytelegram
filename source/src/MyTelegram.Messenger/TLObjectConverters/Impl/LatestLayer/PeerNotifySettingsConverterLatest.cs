namespace MyTelegram.Messenger.TLObjectConverters.Impl.LatestLayer;

public class PeerNotifySettingsConverterLatest(IObjectMapper objectMapper) : IPeerNotifySettingsConverterLatest
{
    public virtual int Layer => Layers.LayerLatest;
    public int RequestLayer { get; set; }
    public IPeerNotifySettings ToPeerNotifySettings(IPeerNotifySettingsReadModel? readModel)
    {
        return ToPeerNotifySettings(readModel?.NotifySettings);
    }

    public virtual IPeerNotifySettings ToPeerNotifySettings(PeerNotifySettings? peerNotifySettings)
    {
        var settings =
            objectMapper.Map<PeerNotifySettings, TPeerNotifySettings>(peerNotifySettings ??
                                                                       PeerNotifySettings.DefaultSettings);
        settings.IosSound = new TNotificationSoundDefault();
        settings.AndroidSound = new TNotificationSoundLocal
        {
            Title = "default",
            Data = "default"
        };
        settings.OtherSound = new TNotificationSoundLocal
        {
            Title = "default",
            Data = "default"
        };

        return settings;
    }
}