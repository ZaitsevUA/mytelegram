namespace MyTelegram.Domain.Sagas.Identities;

[JsonConverter(typeof(SystemTextJsonSingleValueObjectConverter<RecoveryPasswordSagaId>))]
public class RecoveryPasswordSagaId(string value) : SingleValueObject<string>(value), ISagaId;
