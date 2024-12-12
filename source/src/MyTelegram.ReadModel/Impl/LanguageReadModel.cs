using MyTelegram.Domain.Aggregates.Language;

namespace MyTelegram.ReadModel.Impl;

public class LanguageReadModel : ILanguageReadModel,
    IAmReadModelFor<LanguageAggregate, LanguageId, LanguageCreatedEvent>
{
    public DeviceType Platform { get; private set; }
    public string Name { get; private set; }
    public string NativeName { get; private set; }
    public string LanguageCode { get; private set; }
    public string PluralCode { get; private set; }
    public string TranslationsUrl { get; private set; }
    public bool IsEnabled { get; private set; }
    public int TranslatedCount { get; private set; }
    public int LanguageVersion { get; private set; }
    public virtual string Id { get; private set; } = null!;
    public virtual long? Version { get; set; }
    public Task ApplyAsync(IReadModelContext context, IDomainEvent<LanguageAggregate, LanguageId, LanguageCreatedEvent> domainEvent, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}