namespace MyTelegram.Domain.Events.User;

public class SendUserMessageStartedEvent(
    long reqMsgId,
    Guid correlationId) : AggregateEvent<UserAggregate, UserId>, IHasCorrelationId
{
    public long ReqMsgId { get; } = reqMsgId;

    public Guid CorrelationId { get; } = correlationId;
}
