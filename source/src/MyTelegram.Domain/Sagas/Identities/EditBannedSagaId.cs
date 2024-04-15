namespace MyTelegram.Domain.Sagas.Identities;

[JsonConverter(typeof(SystemTextJsonSingleValueObjectConverter<EditBannedSagaId>))]
public class EditBannedSagaId : SingleValueObject<string>, ISagaId
{
    public EditBannedSagaId(string value) : base(value)
    {
    }
}