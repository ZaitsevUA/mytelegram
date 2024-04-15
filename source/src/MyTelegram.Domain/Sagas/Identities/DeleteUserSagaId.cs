namespace MyTelegram.Domain.Sagas.Identities;

[JsonConverter(typeof(SystemTextJsonSingleValueObjectConverter<DeleteUserSagaId>))]
public class DeleteUserSagaId : SingleValueObject<string>, ISagaId
{
    public DeleteUserSagaId(string value) : base(value)
    {
    }
}
