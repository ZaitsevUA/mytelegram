namespace MyTelegram.Domain.Aggregates.PeerSettings;

public class PeerSettingsSnapshot(
    ValueObjects.PeerSettings peerSettings,
    bool hidePeerSettingsBar,
    long ownerPeerId,
    long peerId)
    : ISnapshot
{
    public ValueObjects.PeerSettings PeerSettings { get; } = peerSettings;
    public bool HidePeerSettingsBar { get; private set; } = hidePeerSettingsBar;
    public long OwnerPeerId { get; private set; } = ownerPeerId;
    public long PeerId { get; private set; } = peerId;
}