namespace MyTelegram.Domain.Aggregates.Messaging;

public class MessageViewLogAggregate : AggregateRoot<MessageViewLogAggregate, MessageViewLogId>,
    IApply<CheckMessageViewLogSuccessEvent>
{
    public MessageViewLogAggregate(MessageViewLogId id) : base(id)
    {
    }
    public void Apply(CheckMessageViewLogSuccessEvent aggregateEvent)
    {
    }

    public void CheckMessageViewLog(
        RequestInfo requestInfo,
        int messageId)
    {
        Emit(new CheckMessageViewLogSuccessEvent(requestInfo, messageId, !IsNew));
    }
}