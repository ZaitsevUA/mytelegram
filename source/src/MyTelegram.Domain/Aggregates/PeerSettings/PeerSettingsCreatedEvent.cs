namespace MyTelegram.Domain.Aggregates.PeerSettings;

public class PeerSettingsCreatedEvent(ValueObjects.PeerSettings peerSettings)
    : AggregateEvent<PeerSettingsAggregate, PeerSettingsId>
{
    public ValueObjects.PeerSettings PeerSettings { get; } = peerSettings;
}