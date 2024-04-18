namespace MyTelegram.Domain.Events.Dialog;

public class DeleteUserMessagesStartedEvent(
    RequestInfo requestInfo,
    bool revoke,
    long toUserId,
    List<int> messageIds,
    bool isClearHistory,
    Guid correlationId)
    : RequestAggregateEvent2<DialogAggregate, DialogId>(requestInfo), IHasCorrelationId
{
    public bool Revoke { get; } = revoke;
    public long ToUserId { get; } = toUserId;
    public List<int> MessageIds { get; } = messageIds;
    public bool IsClearHistory { get; } = isClearHistory;
    public Guid CorrelationId { get; } = correlationId;
}
