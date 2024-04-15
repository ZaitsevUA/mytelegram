namespace MyTelegram.Domain.Sagas.Identities;

[JsonConverter(typeof(SystemTextJsonSingleValueObjectConverter<RecoveryPasswordSagaId>))]
public class RecoveryPasswordSagaId : SingleValueObject<string>, ISagaId
{
    public RecoveryPasswordSagaId(string value) : base(value)
    {
    }
}
