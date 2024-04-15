namespace MyTelegram.ReadModel.ReadModelLocators;

public class ChannelReadModelLocator : IChannelReadModelLocator
{
    public IEnumerable<string> GetReadModelIds(IDomainEvent domainEvent)
    {
        var aggregateEvent = domainEvent.GetAggregateEvent();
        if (domainEvent.AggregateType == typeof(ChannelAggregate))
        {
            yield return domainEvent.GetIdentity().Value;
        }
        else
        {
            switch (aggregateEvent)
            {
                case DeleteChannelMessagesCompletedEvent deleteChannelMessagesCompletedEvent:
                    yield return ChannelId.Create(deleteChannelMessagesCompletedEvent.ChannelId).Value;
                    break;
                case DeleteChannelHistoryCompletedEvent deleteChannelHistoryCompletedEvent:
                    yield return ChannelId.Create(deleteChannelHistoryCompletedEvent.ChannelId).Value;
                    break;
                case DeleteReplyMessagesCompletedEvent deleteReplyMessagesCompletedEvent:
                    yield return ChannelId.Create(deleteReplyMessagesCompletedEvent.ChannelId).Value;
                    break;
                case ChannelMemberJoinedEvent channelMemberJoinedEvent:
                    yield return ChannelId.Create(channelMemberJoinedEvent.ChannelId).Value;
                    break;
                case ChannelMemberLeftEvent channelMemberLeftEvent:
                    yield return ChannelId.Create(channelMemberLeftEvent.ChannelId).Value;
                    break;
                case ChannelMemberBannedRightsChangedEvent channelMemberBannedRightsChangedEvent:
                    yield return ChannelId.Create(channelMemberBannedRightsChangedEvent.ChannelId).Value;
                    break;
            }
        }
    }
}
