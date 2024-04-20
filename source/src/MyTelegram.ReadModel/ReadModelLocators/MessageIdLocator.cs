namespace MyTelegram.ReadModel.ReadModelLocators;

public class MessageIdLocator : IMessageIdLocator
{
    public IEnumerable<string> GetReadModelIds(IDomainEvent domainEvent)
    {
        var aggregateEvent = domainEvent.GetAggregateEvent();

        if (domainEvent.AggregateType == typeof(MessageAggregate))
        {
            if (aggregateEvent is ReplyChannelMessageCompletedEvent replyChannelMessageCompletedEvent)
            {
                if (replyChannelMessageCompletedEvent is { PostChannelId: not null, PostMessageId: not null })
                {
                    yield return MessageId.Create(replyChannelMessageCompletedEvent.PostChannelId.Value,
                        replyChannelMessageCompletedEvent.PostMessageId.Value).Value;
                }
            }

            yield return domainEvent.GetIdentity().Value;
        }
        else if (domainEvent.AggregateType == typeof(SendMessageSaga))
        {
            switch (aggregateEvent)
            {
                case PostChannelIdUpdatedEvent postChannelIdUpdatedEvent:
                    yield return MessageId.Create(postChannelIdUpdatedEvent.ChannelId,
                        postChannelIdUpdatedEvent.MessageId).Value;
                    break;
                case SendOutboxMessageCompletedEvent sendOutboxMessageSuccessEvent:
                    yield return MessageId.Create(sendOutboxMessageSuccessEvent.MessageItem.OwnerPeer.PeerId,
                        sendOutboxMessageSuccessEvent.MessageItem.MessageId).Value;
                    break;
                case ReceiveInboxMessageCompletedEvent receiveInboxMessageSuccessEvent:
                    yield return MessageId.Create(receiveInboxMessageSuccessEvent.MessageItem.OwnerPeer.PeerId,
                        receiveInboxMessageSuccessEvent.MessageItem.MessageId).Value;
                    break;
            }
        }
    }
}