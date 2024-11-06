namespace MyTelegram.Domain.Sagas.Events;

public class CreateChannelSagaStartedSagaEvent(
    RequestInfo requestInfo,
    bool broadcast,
    string messageActionData,
    long randomId,
    bool migratedFromChat,
    bool autoCreateFromChat)
    : AggregateEvent<CreateChannelSaga, CreateChannelSagaId>
{
    public RequestInfo RequestInfo { get; } = requestInfo;
    public bool Broadcast { get; } = broadcast;
    public string MessageActionData { get; } = messageActionData;
    public long RandomId { get; } = randomId;
    public bool MigratedFromChat { get; } = migratedFromChat;
    public bool AutoCreateFromChat { get; } = autoCreateFromChat;
}