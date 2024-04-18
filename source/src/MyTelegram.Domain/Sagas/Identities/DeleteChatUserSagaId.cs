namespace MyTelegram.Domain.Sagas.Identities;

[JsonConverter(typeof(SystemTextJsonSingleValueObjectConverter<DeleteChatUserSagaId>))]
public class DeleteChatUserSagaId(string value) : SingleValueObject<string>(value), ISagaId;
