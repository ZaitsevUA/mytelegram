namespace MyTelegram.Domain.Sagas.Identities;

[JsonConverter(typeof(SystemTextJsonSingleValueObjectConverter<EditChatTitleSagaId>))]
public class EditChatTitleSagaId(string value) : SingleValueObject<string>(value), ISagaId;