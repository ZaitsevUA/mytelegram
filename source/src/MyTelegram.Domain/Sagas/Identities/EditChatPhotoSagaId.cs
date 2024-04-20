namespace MyTelegram.Domain.Sagas.Identities;

[JsonConverter(typeof(SystemTextJsonSingleValueObjectConverter<EditChatPhotoSagaId>))]
public class EditChatPhotoSagaId(string value) : SingleValueObject<string>(value), ISagaId;
