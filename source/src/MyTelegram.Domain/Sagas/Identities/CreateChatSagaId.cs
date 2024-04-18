namespace MyTelegram.Domain.Sagas.Identities;

[JsonConverter(typeof(SystemTextJsonSingleValueObjectConverter<CreateChatSagaId>))]
public class CreateChatSagaId(string value) : SingleValueObject<string>(value), ISagaId;
