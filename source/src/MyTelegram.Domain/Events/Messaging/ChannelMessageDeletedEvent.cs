namespace MyTelegram.Domain.Events.Messaging;

public class ChannelMessageDeletedEvent : RequestAggregateEvent2<MessageAggregate, MessageId>
{
    public long ChannelId { get; }
    public int MessageId { get; }
    public bool IsThisMessageForwardFromChannelPost { get; }
    public long? PostChannelId { get; }
    public int? PostMessageId { get; }

    public ChannelMessageDeletedEvent(RequestInfo requestInfo, long channelId, int messageId, bool isThisMessageForwardFromChannelPost, long? postChannelId, int? postMessageId) : base(requestInfo)
    {
        ChannelId = channelId;
        MessageId = messageId;
        IsThisMessageForwardFromChannelPost = isThisMessageForwardFromChannelPost;
        PostChannelId = postChannelId;
        PostMessageId = postMessageId;
    }
}