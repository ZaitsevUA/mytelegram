namespace MyTelegram.Domain.Sagas.Identities;

[JsonConverter(typeof(SystemTextJsonSingleValueObjectConverter<EditChatPhotoSagaId>))]
public class EditChatPhotoSagaId : SingleValueObject<string>, ISagaId
{
    public EditChatPhotoSagaId(string value) : base(value)
    {
    }
}
