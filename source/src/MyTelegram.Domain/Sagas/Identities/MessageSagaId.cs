namespace MyTelegram.Domain.Sagas.Identities;

[JsonConverter(typeof(SystemTextJsonSingleValueObjectConverter<MessageSagaId>))]
public class MessageSagaId : SingleValueObject<string>, ISagaId
{
    public MessageSagaId(string value) : base(value)
    {
    }
}
