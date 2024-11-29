namespace MyTelegram.Messenger.Services.Caching;

public interface ICachedReadModelManager
{
    Task ApplyUpdatesAsync(IReadOnlyCollection<IDomainEvent> domainEvents,
        CancellationToken cancellationToken);
}