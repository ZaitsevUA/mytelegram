using MyTelegram.Domain.Aggregates.Temp;

namespace MyTelegram.Domain.Events.Temp;

public class ForwardMessagesStartedEvent : RequestAggregateEvent2<TempAggregate, TempId>
{
    public bool Silent { get; }
    public bool Background { get; }
    public bool WithMyScore { get; }
    public bool DropAuthor { get; }
    public bool DropMediaCaptions { get; }
    public bool NoForwards { get; }
    public Peer FromPeer { get; }
    public Peer ToPeer { get; }
    public List<int> MessageIds { get; }
    public List<long> RandomIds { get; }
    public int? ScheduleDate { get; }
    public Peer? SendAs { get; }
    public bool ForwardFromLinkedChannel { get; }

    public ForwardMessagesStartedEvent(RequestInfo requestInfo, bool silent, bool background, bool withMyScore, bool dropAuthor,
        bool dropMediaCaptions, bool noForwards, Peer fromPeer,
        Peer toPeer,
        List<int> messageIds, List<long> randomIds, int? scheduleDate, Peer? sendAs, bool forwardFromLinkedChannel) : base(requestInfo)
    {
        Silent = silent;
        Background = background;
        WithMyScore = withMyScore;
        DropAuthor = dropAuthor;
        DropMediaCaptions = dropMediaCaptions;
        NoForwards = noForwards;
        FromPeer = fromPeer;
        ToPeer = toPeer;
        MessageIds = messageIds;
        RandomIds = randomIds;
        ScheduleDate = scheduleDate;
        SendAs = sendAs;
        ForwardFromLinkedChannel = forwardFromLinkedChannel;
    }
}

public class DeleteReplyMessagesStartedEvent : RequestAggregateEvent2<TempAggregate, TempId>
{
    public long ChannelId { get; }
    public List<int> MessageIds { get; }
    public int NewTopMessageId { get; }

    public DeleteReplyMessagesStartedEvent(RequestInfo requestInfo, long channelId, List<int> messageIds, int newTopMessageId) : base(requestInfo)
    {
        ChannelId = channelId;
        MessageIds = messageIds;
        NewTopMessageId = newTopMessageId;
    }
}

public class SetChannelDiscussionGroupStartedEvent : RequestAggregateEvent2<TempAggregate, TempId>
{
    public long BroadcastChannelId { get; }
    public long? DiscussionGroupChannelId { get; }

    public SetChannelDiscussionGroupStartedEvent(RequestInfo requestInfo, long broadcastChannelId, long? discussionGroupChannelId) : base(requestInfo)
    {
        BroadcastChannelId = broadcastChannelId;
        DiscussionGroupChannelId = discussionGroupChannelId;
    }
}

public class PinForwardedChannelMessageStartedEvent
    (RequestInfo requestInfo, long channelId, int messageId) : RequestAggregateEvent2<TempAggregate, TempId>(
        requestInfo)
{
    public long ChannelId { get; } = channelId;
    public int MessageId { get; } = messageId;
}

public class DeleteMessagesStartedEvent(RequestInfo requestInfo, IReadOnlyCollection<MessageItemToBeDeleted> messageItems, bool revoke, bool deleteGroupMessagesForEveryone, int? newTopMessageId, int? newTopMessageIdForOtherParticipant) : RequestAggregateEvent2<TempAggregate, TempId>(requestInfo)
{
    public IReadOnlyCollection<MessageItemToBeDeleted> MessageItems { get; } = messageItems;
    public bool Revoke { get; } = revoke;
    public bool DeleteGroupMessagesForEveryone { get; } = deleteGroupMessagesForEveryone;
    public int? NewTopMessageId { get; } = newTopMessageId;
    public int? NewTopMessageIdForOtherParticipant { get; } = newTopMessageIdForOtherParticipant;
}

public class DeleteHistoryStartedEvent
    (RequestInfo requestInfo, IReadOnlyCollection<MessageItemToBeDeleted> messageItems, bool revoke, bool deleteGroupMessagesForEveryone) :
        RequestAggregateEvent2<TempAggregate, TempId>(requestInfo)
{
    //public List<int> MessageIds { get; } = messageIds;
    public IReadOnlyCollection<MessageItemToBeDeleted> MessageItems { get; } = messageItems;
    public bool Revoke { get; } = revoke;
    public bool DeleteGroupMessagesForEveryone { get; } = deleteGroupMessagesForEveryone;
}

public class DeleteChannelMessagesStartedEvent(RequestInfo requestInfo, long channelId, List<int> messageIds, int newTopMessageId,
    int? newTopMessageIdForDiscussionGroup, long? discussionGroupChannelId, IReadOnlyCollection<int>? repliesMessageIds) : RequestAggregateEvent2<TempAggregate, TempId>(requestInfo)
{
    public long ChannelId { get; } = channelId;
    public List<int> MessageIds { get; } = messageIds;
    public int NewTopMessageId { get; } = newTopMessageId;
    public int? NewTopMessageIdForDiscussionGroup { get; } = newTopMessageIdForDiscussionGroup;
    public long? DiscussionGroupChannelId { get; } = discussionGroupChannelId;
    public IReadOnlyCollection<int>? RepliesMessageIds { get; } = repliesMessageIds;
}

public class DeleteParticipantHistoryStartedEvent(RequestInfo requestInfo, long channelId, List<int> messageIds, int newTopMessageId) : RequestAggregateEvent2<TempAggregate, TempId>(requestInfo)
{
    public long ChannelId { get; } = channelId;
    public List<int> MessageIds { get; } = messageIds;
    public int NewTopMessageId { get; } = newTopMessageId;
}