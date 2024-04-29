namespace MyTelegram.Domain.Sagas.Identities;

[JsonConverter(typeof(SystemTextJsonSingleValueObjectConverter<AddChatUserSagaId>))]
public class AddChatUserSagaId(string value) : SingleValueObject<string>(value), ISagaId;