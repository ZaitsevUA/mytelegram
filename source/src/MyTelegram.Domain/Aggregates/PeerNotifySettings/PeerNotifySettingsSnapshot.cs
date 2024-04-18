namespace MyTelegram.Domain.Aggregates.PeerNotifySettings;

public class PeerNotifySettingsSnapshot(ValueObjects.PeerNotifySettings peerNotifySettings) : ISnapshot
{
    public ValueObjects.PeerNotifySettings PeerNotifySettings { get; } = peerNotifySettings;
}
