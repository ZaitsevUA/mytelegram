namespace MyTelegram.Domain.Sagas;

public class ReadHistoryPtsIncrementedSagaEvent
    (RequestInfo requestInfo, long userId, int pts) : AggregateEvent<ReadHistorySaga, ReadHistorySagaId>,
        IHasRequestInfo
{
    public RequestInfo RequestInfo { get; } = requestInfo;
    public long UserId { get; } = userId;
    public int Pts { get; } = pts;
}

public class ReadHistoryStartedSagaEvent(RequestInfo requestInfo) : AggregateEvent<ReadHistorySaga, ReadHistorySagaId>
{
    public RequestInfo RequestInfo { get; } = requestInfo;
}

public class UpdateInboxMaxIdCompletedSagaEvent(RequestInfo requestInfo, int maxId, int pts) : RequestAggregateEvent2<ReadHistorySaga, ReadHistorySagaId>(requestInfo)
{
    public int MaxId { get; } = maxId;
    public int Pts { get; } = pts;
}

public class UpdateOutboxMaxIdCompletedSagaEvent(long userId, long toPeerId, int maxId, int pts, int ptsCount)
    : AggregateEvent<ReadHistorySaga, ReadHistorySagaId>
{
    public long UserId { get; } = userId;
    public long ToPeerId { get; } = toPeerId;
    public int MaxId { get; } = maxId;
    public int Pts { get; } = pts;
    public int PtsCount { get; } = ptsCount;
}


public class ReadHistorySaga : MyInMemoryAggregateSaga<ReadHistorySaga, ReadHistorySagaId,
        ReadHistorySagaLocator>,
        ISagaIsStartedBy<DialogAggregate, DialogId, ReadInboxMaxIdUpdatedEvent>,
        ISagaHandles<DialogAggregate, DialogId, ReadOutboxMaxIdUpdatedEvent>
//ISagaIsStartedBy<DialogAggregate, DialogId, ReadInboxMessage2Event>,
//ISagaHandles<MessageAggregate, MessageId, InboxMessageHasReadEvent>,
//ISagaHandles<DialogAggregate, DialogId, OutboxMessageHasReadEvent>,
//ISagaHandles<DialogAggregate, DialogId, OutboxAlreadyReadEvent>,
//ISagaHandles<ChatAggregate, ChatId, ReadLatestNoneBotOutboxMessageEvent>,
//IApply<ReadHistoryCompletedEvent>
{
    private readonly IIdGenerator _idGenerator;
    private readonly ReadHistoryState _state = new();

    public ReadHistorySaga(ReadHistorySagaId id, IEventStore eventStore, IIdGenerator idGenerator) : base(id, eventStore)
    {
        _idGenerator = idGenerator;
        Register(_state);
    }

    public async Task HandleAsync(IDomainEvent<DialogAggregate, DialogId, ReadInboxMaxIdUpdatedEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
    {
        Emit(new ReadHistoryStartedSagaEvent(domainEvent.AggregateEvent.RequestInfo));
        var pts = await IncrementPtsAsync(domainEvent.AggregateEvent.RequestInfo.UserId);
        Emit(new UpdateInboxMaxIdCompletedSagaEvent(_state.RequestInfo, domainEvent.AggregateEvent.ReadInboxMaxId, pts));
        CreateReadingHistory(domainEvent.AggregateEvent.SenderUserId, domainEvent.AggregateEvent.SenderMessageId);
        UpdateReadOutboxMaxId(domainEvent.AggregateEvent.SenderUserId, domainEvent.AggregateEvent.SenderMessageId);
    }

    public async Task HandleAsync(IDomainEvent<DialogAggregate, DialogId, ReadOutboxMaxIdUpdatedEvent> domainEvent, ISagaContext sagaContext, CancellationToken cancellationToken)
    {
        var pts = await IncrementPtsAsync(domainEvent.AggregateEvent.OwnerUserId);
        Emit(new UpdateOutboxMaxIdCompletedSagaEvent(domainEvent.AggregateEvent.OwnerUserId,
            domainEvent.AggregateEvent.RequestInfo.UserId, domainEvent.AggregateEvent.ReadOutboxMaxId, pts, 1));

        await CompleteAsync(cancellationToken);
    }

    private void UpdateReadOutboxMaxId(long senderUserId, int senderMessageId)
    {
        var command = new UpdateReadOutboxMaxIdCommand(
            DialogId.Create(senderUserId, PeerType.User, _state.RequestInfo.UserId), _state.RequestInfo,
            senderMessageId);
        Publish(command);
    }

    private async Task<int> IncrementPtsAsync(long userId)
    {
        var pts = await _idGenerator.NextIdAsync(IdType.Pts, userId);
        Emit(new ReadHistoryPtsIncrementedSagaEvent(_state.RequestInfo, userId, pts));

        return pts;
    }

    private void CreateReadingHistory(long senderUserId, int senderMessageId)
    {
        var command = new CreateReadingHistoryCommand(
            ReadingHistoryId.Create(_state.RequestInfo.UserId, senderUserId, senderMessageId), _state.RequestInfo.UserId,
            senderUserId, senderMessageId, DateTime.UtcNow.ToTimestamp());
        Publish(command);

        //var toPeerId = _state.ReaderToPeer.PeerId;
        ////if (_state.ReaderToPeer.PeerType == PeerType.Channel || _state.ReaderToPeer.PeerType == PeerType.Chat)
        //{
        //    var command = new CreateReadingHistoryCommand(ReadingHistoryId.Create(_state.ReaderUserId,
        //            //toPeerId,
        //            toPeerId,
        //            senderMsgId),
        //        _state.ReaderUserId,
        //        toPeerId,
        //        senderMsgId,
        //        DateTime.UtcNow.ToTimestamp());

        //    Publish(command);
        //}
    }

    //public int SenderPts => _state.SenderPts;

    //public long SenderPeerId => _state.SenderPeerId;

    //public void Apply(ReadHistoryCompletedEvent aggregateEvent)
    //{
    //    CompleteAsync();
    //}

    //public Task HandleAsync(IDomainEvent<ChatAggregate, ChatId, ReadLatestNoneBotOutboxMessageEvent> domainEvent,
    //    ISagaContext sagaContext,
    //    CancellationToken cancellationToken)
    //{
    //    Emit(new ReadHistoryReadLatestNoneBotOutboxMessageEvent(domainEvent.AggregateEvent.SenderPeerId));
    //    if (domainEvent.AggregateEvent.SenderPeerId != _state.ReaderUserId)
    //    {
    //        SendReadOutboxMessageCommand(domainEvent.AggregateEvent.SenderPeerId,
    //            new Peer(PeerType.Chat, domainEvent.AggregateEvent.ChatId),
    //            domainEvent.AggregateEvent.SenderMessageId);
    //    }
    //    else
    //    {
    //        HandleReadHistoryCompleted();
    //    }

    //    return Task.CompletedTask;
    //}

    //public Task HandleAsync(IDomainEvent<DialogAggregate, DialogId, OutboxAlreadyReadEvent> domainEvent,
    //    ISagaContext sagaContext,
    //    CancellationToken cancellationToken)
    //{
    //    CreateReadingHistory(domainEvent.AggregateEvent.NewMaxMessageId);

    //    if (!_state.NeedReadLatestNoneBotOutboxMessage)
    //    {
    //        HandleReadHistoryCompleted(true);
    //    }

    //    return Task.CompletedTask;
    //}

    //public async Task HandleAsync(IDomainEvent<DialogAggregate, DialogId, OutboxMessageHasReadEvent> domainEvent,
    //    ISagaContext sagaContext,
    //    CancellationToken cancellationToken)
    //{
    //    Emit(new ReadHistoryOutboxHasReadEvent(
    //        _state.RequestInfo,
    //        domainEvent.AggregateEvent.OwnerPeerId,
    //        domainEvent.AggregateEvent.MaxMessageId));
    //    await IncrementPtsAsync(
    //        domainEvent.AggregateEvent.OwnerPeerId,
    //        0,
    //        0,
    //        PtsChangeReason.OutboxMessageHasRead);

    //    CreateReadingHistory(_state.SenderMessageId);
    //}

    //public Task HandleAsync(IDomainEvent<MessageAggregate, MessageId, InboxMessageHasReadEvent> domainEvent,
    //    ISagaContext sagaContext,
    //    CancellationToken cancellationToken)
    //{
    //    var senderIsBot = false;
    //    var needReadLatestNoneBotOutboxMessage = domainEvent.AggregateEvent.ToPeer.PeerType == PeerType.Chat &&
    //                                             senderIsBot;
    //    Emit(new ReadHistoryInboxHasReadEvent(domainEvent.AggregateEvent.IsOut,
    //        senderIsBot,
    //        needReadLatestNoneBotOutboxMessage));
    //    if (!domainEvent.AggregateEvent.IsOut /*&& !domainEvent.AggregateEvent.SenderIsBot*/)
    //    {
    //        var toPeerForSender =
    //            GetToPeerForSender(domainEvent.AggregateEvent.ToPeer, domainEvent.AggregateEvent.ReaderUid);

    //        SendReadOutboxMessageCommand(domainEvent.AggregateEvent.SenderPeerId,
    //            toPeerForSender,
    //            domainEvent.AggregateEvent.SenderMessageId);
    //    }

    //    if (needReadLatestNoneBotOutboxMessage)
    //    {
    //        var readLatestNoneBotOutboxMessageCommand =
    //            new ReadLatestNoneBotOutboxMessageCommand(
    //                ChatId.Create(domainEvent.AggregateEvent.ToPeer.PeerId),
    //                domainEvent.AggregateEvent.RequestInfo,
    //                domainEvent.Metadata.SourceId.Value);
    //        Publish(readLatestNoneBotOutboxMessageCommand);
    //    }

    //    HandleReadHistoryCompleted();
    //    return Task.CompletedTask;
    //}

    //public async Task HandleAsync(IDomainEvent<DialogAggregate, DialogId, ReadInboxMessage2Event> domainEvent,
    //    ISagaContext sagaContext,
    //    CancellationToken cancellationToken)
    //{
    //    Emit(new ReadHistoryStartedEvent(domainEvent.AggregateEvent.RequestInfo,
    //        domainEvent.AggregateEvent.OwnerPeerId,
    //        domainEvent.AggregateEvent.MaxMessageId,
    //        domainEvent.AggregateEvent.ToPeer,
    //        domainEvent.Metadata.SourceId.Value));

    //    await IncrementPtsAsync(domainEvent.AggregateEvent.OwnerPeerId,
    //        domainEvent.AggregateEvent.ReadCount,
    //        domainEvent.AggregateEvent.UnreadCount,
    //        PtsChangeReason.ReadInboxMessage);

    //    var command = new ReadInboxHistoryCommand(
    //        MessageId.Create(domainEvent.AggregateEvent.OwnerPeerId, domainEvent.AggregateEvent.MaxMessageId),
    //        domainEvent.AggregateEvent.RequestInfo,
    //        domainEvent.AggregateEvent.ReaderUserId,
    //        DateTime.UtcNow.ToTimestamp()
    //    );

    //    Publish(command);
    //}

    //private void CreateReadingHistory(int senderMsgId)
    //{
    //    var toPeerId = _state.ReaderToPeer.PeerId;
    //    //if (_state.ReaderToPeer.PeerType == PeerType.Channel || _state.ReaderToPeer.PeerType == PeerType.Chat)
    //    {
    //        var command = new CreateReadingHistoryCommand(ReadingHistoryId.Create(_state.ReaderUserId,
    //                //toPeerId,
    //                toPeerId,
    //                senderMsgId),
    //            _state.ReaderUserId,
    //            toPeerId,
    //            senderMsgId,
    //            DateTime.UtcNow.ToTimestamp());

    //        Publish(command);
    //    }
    //}

    //private static Peer GetToPeerForSender(Peer readerToPeer,
    //    long readerPeerId)
    //{
    //    if (readerToPeer.PeerType == PeerType.User)
    //    {
    //        return new Peer(PeerType.User, readerPeerId);
    //    }

    //    return readerToPeer;
    //}

    //private void HandleReadHistoryCompleted(bool outboxAlreadyRead = false)
    //{
    //    if (_state.ReadHistoryCompleted || outboxAlreadyRead)
    //    {
    //        //Complete();
    //        Emit(new ReadHistoryCompletedEvent(_state.RequestInfo,
    //            _state.SenderIsBot,
    //            _state.ReaderUserId,
    //            _state.ReaderMessageId,
    //            _state.ReaderPts,
    //            _state.ReaderToPeer,
    //            _state.SenderPeerId,
    //            _state.SenderPts,
    //            _state.SenderMessageId,
    //            _state.IsOut,
    //            outboxAlreadyRead,
    //            _state.SourceCommandId
    //        ));
    //    }
    //}

    ////private void IncrementPts(long peerId, PtsChangeReason reason, Guid correlationId)
    ////{
    ////    var incrementPtsCommand = new IncrementPtsCommand(PtsId.Create(peerId), reason, correlationId);
    ////    Publish(incrementPtsCommand);
    ////}

    //private async Task IncrementPtsAsync(long peerId,
    //    int readCount,
    //    int unreadCount,
    //    PtsChangeReason reason)
    //{
    //    var pts = await _idGenerator.NextIdAsync(IdType.Pts, peerId);

    //    var requestInfo = _state.RequestInfo;
    //    if (reason == PtsChangeReason.OutboxMessageHasRead)
    //    {
    //        requestInfo = _state.RequestInfo with { PermAuthKeyId = 0 };
    //    }

    //    Emit(new ReadHistoryPtsIncrementEvent(
    //        requestInfo,
    //        peerId,
    //        pts,
    //        readCount,
    //        unreadCount,
    //        reason));
    //    HandleReadHistoryCompleted();
    //}

    ////protected override Task LoadSnapshotAsync(ReadHistorySagaSnapshot snapshot,
    ////    ISnapshotMetadata metadata,
    ////    CancellationToken cancellationToken)
    ////{
    ////    _state.LoadSnapshot(snapshot);
    ////    return Task.CompletedTask;
    ////}

    //private void SendReadOutboxMessageCommand(
    //    long senderPeerId,
    //    Peer toPeer,
    //    int senderMessageId)
    //{
    //    var senderDialogId = DialogId.Create(senderPeerId, toPeer);
    //    var outboxMessageHasReadCommand = new OutboxMessageHasReadCommand(senderDialogId,
    //        _state.RequestInfo,
    //        senderMessageId,
    //        senderPeerId,
    //        toPeer);
    //    Publish(outboxMessageHasReadCommand);
    //}

}