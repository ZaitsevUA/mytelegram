using MyTelegram.Domain.Aggregates.Temp;
using MyTelegram.Domain.Commands.Messaging;
using MyTelegram.Schema.Extensions;

namespace MyTelegram.Domain.Sagas;

public class ReplyBroadcastChannelCompletedSagaEvent : AggregateEvent<SendMessageSaga, SendMessageSagaId>
{
    public long ChannelId { get; }
    public int MessageId { get; }
    public MessageReply Reply { get; }

    public ReplyBroadcastChannelCompletedSagaEvent(long channelId, int messageId, MessageReply reply)
    {
        ChannelId = channelId;
        MessageId = messageId;
        Reply = reply;
    }
}

public class PostChannelIdUpdatedEvent : AggregateEvent<SendMessageSaga, SendMessageSagaId>
{
    public long ChannelId { get; }
    public int MessageId { get; }
    public long PostChannelId { get; }
    public int PostMessageId { get; }

    public PostChannelIdUpdatedEvent(long channelId, int messageId, long postChannelId, int postMessageId)
    {
        ChannelId = channelId;
        MessageId = messageId;
        PostChannelId = postChannelId;
        PostMessageId = postMessageId;
    }
}

public class SendMessageSaga : MyInMemoryAggregateSaga<SendMessageSaga, SendMessageSagaId, SendMessageSagaLocator>,
    ISagaIsStartedBy<MessageAggregate, MessageId, OutboxMessageCreatedEvent>,
    ISagaHandles<MessageAggregate, MessageId, InboxMessageCreatedEvent>,
    ISagaHandles<MessageAggregate, MessageId, ReplyChannelMessageCompletedEvent>
{
    private readonly IIdGenerator _idGenerator;
    private readonly SendMessageSagaState _state = new();
    public SendMessageSaga(SendMessageSagaId id, IEventStore eventStore, IIdGenerator idGenerator) : base(id, eventStore)
    {
        _idGenerator = idGenerator;
        Register(_state);
    }

    public async Task HandleAsync(IDomainEvent<MessageAggregate, MessageId, OutboxMessageCreatedEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
    {
        Emit(new SendMessageSagaStartedEvent(domainEvent.AggregateEvent.RequestInfo,
            domainEvent.AggregateEvent.OutboxMessageItem,
            domainEvent.AggregateEvent.MentionedUserIds,
            domainEvent.AggregateEvent.ReplyToMsgItems,
            domainEvent.AggregateEvent.ClearDraft,
            domainEvent.AggregateEvent.GroupItemCount,
            domainEvent.AggregateEvent.LinkedChannelId,
            domainEvent.AggregateEvent.ChatMembers
            ));

        await HandleSendOutboxMessageCompletedAsync();

        await CreateInboxMessageAsync(domainEvent.AggregateEvent);

        CreateMentions(domainEvent.AggregateEvent.MentionedUserIds, domainEvent.AggregateEvent.OutboxMessageItem.MessageId);
    }

    private void CreateMentions(List<long>? mentionedUserIds, int messageId)
    {
        //if (mentionedUserIds?.Count > 0)
        //{
        //    foreach (var mentionedUserId in mentionedUserIds)
        //    {
        //        var command = new CreateMentionCommand(DialogId.Create(mentionedUserId, _state.MessageItem.ToPeer),
        //            mentionedUserId, /*_state.MessageItem.ToPeer.PeerId,*/ messageId);
        //        Publish(command);
        //    }
        //}
    }

    public Task HandleAsync(IDomainEvent<MessageAggregate, MessageId, InboxMessageCreatedEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
    {
        var item = domainEvent.AggregateEvent.InboxMessageItem;

        //var command = new AddInboxMessageIdToOutboxMessageCommand(
        //    MessageId.Create(item.SenderPeer.PeerId,
        //        domainEvent.AggregateEvent.SenderMessageId),
        //    _state.RequestInfo,
        //    item.OwnerPeer.PeerId,
        //    item.MessageId);
        //Publish(command);

        var command = new ReceiveInboxMessageCommand(
            DialogId.Create(domainEvent.AggregateEvent.InboxMessageItem.OwnerPeer.PeerId,
                domainEvent.AggregateEvent.InboxMessageItem.ToPeer),
            domainEvent.AggregateEvent.RequestInfo,
            domainEvent.AggregateEvent.InboxMessageItem.MessageId,
            domainEvent.AggregateEvent.InboxMessageItem.OwnerPeer.PeerId,
            domainEvent.AggregateEvent.InboxMessageItem.ToPeer);
        Publish(command);

        return HandleReceiveInboxMessageCompletedAsync(item);
    }

    private async Task CreateInboxMessageAsync(OutboxMessageCreatedEvent outbox)
    {
        switch (outbox.OutboxMessageItem.ToPeer.PeerType)
        {
            case PeerType.User:
                await CreateInboxMessageForUserAsync(outbox.OutboxMessageItem.ToPeer.PeerId);
                break;
            case PeerType.Chat:
                if (outbox.ChatMembers?.Count > 0)
                {
                    foreach (var chatMemberUserId in outbox.ChatMembers)
                    {
                        if (chatMemberUserId == outbox.RequestInfo.UserId)
                        {
                            continue;
                        }

#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                        await CreateInboxMessageForUserAsync(chatMemberUserId);
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                    }
                }

                break;
        }
    }

    private async Task HandleReceiveInboxMessageCompletedAsync(MessageItem inboxMessageItem)
    {
        var pts = await _idGenerator.NextIdAsync(IdType.Pts, inboxMessageItem.OwnerPeer.PeerId);
        Emit(new ReceiveInboxMessageCompletedEvent(inboxMessageItem, pts, string.Empty));

        if (_state.IsCreateInboxMessagesCompleted())
        {
            var item = _state.MessageItem;
            var command = new AddInboxItemsToOutboxMessageCommand(
                MessageId.Create(item.SenderPeer.PeerId,
                    item.MessageId),
                _state.RequestInfo,
                _state.InboxItems
                );
            Publish(command);

            await CompleteAsync();
        }
    }

    private async Task HandleSendOutboxMessageCompletedAsync()
    {
        var pts = await _idGenerator.NextIdAsync(IdType.Pts, _state.MessageItem.OwnerPeer.PeerId);
        var linkedChannelId = _state.LinkedChannelId;
        //var globalSeqNo = _state.MessageItem.ToPeer.PeerType == PeerType.Channel ? await _idGenerator.NextLongIdAsync(IdType.GlobalSeqNo) : 0;

        Emit(new SendOutboxMessageCompletedEvent(_state.RequestInfo,
            _state.MessageItem,
            _state.MentionedUserIds,
            pts,
            _state.GroupItemCount,
            linkedChannelId,
            null//,
                //globalSeqNo
            /*_state.BotUserIds*/));

        var command = new UpdateDialogCommand(
            DialogId.Create(_state.MessageItem.SenderUserId, _state.MessageItem.ToPeer),
            _state.MessageItem.SenderUserId,
            _state.MessageItem.ToPeer,
            _state.MessageItem.MessageId,
            pts
        );
        Publish(command);

        if (_state.MessageItem.ToPeer.PeerType == PeerType.Channel)
        {
            SetChannelPts(_state.MessageItem.ToPeer.PeerId, pts, _state.MessageItem.MessageId);

            if (_state.LinkedChannelId.HasValue && _state.MessageItem.SendMessageType != SendMessageType.MessageService)
            {
                ForwardBroadcastMessageToLinkedChannel(_state.LinkedChannelId.Value, _state.MessageItem.MessageId);
            }

            // handle reply discussion message
            if (!HandleReplyDiscussionMessage(pts))
            {
                await CompleteAsync();
            }
        }
    }
    private bool HandleReplyDiscussionMessage(int pts)
    {
        if (_state.MessageItem is { InputReplyTo: not null, ToPeer.PeerType: PeerType.Channel })
        {
            switch (_state.MessageItem.InputReplyTo)
            {
                case TInputReplyToMessage inputReplyToMessage:
                    ReplyToMessage(_state.MessageItem.ToPeer.PeerId, inputReplyToMessage.ReplyToMsgId, pts, _state.MessageItem.MessageId);
                    return true;
                case TInputReplyToStory inputReplyToStory:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        return false;
    }
    private void ForwardBroadcastMessageToLinkedChannel(long linkedChannelId, int messageId)
    {
        var fromPeer = _state.MessageItem!.ToPeer;
        var toPeer = new Peer(PeerType.Channel, linkedChannelId);
        var command = new StartForwardMessagesCommand(TempId.New,
            _state.RequestInfo,
            false,
            false,
            false,
            false,
            false,
            false,
            fromPeer,
            toPeer,
            [messageId],
            [Random.Shared.NextInt64()],
            null,
            _state.MessageItem.SendAs,
            true
        );
        Publish(command);
    }


    private void ReplyToMessage(long ownerPeerId, int messageId, int repliesPts, int maxMessageId)
    {
        var replierPeer = _state.MessageItem.SendAs ?? _state.RequestInfo.UserId.ToUserPeer();
        var command = new ReplyToMessageCommand(MessageId.Create(ownerPeerId, messageId), _state.RequestInfo, replierPeer, repliesPts, maxMessageId);
        Publish(command);
    }

    private void SetChannelPts(long channelId, int pts, int messageId)
    {
        var command = new SetChannelPtsCommand(ChannelId.Create(channelId), _state.MessageItem!.SenderPeer.PeerId, pts, messageId, _state.MessageItem.Date);
        Publish(command);
    }
    private async Task CreateInboxMessageForUserAsync(long inboxOwnerUserId)
    {
        var outMessageItem = _state.MessageItem!;
        var toPeer = outMessageItem.ToPeer.PeerType == PeerType.Chat ? outMessageItem.ToPeer : outMessageItem.OwnerPeer;

        var replyTo = outMessageItem.InputReplyTo.ToBytes().ToTObject<IInputReplyTo>();

        if (_state.ReplyToMsgItems.TryGetValue(inboxOwnerUserId, out var replyToMsgId))
        {
            switch (replyTo)
            {
                case TInputReplyToMessage inputReplyToMessage:
                    inputReplyToMessage.ReplyToMsgId = replyToMsgId;
                    break;
                case TInputReplyToStory inputReplyToStory:
                    inputReplyToStory.StoryId = replyToMsgId;
                    break;
            }
        }

        // Channel only create outbox message,
        // Use IdType.MessageId and IdType.ChannelMessageId will not be used
        var inboxMessageId = await _idGenerator.NextIdAsync(IdType.MessageId, inboxOwnerUserId);
        var aggregateId = MessageId.Create(inboxOwnerUserId, inboxMessageId);
        var inboxMessageItem = outMessageItem with
        {
            OwnerPeer = new Peer(PeerType.User, inboxOwnerUserId),
            ToPeer = toPeer,
            MessageId = inboxMessageId,
            IsOut = false,
            InputReplyTo = replyTo,
        };

        var command = new CreateInboxMessageCommand(aggregateId, _state.RequestInfo, inboxMessageItem, outMessageItem.MessageId);
        Publish(command);
    }

    public Task HandleAsync(IDomainEvent<MessageAggregate, MessageId, ReplyChannelMessageCompletedEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
    {
        if (domainEvent.AggregateEvent.PostChannelId.HasValue && domainEvent.AggregateEvent.PostMessageId.HasValue)
        {
            Emit(new ReplyBroadcastChannelCompletedSagaEvent(domainEvent.AggregateEvent.PostChannelId.Value, domainEvent.AggregateEvent.PostMessageId.Value, domainEvent.AggregateEvent.Reply));
            Emit(new PostChannelIdUpdatedEvent(_state.MessageItem.OwnerPeer.PeerId, _state.MessageItem.MessageId, domainEvent.AggregateEvent.PostChannelId.Value, domainEvent.AggregateEvent.PostMessageId.Value));
        }

        return CompleteAsync(cancellationToken);
    }
}