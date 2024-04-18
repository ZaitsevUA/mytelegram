namespace MyTelegram.Domain.Events.PeerNotifySettings;

public class PeerNotifySettingsUpdatedEvent(
    RequestInfo requestInfo,
    long ownerPeerId,
    PeerType peerType,
    long peerId,
    ValueObjects.PeerNotifySettings peerNotifySettings)
    : RequestAggregateEvent2<PeerNotifySettingsAggregate, PeerNotifySettingsId>(requestInfo)
{
    //ShowPreviews = showPreviews;
    //Silent = silent;
    //MuteUntil = muteUntil;
    //Sound = sound;

    public long OwnerPeerId { get; } = ownerPeerId;
    public long PeerId { get; } = peerId;

    public ValueObjects.PeerNotifySettings PeerNotifySettings { get; } = peerNotifySettings;

    public PeerType PeerType { get; } = peerType;
    //public bool ShowPreviews { get; }// = true;
    //public bool Silent { get; }
    //public int MuteUntil { get; }// = int.MaxValue;
    //public string Sound { get; }// = "default";
}