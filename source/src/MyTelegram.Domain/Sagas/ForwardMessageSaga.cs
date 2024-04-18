using MyTelegram.Domain.Aggregates.Temp;

namespace MyTelegram.Domain.Sagas;
public class MessageReplyCreatedEvent(long postChannelId, int postMessageId, long channelId, int messageId)
    : AggregateEvent<ForwardMessageSaga, ForwardMessageSagaId>
{
    public long PostChannelId { get; } = postChannelId;
    public int PostMessageId { get; } = postMessageId;
    public long ChannelId { get; } = channelId;
    public int MessageId { get; } = messageId;
}

public class ForwardMessageSaga : MyInMemoryAggregateSaga<ForwardMessageSaga, ForwardMessageSagaId, ForwardMessageSagaLocator>,
        ISagaIsStartedBy<TempAggregate, TempId, ForwardMessagesStartedEvent>,
        ISagaHandles<MessageAggregate, MessageId, MessageForwardedEvent>
{
    private readonly IIdGenerator _idGenerator;
    private readonly ForwardMessageState _state = new();

    public ForwardMessageSaga(ForwardMessageSagaId id, IEventStore eventStore, IIdGenerator idGenerator) : base(id, eventStore)
    {
        _idGenerator = idGenerator;
        Register(_state);
    }

    public async Task HandleAsync(IDomainEvent<MessageAggregate, MessageId, MessageForwardedEvent> domainEvent,
        ISagaContext sagaContext,
        CancellationToken cancellationToken)
    {
        var outMessageId = await SendMessageToTargetPeerAsync(domainEvent.AggregateEvent);
        Emit(new ForwardSingleMessageSuccessEvent());
        if (_state.ForwardFromLinkedChannel)
        {
            Emit(new MessageReplyCreatedEvent(domainEvent.AggregateEvent.OriginalMessageItem.ToPeer.PeerId, domainEvent.AggregateEvent.OriginalMessageItem.MessageId, _state.ToPeer.PeerId, outMessageId));
            PinForwardedChannelMessage(_state.ToPeer.PeerId, outMessageId);
        }
        await HandleForwardCompletedAsync();
    }
    private void PinForwardedChannelMessage(long channelId, int messageId)
    {
        var command = new PinChannelMessageCommand(MessageId.Create(channelId, messageId), _state.RequestInfo);
        Publish(command);
    }

    public Task HandleAsync(IDomainEvent<TempAggregate, TempId, ForwardMessagesStartedEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
    {
        Emit(new ForwardMessageSagaStartedEvent(domainEvent.AggregateEvent.RequestInfo,
            domainEvent.AggregateEvent.FromPeer,
            domainEvent.AggregateEvent.ToPeer,
            domainEvent.AggregateEvent.MessageIds,
            domainEvent.AggregateEvent.RandomIds,
            domainEvent.AggregateEvent.ForwardFromLinkedChannel
        ));
        ForwardMessage(domainEvent.AggregateEvent);
        return Task.CompletedTask;
    }

    private void ForwardMessage(ForwardMessagesStartedEvent aggregateEvent)
    {
        var ownerPeerId = _state.FromPeer.PeerType == PeerType.Channel
            ? _state.FromPeer.PeerId
            : _state.RequestInfo.UserId;
        var index = 0;
        foreach (var messageId in aggregateEvent.MessageIds)
        {
            var randomId = aggregateEvent.RandomIds[index];
            var command = new ForwardMessageCommand(MessageId.Create(ownerPeerId, messageId),
                aggregateEvent.RequestInfo,
                randomId);
            Publish(command);
            index++;
        }
    }

    private Task HandleForwardCompletedAsync()
    {
        if (_state.IsCompleted)
        {
            return CompleteAsync();
        }

        return Task.CompletedTask;
    }

    private async Task<int> SendMessageToTargetPeerAsync(MessageForwardedEvent aggregateEvent)
    {
        var selfUserId = _state.RequestInfo.UserId;
        var ownerPeerId = _state.ToPeer.PeerType == PeerType.Channel
            ? _state.ToPeer.PeerId
            : selfUserId;

        var outMessageId = 0;
        var fromId = _state.FromPeer;
        var channelPost = _state.FromPeer.PeerType == PeerType.Channel ? aggregateEvent.OriginalMessageItem.MessageId : 0;
        var senderPeer = new Peer(PeerType.User, _state.RequestInfo.UserId);

        var savedFromPeer = _state.ToPeer.PeerId == selfUserId ? _state.FromPeer : null;
        int? savedFromMsgId = _state.ToPeer.PeerId == selfUserId ? aggregateEvent.OriginalMessageItem.MessageId : null;
        var isOut = true;
        MessageReply? reply = null;
        long? postChannelId = null;
        int? postMessageId = null;
        if (_state.ForwardFromLinkedChannel)
        {
            savedFromPeer = _state.FromPeer;
            savedFromMsgId = aggregateEvent.OriginalMessageItem.MessageId;
            senderPeer = _state.FromPeer;
            isOut = false;
            reply = aggregateEvent.OriginalMessageItem.Reply;
            postChannelId = aggregateEvent.OriginalMessageItem.ToPeer.PeerId;
            postMessageId = aggregateEvent.OriginalMessageItem.MessageId;
            //senderPeer=new Peer(PeerType.Channel,_state.)
        }
        // TODO:Set fromName
        var fwdHeader = new MessageFwdHeader(
            false,
            false,
            fromId,
            null,
            channelPost,
            //aggregateEvent.PostAuthor,
            null,
            //aggregateEvent.Date,
            DateTime.UtcNow.ToTimestamp(),
            savedFromPeer,
            savedFromMsgId, null, null, null, null, _state.ForwardFromLinkedChannel);

        outMessageId = await _idGenerator.NextIdAsync(IdType.MessageId, ownerPeerId);
        //var aggregateId = MessageId.CreateWithRandomId(ownerPeerId, aggregateEvent.RandomId);
        var aggregateId = MessageId.Create(ownerPeerId, outMessageId);


        var ownerPeer = _state.ToPeer.PeerType == PeerType.Channel
            ? _state.ToPeer
            : senderPeer;
        var toPeer = _state.ToPeer;
        var item = aggregateEvent.OriginalMessageItem;

        var command = new CreateOutboxMessageCommand(aggregateId, aggregateEvent.RequestInfo,
            new MessageItem(
                ownerPeer,
                toPeer,
                senderPeer,
                aggregateEvent.OriginalMessageItem.SenderUserId,
                outMessageId,
                item.Message,
                DateTime.UtcNow.ToTimestamp(),
                aggregateEvent.RandomId,
                isOut,
                SendMessageType.Text,
                MessageType.Text,
                MessageSubType.ForwardMessage,
                //replyToMsgId: item.ReplyToMsgId,
                InputReplyTo: item.InputReplyTo,
                Entities: item.Entities,
                Media: item.Media,
                FwdHeader: fwdHeader,
                Views: item.Views,
                PollId: item.PollId,
                EditHide: _state.ForwardFromLinkedChannel,
                Reply: reply,
                IsForwardFromChannelPost: _state.ForwardFromLinkedChannel,
                PostChannelId: postChannelId,
                PostMessageId: postMessageId
            )
        );

        Publish(command);
        return outMessageId;
    }
}
