namespace MyTelegram.Domain.Events.PeerNotifySettings;

public class PeerNotifySettingsUpdatedEvent(
    RequestInfo requestInfo,
    long ownerPeerId,
    PeerType peerType,
    long peerId,
    ValueObjects.PeerNotifySettings peerNotifySettings)
    : RequestAggregateEvent2<PeerNotifySettingsAggregate, PeerNotifySettingsId>(requestInfo)
{
    public long OwnerPeerId { get; } = ownerPeerId;
    public long PeerId { get; } = peerId;

    public ValueObjects.PeerNotifySettings PeerNotifySettings { get; } = peerNotifySettings;

    public PeerType PeerType { get; } = peerType;
}
