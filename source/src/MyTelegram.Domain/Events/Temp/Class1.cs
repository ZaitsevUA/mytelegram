using MyTelegram.Domain.Aggregates.Temp;

namespace MyTelegram.Domain.Events.Temp;

public class ForwardMessagesStartedEvent(
    RequestInfo requestInfo,
    bool silent,
    bool background,
    bool withMyScore,
    bool dropAuthor,
    bool dropMediaCaptions,
    bool noForwards,
    Peer fromPeer,
    Peer toPeer,
    List<int> messageIds,
    List<long> randomIds,
    int? scheduleDate,
    Peer? sendAs,
    bool forwardFromLinkedChannel)
    : RequestAggregateEvent2<TempAggregate, TempId>(requestInfo)
{
    public bool Silent { get; } = silent;
    public bool Background { get; } = background;
    public bool WithMyScore { get; } = withMyScore;
    public bool DropAuthor { get; } = dropAuthor;
    public bool DropMediaCaptions { get; } = dropMediaCaptions;
    public bool NoForwards { get; } = noForwards;
    public Peer FromPeer { get; } = fromPeer;
    public Peer ToPeer { get; } = toPeer;
    public List<int> MessageIds { get; } = messageIds;
    public List<long> RandomIds { get; } = randomIds;
    public int? ScheduleDate { get; } = scheduleDate;
    public Peer? SendAs { get; } = sendAs;
    public bool ForwardFromLinkedChannel { get; } = forwardFromLinkedChannel;
}

public class DeleteReplyMessagesStartedEvent(
    RequestInfo requestInfo,
    long channelId,
    List<int> messageIds,
    int newTopMessageId)
    : RequestAggregateEvent2<TempAggregate, TempId>(requestInfo)
{
    public long ChannelId { get; } = channelId;
    public List<int> MessageIds { get; } = messageIds;
    public int NewTopMessageId { get; } = newTopMessageId;
}

public class SetChannelDiscussionGroupStartedEvent(
    RequestInfo requestInfo,
    long broadcastChannelId,
    long? discussionGroupChannelId)
    : RequestAggregateEvent2<TempAggregate, TempId>(requestInfo)
{
    public long BroadcastChannelId { get; } = broadcastChannelId;
    public long? DiscussionGroupChannelId { get; } = discussionGroupChannelId;
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