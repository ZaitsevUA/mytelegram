namespace MyTelegram.Domain.Commands.Messaging;

public class AddInboxItemsToOutboxMessageCommand : RequestCommand2<MessageAggregate, MessageId, IExecutionResult>
{
    public List<InboxItem> InboxItems { get; }

    public AddInboxItemsToOutboxMessageCommand(MessageId aggregateId,
        RequestInfo requestInfo,
        List<InboxItem> inboxItems) : base(aggregateId, requestInfo)
    {
        InboxItems = inboxItems;
    }
}