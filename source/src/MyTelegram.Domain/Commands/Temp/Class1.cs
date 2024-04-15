using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyTelegram.Domain.Aggregates.Temp;
using MyTelegram.Domain.Sagas;

namespace MyTelegram.Domain.Commands.Temp;

public class StartForwardMessagesCommand : RequestCommand2<TempAggregate, TempId, IExecutionResult>
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

    public StartForwardMessagesCommand(TempId aggregateId, RequestInfo requestInfo, bool silent, bool background, bool withMyScore, bool dropAuthor,
        bool dropMediaCaptions, bool noForwards, Peer fromPeer, Peer toPeer,
        List<int> messageIds, List<long> randomIds, int? scheduleDate, Peer? sendAs, bool forwardFromLinkedChannel) : base(aggregateId, requestInfo)
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

public class StartDeleteReplyMessagesCommand : RequestCommand2<TempAggregate, TempId, IExecutionResult>
{
    public long ChannelId { get; }
    public List<int> MessageIds { get; }
    public int NewTopMessageId { get; }

    public StartDeleteReplyMessagesCommand(TempId aggregateId, RequestInfo requestInfo, long channelId, List<int> messageIds, int newTopMessageId) : base(aggregateId, requestInfo)
    {
        ChannelId = channelId;
        MessageIds = messageIds;
        NewTopMessageId = newTopMessageId;
    }
}

public class StartSetDiscussionGroupCommand : RequestCommand2<TempAggregate, TempId, IExecutionResult>
{
    public long BroadcastChannelId { get; }
    public long? DiscussionGroupChannelId { get; }

    public StartSetDiscussionGroupCommand(TempId aggregateId, RequestInfo requestInfo, long broadcastChannelId, long? discussionGroupChannelId) : base(aggregateId, requestInfo)
    {
        BroadcastChannelId = broadcastChannelId;
        DiscussionGroupChannelId = discussionGroupChannelId;
    }
}

public class StartPinForwardedChannelMessageCommand : RequestCommand2<TempAggregate, TempId, IExecutionResult>
{
    public long ChannelId { get; }
    public int MessageId { get; }

    public StartPinForwardedChannelMessageCommand(TempId aggregateId, RequestInfo requestInfo, long channelId, int messageId) : base(aggregateId, requestInfo)
    {
        ChannelId = channelId;
        MessageId = messageId;
    }
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