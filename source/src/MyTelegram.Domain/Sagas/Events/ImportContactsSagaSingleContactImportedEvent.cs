namespace MyTelegram.Domain.Sagas.Events;

public class ImportContactsSagaSingleContactImportedEvent(PhoneContact phoneContact)
    : AggregateEvent<ImportContactsSaga, ImportContactsSagaId>
{
    public PhoneContact PhoneContact { get; } = phoneContact;
}
