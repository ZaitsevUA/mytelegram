namespace MyTelegram.Domain.Aggregates.PeerSettings;

public class PeerSettingsBarHiddenEvent(long ownerPeerId, long peerId)
    : AggregateEvent<PeerSettingsAggregate, PeerSettingsId>
{
    public long OwnerPeerId { get; } = ownerPeerId;
    public long PeerId { get; } = peerId;
}