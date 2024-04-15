namespace MyTelegram.Domain.Events.Messaging;

public class MessageReplyUpdatedEvent : AggregateEvent<MessageAggregate, MessageId>
{
    public long OwnerChannelId { get; }
    public long ChannelId { get; }
    public int MessageId { get; }
    public int Pts { get; }

    public MessageReplyUpdatedEvent(long ownerChannelId, long channelId, int messageId,int pts)
    {
        OwnerChannelId = ownerChannelId;
        ChannelId = channelId;
        MessageId = messageId;
        Pts = pts;
    }
}