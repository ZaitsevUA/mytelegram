namespace MyTelegram.Domain.Sagas.States;

public class CreateChannelSagaState : AggregateState<CreateChannelSaga, CreateChannelSagaId, CreateChannelSagaState>,
    IApply<CreateChannelSagaStartedSagaEvent>
{
    public RequestInfo RequestInfo { get; private set; }
    public string MessageActionData { get; private set; }
    public long RandomId { get; private set; }
    public bool MigratedFromChat { get; private set; }
    public bool AutoCreateFromChat { get; private set; }
    public bool Broadcast { get; private set; }
    public void Apply(CreateChannelSagaStartedSagaEvent aggregateEvent)
    {
        RequestInfo = aggregateEvent.RequestInfo;
        MessageActionData = aggregateEvent.MessageActionData;
        RandomId = aggregateEvent.RandomId;
        MigratedFromChat = aggregateEvent.MigratedFromChat;
        AutoCreateFromChat = aggregateEvent.AutoCreateFromChat;
        Broadcast = aggregateEvent.Broadcast;
    }
}