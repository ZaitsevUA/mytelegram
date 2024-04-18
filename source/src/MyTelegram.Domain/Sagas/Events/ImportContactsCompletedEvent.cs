namespace MyTelegram.Domain.Sagas.Events;

public class ImportContactsCompletedEvent(
    RequestInfo requestInfo,
    List<PhoneContact> phoneContacts)
    : RequestAggregateEvent2<ImportContactsSaga, ImportContactsSagaId>(requestInfo)
{
    public List<PhoneContact> PhoneContacts { get; } = phoneContacts;
}
