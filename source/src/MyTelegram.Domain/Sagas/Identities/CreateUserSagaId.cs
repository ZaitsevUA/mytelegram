namespace MyTelegram.Domain.Sagas.Identities;

[JsonConverter(typeof(SystemTextJsonSingleValueObjectConverter<CreateUserSagaId>))]
public class CreateUserSagaId : SingleValueObject<string>, ISagaId
{
    public CreateUserSagaId(string value) : base(value)
    {
    }
}