namespace MyTelegram.Domain.Sagas.Identities;

[JsonConverter(typeof(SystemTextJsonSingleValueObjectConverter<EditAdminSagaId>))]
public class EditAdminSagaId : SingleValueObject<string>, ISagaId
{
    public EditAdminSagaId(string value) : base(value)
    {
    }
}