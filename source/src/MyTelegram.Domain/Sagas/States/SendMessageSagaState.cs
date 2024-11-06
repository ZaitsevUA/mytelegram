namespace MyTelegram.Domain.Sagas.States;

public class SendMessageSagaState : AggregateState<SendMessageSaga, SendMessageSagaId, SendMessageSagaState>,
    IApply<SendMessageSagaStartedSagaEvent>,
    IApply<SendOutboxMessageCompletedSagaEvent>,
    IApply<ReceiveInboxMessageCompletedSagaEvent>,
    IApply<ReplyChannelMessageCompletedEvent>,
    IApply<ReplyBroadcastChannelCompletedSagaEvent>,
    IApply<PostChannelIdUpdatedSagaEvent>
{
    public RequestInfo RequestInfo { get; set; } = default!;
    public MessageItem MessageItem { get; set; } = default!;
    public List<long>? MentionedUserIds { get; private set; }
    public int GroupItemCount { get; set; }
    public long? LinkedChannelId { get; set; }
    public List<long>? ChatMembers { get; private set; } = new();
    public List<InboxItem> InboxItems { get; private set; } = new();

    public Dictionary<long, int> ReplyToMsgItems { get; private set; } = new();

    public void Apply(SendMessageSagaStartedSagaEvent aggregateEvent)
    {
        RequestInfo = aggregateEvent.RequestInfo;
        MessageItem = aggregateEvent.MessageItem;
        MentionedUserIds = aggregateEvent.MentionedUserIds;
        GroupItemCount = aggregateEvent.GroupItemCount;
        LinkedChannelId = aggregateEvent.LinkedChannelId;
        ChatMembers = aggregateEvent.ChatMembers;

        if (aggregateEvent.ReplyToMsgItems?.Count > 0)
        {
            ReplyToMsgItems = aggregateEvent.ReplyToMsgItems.ToDictionary(k => k.UserId, v => v.MessageId);
        }
    }

    public void Apply(SendOutboxMessageCompletedSagaEvent aggregateEvent)
    {
    }

    public void Apply(ReceiveInboxMessageCompletedSagaEvent aggregateEvent)
    {
        InboxItems.Add(new(aggregateEvent.MessageItem.OwnerPeer.PeerId, aggregateEvent.MessageItem.MessageId));
    }

    public bool IsCreateInboxMessagesCompleted()
    {
        switch (MessageItem.ToPeer.PeerType)
        {
            case PeerType.User:
                return InboxItems.Count == 1;
            case PeerType.Chat:
                return InboxItems.Count == ChatMembers?.Count - 1;
        }

        return false;
    }

    public void Apply(ReplyChannelMessageCompletedEvent aggregateEvent)
    {
    }

    public void Apply(ReplyBroadcastChannelCompletedSagaEvent aggregateEvent)
    {
    }

    public void Apply(PostChannelIdUpdatedSagaEvent aggregateEvent)
    {
    }
}