using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyTelegram.Domain.Aggregates.Temp;
using MyTelegram.Domain.Sagas;

namespace MyTelegram.Domain.Commands.Temp;

public class StartForwardMessagesCommand(
    TempId aggregateId,
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
    : RequestCommand2<TempAggregate, TempId, IExecutionResult>(aggregateId, requestInfo)
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

public class StartDeleteReplyMessagesCommand(
    TempId aggregateId,
    RequestInfo requestInfo,
    long channelId,
    List<int> messageIds,
    int newTopMessageId)
    : RequestCommand2<TempAggregate, TempId, IExecutionResult>(aggregateId, requestInfo)
{
    public long ChannelId { get; } = channelId;
    public List<int> MessageIds { get; } = messageIds;
    public int NewTopMessageId { get; } = newTopMessageId;
}

public class StartSetDiscussionGroupCommand(
    TempId aggregateId,
    RequestInfo requestInfo,
    long broadcastChannelId,
    long? discussionGroupChannelId)
    : RequestCommand2<TempAggregate, TempId, IExecutionResult>(aggregateId, requestInfo)
{
    public long BroadcastChannelId { get; } = broadcastChannelId;
    public long? DiscussionGroupChannelId { get; } = discussionGroupChannelId;
}

public class StartPinForwardedChannelMessageCommand(
    TempId aggregateId,
    RequestInfo requestInfo,
    long channelId,
    int messageId)
    : RequestCommand2<TempAggregate, TempId, IExecutionResult>(aggregateId, requestInfo)
{
    public long ChannelId { get; } = channelId;
    public int MessageId { get; } = messageId;
}

public class StartDeleteMessagesCommand(
    TempId aggregateId,
    RequestInfo requestInfo,
    //List<int> messageIds,
    IReadOnlyCollection<MessageItemToBeDeleted> messageItems,
    bool revoke,
    bool deleteGroupMessagesForEveryone,
    int? newTopMessageId,
    int? newTopMessageIdForOtherParticipant
    )
    : RequestCommand2<TempAggregate, TempId, IExecutionResult>(aggregateId, requestInfo)
{
    //public List<int> MessageIds { get; } = messageIds;
    public IReadOnlyCollection<MessageItemToBeDeleted> MessageItems { get; } = messageItems;
    public bool Revoke { get; } = revoke;
    public bool DeleteGroupMessagesForEveryone { get; } = deleteGroupMessagesForEveryone;
    public int? NewTopMessageId { get; } = newTopMessageId;
    public int? NewTopMessageIdForOtherParticipant { get; } = newTopMessageIdForOtherParticipant;
}

public class StartDeleteHistoryCommand(
        TempId aggregateId,
        RequestInfo requestInfo,
        //List<int> messageIds,
        IReadOnlyCollection<MessageItemToBeDeleted> messageItems,
        bool revoke,
        bool deleteGroupMessagesForEveryone)
    : RequestCommand2<TempAggregate, TempId, IExecutionResult>(aggregateId, requestInfo)
{
    //public List<int> MessageIds { get; } = messageIds;
    public IReadOnlyCollection<MessageItemToBeDeleted> MessageItems { get; } = messageItems;
    public bool Revoke { get; } = revoke;
    public bool DeleteGroupMessagesForEveryone { get; } = deleteGroupMessagesForEveryone;
}

public class StartDeleteChannelMessagesCommand(TempId aggregateId,
    RequestInfo requestInfo,
    long channelId,
    List<int> messageIds,
    int newTopMessageId,
    int? newTopMessageIdForDiscussionGroup,
    long? discussionGroupChannelId,
    IReadOnlyCollection<int>? repliesMessageIds) : RequestCommand2<TempAggregate, TempId, IExecutionResult>(aggregateId, requestInfo)
{
    public long ChannelId { get; } = channelId;
    public List<int> MessageIds { get; } = messageIds;
    public int NewTopMessageId { get; } = newTopMessageId;
    public int? NewTopMessageIdForDiscussionGroup { get; } = newTopMessageIdForDiscussionGroup;
    public long? DiscussionGroupChannelId { get; } = discussionGroupChannelId;
    public IReadOnlyCollection<int>? RepliesMessageIds { get; } = repliesMessageIds;
}


public class StartDeleteParticipantHistoryCommand(TempId aggregateId,
    RequestInfo requestInfo, long channelId, List<int> messageIds, int newTopMessageId) : RequestCommand2<TempAggregate, TempId, IExecutionResult>(aggregateId, requestInfo)
{
    public long ChannelId { get; } = channelId;
    public List<int> MessageIds { get; } = messageIds;
    public int NewTopMessageId { get; } = newTopMessageId;
}