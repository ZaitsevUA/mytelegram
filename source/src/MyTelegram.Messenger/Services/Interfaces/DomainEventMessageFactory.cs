namespace MyTelegram.Messenger.Services.Interfaces;

public class DomainEventMessageFactory(IEventJsonSerializer eventJsonSerializer) : IDomainEventMessageFactory
{
    public DomainEventMessage CreateDomainEventMessage(IDomainEvent domainEvent)
    {
        var serializedEvent = eventJsonSerializer.Serialize(
            domainEvent.GetAggregateEvent(),
            domainEvent.Metadata);

        return new DomainEventMessage(domainEvent.Metadata.EventId.Value, serializedEvent.SerializedData,
            domainEvent.Metadata);
    }
}