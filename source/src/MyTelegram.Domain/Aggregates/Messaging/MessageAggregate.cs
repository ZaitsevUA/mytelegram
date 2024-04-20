namespace MyTelegram.Domain.Aggregates.Messaging;

public class MessageAggregate : SnapshotAggregateRoot<MessageAggregate, MessageId, MessageSnapshot>
{
    private readonly MessageState _state = new();

    public MessageAggregate(MessageId id) : base(id, SnapshotEveryFewVersionsStrategy.Default)
    {
        Register(_state);
    }

    public void AddInboxItemsToOutboxMessage(List<InboxItem> inboxItems)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new InboxItemsAddedToOutboxMessageEvent(inboxItems));
    }

    /// <summary>
    ///     Sender's message id and receiver's message id are independent,add receiver's message id to sender,delete messages
    ///     and pin messages need this
    /// </summary>
    /// <param name="inboxOwnerPeerId"></param>
    /// <param name="inboxMessageId"></param>
    public void AddInboxMessageIdToOutboxMessage(long inboxOwnerPeerId,
        int inboxMessageId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new InboxMessageIdAddedToOutboxMessageEvent(new InboxItem(inboxOwnerPeerId, inboxMessageId)));
    }

    public void CreateInboxMessage(
        RequestInfo requestInfo,
        MessageItem inboxMessageItem,
        int senderMessageId)
    {
        Specs.AggregateIsNew.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new InboxMessageCreatedEvent(requestInfo, inboxMessageItem, senderMessageId));
    }

    public void CreateOutboxMessage(RequestInfo requestInfo,
        MessageItem outboxMessageItem,
        List<long>? mentionedUserIds,
        List<ReplyToMsgItem>? replyToMsgItems,
        bool clearDraft,
        int groupItemCount,
        long? linkedChannelId,
        List<long>? chatMembers)
    {
        Specs.AggregateIsNew.ThrowDomainErrorIfNotSatisfied(this);
        if (outboxMessageItem.Post)
        {
            var reply = new MessageReply(linkedChannelId, 0, 0, null, null);
            outboxMessageItem = outboxMessageItem with { Views = 1, Reply = reply };
        }
        if (!outboxMessageItem.BatchId.HasValue)
        {
            outboxMessageItem = outboxMessageItem with { BatchId = SequentialGuid.Create() };
        }

        Emit(new OutboxMessageCreatedEvent(requestInfo,
            outboxMessageItem,
            mentionedUserIds,
            replyToMsgItems,
            clearDraft,
            groupItemCount,
            linkedChannelId,
            chatMembers
        ));
    }

    public void DeleteChannelMessage(RequestInfo requestInfo)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new ChannelMessageDeletedEvent(requestInfo,
            _state.MessageItem.OwnerPeer.PeerId,
            _state.MessageItem.MessageId,
            _state.MessageItem.IsForwardFromChannelPost,
            _state.MessageItem.FwdHeader?.SavedFromPeer?.PeerId,
            _state.MessageItem.FwdHeader?.SavedFromMsgId));
    }

    public void DeleteInboxMessage(RequestInfo requestInfo)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new InboxMessageDeletedEvent(
            requestInfo,
            _state.MessageItem.OwnerPeer.PeerId,
            _state.MessageItem.MessageId,
            _state.SenderMessageId
        ));
    }

    public void DeleteMessage(RequestInfo requestInfo, int messageId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new MessageDeletedEvent(
            requestInfo,
            _state.MessageItem.ToPeer,
            _state.MessageItem.OwnerPeer.PeerId,
            messageId,
            _state.MessageItem.IsOut,
            _state.MessageItem.SenderPeer.PeerId,
            _state.SenderMessageId,
            _state.InboxItems
        ));
    }

    public void DeleteMessage(RequestInfo requestInfo)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new MessageDeleted4Event(
            requestInfo,
            _state.MessageItem.ToPeer,
            _state.MessageItem.OwnerPeer.PeerId,
            _state.MessageItem.MessageId,
            _state.MessageItem.IsOut,
            _state.MessageItem.SenderPeer.PeerId,
            _state.SenderMessageId,
            _state.InboxItems));
    }
    public void DeleteMessage4(RequestInfo requestInfo)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new MessageDeleted4Event(
            requestInfo,
            _state.MessageItem.ToPeer,
            _state.MessageItem.OwnerPeer.PeerId,
            _state.MessageItem.MessageId,
            _state.MessageItem.IsOut,
            _state.MessageItem.SenderPeer.PeerId,
            _state.SenderMessageId,
            _state.InboxItems
        ));
    }
    public void DeleteOtherParticipantMessage(RequestInfo requestInfo)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
    }

    public void DeleteOtherPartyMessage(RequestInfo requestInfo)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new OtherPartyMessageDeletedEvent(
            requestInfo,
            _state.MessageItem.OwnerPeer.PeerId,
            _state.MessageItem.MessageId));
    }

    public void DeleteOutboxMessage(RequestInfo requestInfo)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new OutboxMessageDeletedEvent(
            requestInfo,
            _state.MessageItem.OwnerPeer.PeerId,
            _state.MessageItem.MessageId,
            _state.InboxItems));
    }

    public void DeleteSelfMessage(RequestInfo requestInfo, int messageId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new SelfMessageDeletedEvent(
            requestInfo,
            _state.MessageItem.OwnerPeer.PeerId,
            messageId,
            _state.MessageItem.IsOut,
            _state.MessageItem.SenderPeer.PeerId,
            _state.SenderMessageId,
            _state.InboxItems
        ));
    }

    public void EditInboxMessage(
        RequestInfo requestInfo,
        int messageId,
        string newMessage,
        int editDate,
        byte[]? entities,
        byte[]? media)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new InboxMessageEditedEvent(
            requestInfo,
            _state.MessageItem.OwnerPeer.PeerId,
            messageId,
            newMessage,
            entities,
            editDate,
            _state.MessageItem.ToPeer,
            media,
            null,
            null));
    }

    public void EditOutboxMessage(RequestInfo requestInfo,
        int messageId,
        string newMessage,
        int editDate,
        byte[]? entities,
        byte[]? media)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        if (_state.MessageItem.Date + MyTelegramServerDomainConsts.EditTimeLimit < DateTime.UtcNow.ToTimestamp())
        {
            //ThrowHelper.ThrowUserFriendlyException(RpcErrorMessages.MessageEditTimeExpired);
            RpcErrors.RpcErrors400.MessageEditTimeExpired.ThrowRpcError();
        }

        if (!_state.MessageItem.IsOut)
        {
            //ThrowHelper.ThrowUserFriendlyException(RpcErrorMessages.MessageAuthorRequired);
            RpcErrors.RpcErrors403.MessageAuthorRequired.ThrowRpcError();
        }

        Emit(new OutboxMessageEditedEvent(requestInfo,
            _state.InboxItems,
            _state.MessageItem,
            _state.MessageItem.MessageId,
            newMessage,
            editDate,
            entities,
            media,
            new List<ReactionCount>(),
            new List<Reaction>()));
    }

    public void ForwardMessage(
        RequestInfo requestInfo,
        long randomId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new MessageForwardedEvent(requestInfo, randomId, _state.MessageItem));
    }

    public void IncrementViews()
    {
        //Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        if (!IsNew)
        {
            Emit(new MessageViewsIncrementedEvent(_state.MessageItem.MessageId, _state.MessageItem.Views ?? 0 + 1));
        }
    }

    public void PinChannelMessage(RequestInfo requestInfo)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new ChannelMessagePinnedEvent(requestInfo, _state.MessageItem.ToPeer.PeerId,
            _state.MessageItem.MessageId));
    }
    public void ReadInboxHistory(RequestInfo requestInfo,
        long readerUid)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new InboxMessageHasReadEvent(requestInfo,
            readerUid,
            _state.MessageItem.MessageId,
            _state.MessageItem.SenderPeer.PeerId,
            _state.SenderMessageId,
            _state.MessageItem.ToPeer,
            _state.MessageItem.SenderPeer.PeerId == readerUid
        ));
    }

    public void ReplyToMessage(RequestInfo requestInfo, Peer replierPeer, int repliesPts, int messageId)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        var reply = _state.Reply ?? new MessageReply(null, 0, repliesPts, messageId, new List<Peer>());
        reply.Replies++;
        var recentRepliers = reply.RecentRepliers ?? new List<Peer>();
        var peer = recentRepliers.FirstOrDefault(p => p.PeerId == replierPeer.PeerId);
        if (peer != null)
        {
            recentRepliers.Remove(peer);
        }

        if (recentRepliers.Count > MyTelegramServerDomainConsts.MaxRecentRepliersCount)
        {
            recentRepliers.RemoveAt(MyTelegramServerDomainConsts.MaxRecentRepliersCount - 1);
        }

        recentRepliers.Insert(0, replierPeer);

        long? postChannelId = null;
        int? postMessageId = null;
        if (_state.MessageItem.FwdHeader?.ForwardFromLinkedChannel ?? false)
        {
            postChannelId = _state.MessageItem.PostChannelId;
            postMessageId = _state.MessageItem.PostMessageId;
        }

        Emit(new ReplyChannelMessageCompletedEvent(requestInfo, _state.MessageItem.ToPeer.PeerId,
            _state.MessageItem.MessageId, reply, postChannelId, postMessageId));
    }

    //public void StartForwardMessage(RequestInfo requestInfo,
    //    Peer fromPeer,
    //    Peer toPeer,
    //    IReadOnlyList<int> idList,
    //    IReadOnlyList<long> randomIdList,
    //    bool forwardFromLinkedChannel)
    //{
    //    Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
    //    Emit(new ForwardMessageStartedEvent(requestInfo,
    //        fromPeer,
    //        toPeer,
    //        idList,
    //        randomIdList,
    //        forwardFromLinkedChannel));
    //}


    //public void StartSendMessage(RequestInfo requestInfo,
    //    MessageItem outMessageItem,
    //    List<long>? mentionedUserIds,
    //    bool clearDraft,
    //    int groupItemCount,
    //    bool forwardFromLinkedChannel)
    //{
    //    Specs.AggregateIsNew.ThrowDomainErrorIfNotSatisfied(this);
    //    Emit(new SendMessageStartedEvent(requestInfo,
    //        outMessageItem,
    //        mentionedUserIds,
    //        clearDraft,
    //        groupItemCount,
    //        forwardFromLinkedChannel));
    //}

    public void StartUpdatePinnedMessage(RequestInfo requestInfo,
        bool pinned,
        bool pmOneSide,
        bool silent,
        int date,
        long randomId,
        string messageActionData)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        var oldPmOneSide = pmOneSide;
        if (!pinned)
        {
            oldPmOneSide = _state.PmOneSide;
        }

        var item = _state.MessageItem;
        Emit(new UpdatePinnedMessageStartedEvent(requestInfo,
            item.OwnerPeer.PeerId,
            item.MessageId,
            pinned,
            oldPmOneSide,
            silent,
            date,
            item.IsOut,
            _state.InboxItems,
            item.SenderPeer.PeerId,
            _state.SenderMessageId,
            item.ToPeer,
            randomId,
            messageActionData
        ));
    }

    public void UpdateInboxMessagePinned(
        RequestInfo requestInfo,
        bool pinned,
        bool pmOneSide,
        bool silent,
        int date)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        var item = _state.MessageItem;
        Emit(new InboxMessagePinnedUpdatedEvent(
            requestInfo,
            item.OwnerPeer.PeerId,
            item.MessageId,
            pinned,
            pmOneSide,
            silent,
            date,
            item.ToPeer,
            _state.Pts));
    }

    public void UpdateMessageRely(int pts)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        Emit(new MessageReplyUpdatedEvent(_state.MessageItem.OwnerPeer.PeerId,
            MyTelegramServerDomainConsts.DeletedChannelIdForChannelPost, _state.MessageItem.MessageId, pts));
    }
    public void UpdateOutboxMessagePinned(
        RequestInfo requestInfo,
        bool pinned,
        bool pmOneSide,
        bool silent,
        int date)
    {
        Specs.AggregateIsCreated.ThrowDomainErrorIfNotSatisfied(this);
        var item = _state.MessageItem;
        Emit(new OutboxMessagePinnedUpdatedEvent(
            requestInfo,
            item.OwnerPeer.PeerId,
            item.MessageId,
            pinned,
            pmOneSide,
            silent,
            date,
            _state.InboxItems,
            item.SenderPeer.PeerId,
            _state.SenderMessageId,
            item.ToPeer,
            _state.Pts
        ));
    }

    protected override Task<MessageSnapshot> CreateSnapshotAsync(CancellationToken cancellationToken)
    {
        return Task.FromResult(new MessageSnapshot(_state.MessageItem,
            _state.InboxItems,
            _state.SenderMessageId,
            _state.Pinned,
            _state.EditDate,
            _state.Edited,
            _state.Pts,
            _state.Reply
        ));
    }

    protected override Task LoadSnapshotAsync(MessageSnapshot snapshot,
        ISnapshotMetadata metadata,
        CancellationToken cancellationToken)
    {
        _state.LoadSnapshot(snapshot);
        return Task.CompletedTask;
    }
}