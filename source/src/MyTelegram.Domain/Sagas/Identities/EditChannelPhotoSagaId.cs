namespace MyTelegram.Domain.Sagas.Identities;

[JsonConverter(typeof(SystemTextJsonSingleValueObjectConverter<EditChannelPhotoSagaId>))]
public class EditChannelPhotoSagaId : SingleValueObject<string>, ISagaId
{
    public EditChannelPhotoSagaId(string value) : base(value)
    {
    }
}
