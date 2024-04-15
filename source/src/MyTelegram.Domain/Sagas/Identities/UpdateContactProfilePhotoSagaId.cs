namespace MyTelegram.Domain.Sagas.Identities;

[JsonConverter(typeof(SystemTextJsonSingleValueObjectConverter<UpdateContactProfilePhotoSagaId>))]
public class UpdateContactProfilePhotoSagaId : SingleValueObject<string>, ISagaId
{
    public UpdateContactProfilePhotoSagaId(string value) : base(value)
    {
    }
}