//using System;
//using System.Threading;
//using System.Threading.Tasks;
//using EventFlow.Aggregates;
//using EventFlow.Sagas;
//using EventFlow.Sagas.AggregateSagas;
//using EventFlow.ValueObjects;
//using MyTelegramServer.Domain.Aggregates.Dialog;
//using MyTelegramServer.Domain.Aggregates.Pts;
//using MyTelegramServer.Domain.Commands.Dialog;
//using MyTelegramServer.Domain.Commands.Pts;
//using MyTelegramServer.Domain.Events;
//using MyTelegramServer.Domain.Events.Dialog;
//using MyTelegramServer.Domain.ValueObjects;

//namespace MyTelegramServer.Domain.Sagas
//{
//    public class ReadHistorySagaId : SingleValueObject<string>, ISagaId
//    {
//        public ReadHistorySagaId(string value) : base(value)
//        {
//        }
//    }

//    public class ReadHistorySagaLocator : ISagaLocator
//    {
//        public Task<ISagaId> LocateSagaAsync(IDomainEvent domainEvent,
//            CancellationToken cancellationToken)
//        {
//            var id = domainEvent.GetAggregateEvent() as IHasCorrelationId;

//            return Task.FromResult<ISagaId>(new ReadHistorySagaId(id.CorrelationId.ToString("n")));
//        }
//    }

//    public class ReadHistoryState : AggregateState<ReadHistorySaga, ReadHistorySagaId, ReadHistoryState>,
//        IApply<ReadHistoryStartedEvent>,
//        IApply<ReadOutboxHistoryEvent>,
//        //IApply<ReadHistoryCompletedEvent>,
//        IHasRequestMessageId
//    {
//        public long SenderPeerId { get; private set; }
//        public int SenderMessageId { get; private set; }
//        public int ReaderPts { get; private set; }
//        public int SenderPts { get; private set; }
//        public bool SenderIsBot { get; private set; }
//        public long ReaderUid { get; private set; }
//        public int ReaderMessageId { get; private set; }
//        public bool Out { get; private set; }
//        public PeerType ToPeerType { get; private set; }
//        public long ToPeerId { get; private set; }

//        private bool _outboxHasRead;
//        internal bool ReadHistoryCompleted => Out || _outboxHasRead;

//        public void Apply(ReadHistoryStartedEvent aggregateEvent)
//        {
//            SenderPeerId = aggregateEvent.SenderPeerId;
//            SenderMessageId = aggregateEvent.SenderMessageId;
//            ReaderUid = aggregateEvent.ReaderUid;
//            ReaderMessageId = aggregateEvent.ReaderMessageId;
//            ReaderPts = aggregateEvent.ReaderPts;
//            SenderPts = aggregateEvent.SenderPts;
//            SenderIsBot = aggregateEvent.SenderIsBot;

//            Out = aggregateEvent.Out;
//            ReqMsgId = aggregateEvent.ReqMsgId;
//            ToPeerType = aggregateEvent.ToPeerType;
//            ToPeerId = aggregateEvent.ToPeerId;

//        }

//        public void Apply(ReadOutboxHistoryEvent aggregateEvent)
//        {
//            _outboxHasRead = true;
//        }

//        public long ReqMsgId { get; private set; }
//        public void Apply(ReadHistoryCompletedEvent aggregateEvent)
//        {

//        }
//    }

//    public class ReadHistorySaga : AggregateSaga<ReadHistorySaga, ReadHistorySagaId, ReadHistorySagaLocator>,
//        ISagaIsStartedBy<DialogAggregate, DialogId, ReadInboxMessageEvent>,
//        ISagaHandles<DialogAggregate, DialogId, OutboxMessageHasReadEvent>,
//        IApply<ReadHistoryCompletedEvent>
//    {
//        private readonly ReadHistoryState _state = new();
//        public ReadHistorySaga(ReadHistorySagaId id) : base(id)
//        {
//            Register(_state);
//        }

//        public Task HandleAsync(IDomainEvent<DialogAggregate, DialogId, ReadInboxMessageEvent> domainEvent,
//            ISagaContext sagaContext,
//            CancellationToken cancellationToken)
//        {
//            //TestConsoleLogger.WriteLine($"start read history saga...{domainEvent.AggregateEvent.ReqMsgId}");
//            Emit(new ReadHistoryStartedEvent(domainEvent.AggregateEvent.SenderPeerId,
//                domainEvent.AggregateEvent.SenderMessageId,
//                domainEvent.AggregateEvent.OwnerPeerId,
//                domainEvent.AggregateEvent.MaxMessageId,
//                domainEvent.AggregateEvent.Out,
//                domainEvent.AggregateEvent.ReqMsgId,
//                domainEvent.AggregateEvent.ToPeerType,
//                domainEvent.AggregateEvent.ToPeerId,
//                domainEvent.AggregateEvent.ReaderPts,
//                domainEvent.AggregateEvent.SenderPts,
//                domainEvent.AggregateEvent.SenderIsBot
//                ));
//            IncrementPts(domainEvent.AggregateEvent.OwnerPeerId, PtsChangeReason.ReadInboxMessage, domainEvent.AggregateEvent.CorrelationId);
//            if (!domainEvent.AggregateEvent.Out && !domainEvent.AggregateEvent.SenderIsBot)
//            {
//                var dialogId = DialogId.Create(domainEvent.AggregateEvent.SenderPeerId,
//                    new Peer(PeerType.User, domainEvent.AggregateEvent.OwnerPeerId));
//                var outboxMessageHasReadCommand =
//                    new OutboxMessageHasReadCommand(dialogId, domainEvent.AggregateEvent.SenderMessageId,
//                        domainEvent.AggregateEvent.SenderPeerId, domainEvent.AggregateEvent.CorrelationId);
//                Publish(outboxMessageHasReadCommand);
//            }

//            //increment pts
//            HandleReadHistoryCompleted(domainEvent.AggregateEvent.SenderIsBot);
//            return Task.CompletedTask;
//        }

//        public Task HandleAsync(IDomainEvent<DialogAggregate, DialogId, OutboxMessageHasReadEvent> domainEvent,
//            ISagaContext sagaContext,
//            CancellationToken cancellationToken)
//        {
//            //Emit(new ReadHistoryCompletedEvent());
//            Emit(new ReadOutboxHistoryEvent(domainEvent.AggregateEvent.OwnerPeerId, domainEvent.AggregateEvent.MaxMessageId));
//            IncrementPts(domainEvent.AggregateEvent.OwnerPeerId, PtsChangeReason.OutboxMessageHasRead, domainEvent.AggregateEvent.CorrelationId);
//            HandleReadHistoryCompleted();
//            return Task.CompletedTask;
//        }

//        private void IncrementPts(long peerId, PtsChangeReason reason, Guid correlationId)
//        {
//            var incrementPtsCommand = new IncrementPtsCommand(PtsId.Create(peerId), reason, correlationId);
//            Publish(incrementPtsCommand);
//        }

//        private void HandleReadHistoryCompleted(bool forceCompletedByReadBotResponse = false)
//        {
//            if (_state.ReadHistoryCompleted || forceCompletedByReadBotResponse)
//            {
//                //Complete();
//                Emit(new ReadHistoryCompletedEvent(_state.ReaderUid, _state.ReqMsgId, _state.SenderPeerId,
//                    _state.Out, _state.SenderMessageId, _state.ToPeerType,
//                    _state.ToPeerId, _state.ReaderPts, _state.SenderPts, _state.SenderIsBot));
//            }
//        }

//        public void Apply(ReadHistoryCompletedEvent aggregateEvent)
//        {
//            Complete();
//        }
//    }

//    public class ReadHistoryStartedEvent : AggregateEvent<ReadHistorySaga, ReadHistorySagaId>
//    {
//        public ReadHistoryStartedEvent(long senderUid,
//            int senderMessageId,
//            long readerUid,
//            int readerMessageId,
//            bool @out,
//            long reqMsgId,
//            PeerType toPeerType,
//            long toPeerId,
//            int readerPts,
//            int senderPts,
//            bool senderIsBot

//            )
//        {
//            SenderPeerId = senderUid;
//            SenderMessageId = senderMessageId;
//            ReaderUid = readerUid;
//            ReaderMessageId = readerMessageId;
//            Out = @out;
//            ReqMsgId = reqMsgId;
//            ToPeerType = toPeerType;
//            ToPeerId = toPeerId;
//            ReaderPts = readerPts;
//            SenderPts = senderPts;
//            SenderIsBot = senderIsBot;
//        }
//        //public ReadHistoryStartedEvent(ReadInboxMessageEvent eventData)
//        //{
//        //    EventData = eventData;
//        //}

//        //public ReadInboxMessageEvent EventData { get; }

//        public long SenderPeerId { get; }
//        public int SenderMessageId { get; }
//        public long ReaderUid { get; }
//        public int ReaderMessageId { get; }
//        public bool Out { get; }
//        public long ReqMsgId { get; }
//        public PeerType ToPeerType { get; }
//        public long ToPeerId { get; }
//        public int ReaderPts { get; private set; }
//        public int SenderPts { get; private set; }
//        public bool SenderIsBot { get; }
//    }

//    public class ReadOutboxHistoryEvent : AggregateEvent<ReadHistorySaga, ReadHistorySagaId>
//    {
//        public ReadOutboxHistoryEvent(long senderUid,
//            int senderMessageId)
//        {
//            SenderPeerId = senderUid;
//            SenderMessageId = senderMessageId;
//        }

//        public long SenderPeerId { get; }
//        public int SenderMessageId { get; }
//    }

//    public class ReadHistoryCompletedEvent : AggregateEvent<ReadHistorySaga, ReadHistorySagaId>, IHasRequestMessageId
//    {
//        public ReadHistoryCompletedEvent(long readerUid,
//            long reqMsgId,
//            long senderUid,
//            bool @out,
//            int senderMessageId,
//            PeerType toPeerType,
//            long toPeerId,
//            int readerPts,
//            int senderPts,
//            bool senderIsBot

//            )
//        {
//            ReaderUid = readerUid;
//            ReqMsgId = reqMsgId;
//            SenderPeerId = senderUid;
//            Out = @out;
//            SenderMessageId = senderMessageId;
//            ToPeerType = toPeerType;
//            ToPeerId = toPeerId;
//            ReaderPts = readerPts;
//            SenderPts = senderPts;
//            SenderIsBot = senderIsBot;
//        }

//        public long ReaderUid { get; }
//        public long ReqMsgId { get; }

//        public long SenderPeerId { get; }
//        public int SenderMessageId { get; }
//        public PeerType ToPeerType { get; }
//        public long ToPeerId { get; }
//        public int ReaderPts { get; }
//        public int SenderPts { get; }
//        public bool SenderIsBot { get; }
//        public int CurrentReaderPts { get; }
//        public bool Out { get; }
//    }
//}


