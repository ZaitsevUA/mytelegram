using MyTelegram.Domain.Aggregates.PeerNotifySettings;
using MyTelegram.Domain.Events.PeerNotifySettings;
using MyTelegram.Messenger.Services.Caching;
using MyTelegram.Messenger.TLObjectConverters.Interfaces;
using MyTelegram.Services.TLObjectConverters;

namespace MyTelegram.Messenger.QueryServer.DomainEventHandlers;

public class OtherDomainEventHandler(
    IObjectMessageSender objectMessageSender,
    ICommandBus commandBus,
    IIdGenerator idGenerator,
    IAckCacheService ackCacheService,
    IResponseCacheAppService responseCacheAppService,
    IEventBus eventBus,
    ILogger<OtherDomainEventHandler> logger,
    ILayeredService<IUpdatesConverter> layeredUpdatesService,
    ILayeredService<IAuthorizationConverter> layeredAuthorizationService,
    ILayeredService<IUserConverter> layeredUserService,
    ICacheManager<GlobalPrivacySettingsCacheItem> cacheManager)
    : DomainEventHandlerBase(objectMessageSender,
            commandBus,
            idGenerator,
            ackCacheService,
            responseCacheAppService),
        ISubscribeSynchronousTo<SignInSaga, SignInSagaId, SignInSuccessEvent>,
        ISubscribeSynchronousTo<SignInSaga, SignInSagaId, SignUpRequiredEvent>,
        //ISubscribeSynchronousTo<AppCodeAggregate, AppCodeId, SignUpRequiredEvent>,
        ISubscribeSynchronousTo<UpdatePinnedMessageSaga, UpdatePinnedMessageSagaId, UpdatePinnedMessageCompletedEvent>,
        //ISubscribeSynchronousTo<DeleteMessageSaga, DeleteMessageSagaId, DeleteMessagesCompletedEvent>,
        //ISubscribeSynchronousTo<DeleteMessageSaga2, DeleteMessageSaga2Id, DeleteMessagesCompletedEvent2>,
        ISubscribeSynchronousTo<ClearHistorySaga, ClearHistorySagaId, ClearSingleUserHistoryCompletedEvent>,
        //ISubscribeSynchronousTo<DeleteParticipantHistorySaga, DeleteParticipantHistorySagaId, DeleteParticipantHistoryCompletedEvent>,
        ISubscribeSynchronousTo<PeerNotifySettingsAggregate, PeerNotifySettingsId, PeerNotifySettingsUpdatedEvent>,
        ISubscribeSynchronousTo<UserAggregate, UserId, UserGlobalPrivacySettingsChangedEvent>,
        ISubscribeSynchronousTo<PinForwardedChannelMessageSaga, PinForwardedChannelMessageSagaId,
            PinChannelMessagePtsIncrementedEvent>

//,
//ISubscribeSynchronousTo<DeleteMessagesSaga4, DeleteMessagesSaga4Id, DeleteSelfMessagesCompletedEvent4>,
//ISubscribeSynchronousTo<DeleteMessagesSaga4, DeleteMessagesSaga4Id, DeleteOtherParticipantMessagesCompletedEvent4>,
//ISubscribeSynchronousTo<DeleteMessagesSaga4, DeleteMessagesSaga4Id, DeleteSelfHistoryCompletedEvent4>,
//ISubscribeSynchronousTo<DeleteMessagesSaga4, DeleteMessagesSaga4Id, DeleteOtherParticipantHistoryCompletedEvent4>,
//ISubscribeSynchronousTo<DeleteChannelMessagesSaga,DeleteChannelMessagesSagaId, DeleteChannelMessagesCompletedEvent>

{
    private readonly IObjectMessageSender _objectMessageSender = objectMessageSender;

    public async Task HandleAsync(
        IDomainEvent<ClearHistorySaga, ClearHistorySagaId, ClearSingleUserHistoryCompletedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        if (domainEvent.AggregateEvent.IsSelf)
        {
            var r = new TAffectedHistory
            {
                Pts = domainEvent.AggregateEvent.DeletedBoxItem.Pts,
                PtsCount = domainEvent.AggregateEvent.DeletedBoxItem.PtsCount,
                Offset = domainEvent.AggregateEvent.NextMaxId
            };
            await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.RequestInfo,
                r
            );
        }

        var date = DateTime.UtcNow.ToTimestamp();
        var updates = layeredUpdatesService.GetConverter(domainEvent.AggregateEvent.RequestInfo.Layer)
            .ToDeleteMessagesUpdates(domainEvent.AggregateEvent.ToPeerType,
                domainEvent.AggregateEvent.DeletedBoxItem,
                date);
        var layeredData = layeredUpdatesService.GetLayeredData(c =>
            c.ToDeleteMessagesUpdates(domainEvent.AggregateEvent.ToPeerType,
                domainEvent.AggregateEvent.DeletedBoxItem,
                date));
        await PushUpdatesToPeerAsync(
            new Peer(PeerType.User, domainEvent.AggregateEvent.DeletedBoxItem.OwnerPeerId),
            updates,
            pts: domainEvent.AggregateEvent.DeletedBoxItem.Pts,
            layeredData: layeredData);
    }

    public async Task HandleAsync(IDomainEvent<DeleteMessagesSaga4, DeleteMessagesSaga4Id, DeleteSelfMessagesCompletedEvent4> domainEvent, CancellationToken cancellationToken)
    {
        var r = new TAffectedMessages
        {
            Pts = domainEvent.AggregateEvent.Pts,
            PtsCount = domainEvent.AggregateEvent.PtsCount
        };
        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.RequestInfo,
            r,
            domainEvent.AggregateEvent.RequestInfo.UserId, domainEvent.AggregateEvent.Pts);

        var selfOtherDeviceUpdates = layeredUpdatesService.Converter.ToDeleteMessagesUpdates(PeerType.User,
            new DeletedBoxItem(domainEvent.AggregateEvent.RequestInfo.UserId, domainEvent.AggregateEvent.Pts,
                domainEvent.AggregateEvent.PtsCount, domainEvent.AggregateEvent.MessageIds),
            DateTime.UtcNow.ToTimestamp());
        await PushUpdatesToPeerAsync(domainEvent.AggregateEvent.RequestInfo.UserId.ToUserPeer(), selfOtherDeviceUpdates,
            domainEvent.AggregateEvent.RequestInfo.AuthKeyId);
    }

    public Task HandleAsync(IDomainEvent<DeleteMessagesSaga4, DeleteMessagesSaga4Id, DeleteOtherParticipantMessagesCompletedEvent4> domainEvent, CancellationToken cancellationToken)
    {
        var updates = layeredUpdatesService.Converter.ToDeleteMessagesUpdates(PeerType.User,
            new DeletedBoxItem(domainEvent.AggregateEvent.UserId, domainEvent.AggregateEvent.Pts,
                domainEvent.AggregateEvent.PtsCount, domainEvent.AggregateEvent.MessageIds),
            DateTime.UtcNow.ToTimestamp());
        var layeredUpdates = layeredUpdatesService.GetLayeredData(c => c.ToDeleteMessagesUpdates(PeerType.User,
            new DeletedBoxItem(domainEvent.AggregateEvent.UserId, domainEvent.AggregateEvent.Pts,
                domainEvent.AggregateEvent.PtsCount, domainEvent.AggregateEvent.MessageIds),
            DateTime.UtcNow.ToTimestamp()));

        return PushUpdatesToPeerAsync(domainEvent.AggregateEvent.UserId.ToUserPeer(), updates,
            pts: domainEvent.AggregateEvent.Pts, layeredData: layeredUpdates);

    }
    //public async Task HandleAsync(
    //    IDomainEvent<DeleteMessageSaga, DeleteMessageSagaId, DeleteMessagesCompletedEvent> domainEvent,
    //    CancellationToken cancellationToken)
    //{
    //    var date = DateTime.UtcNow.ToTimestamp();
    //    IUpdates? channelUpdates = null;
    //    Peer channelPeer;
    //    if (domainEvent.AggregateEvent.ToPeerType == PeerType.Channel)
    //    {
    //        channelPeer = new Peer(PeerType.Channel, domainEvent.AggregateEvent.SelfDeletedBoxItem.OwnerPeerId);
    //        channelUpdates = _layeredUpdatesService.GetConverter(domainEvent.AggregateEvent.RequestInfo.Layer)
    //            .ToDeleteMessagesUpdates(domainEvent.AggregateEvent.ToPeerType,
    //                domainEvent.AggregateEvent.SelfDeletedBoxItem,
    //                date);
    //        var globalSeqNo = await SavePushUpdatesAsync(
    //            channelPeer.PeerId,
    //            channelUpdates,
    //            domainEvent.AggregateEvent.SelfDeletedBoxItem.Pts,
    //            domainEvent.AggregateEvent.RequestInfo.AuthKeyId,
    //            chats: new List<long> { channelPeer.PeerId }
    //        );
    //        await AddRpcGlobalSeqNoForAuthKeyIdAsync(domainEvent.AggregateEvent.RequestInfo.ReqMsgId,
    //            domainEvent.AggregateEvent.RequestInfo.UserId,
    //            globalSeqNo);
    //        await UpdateSelfGlobalSeqNoAfterSendChannelMessageAsync(domainEvent.AggregateEvent.RequestInfo.UserId,
    //            globalSeqNo);
    //    }

    //    var r = new TAffectedMessages
    //    {
    //        Pts = domainEvent.AggregateEvent.SelfDeletedBoxItem.Pts,
    //        PtsCount = domainEvent.AggregateEvent.SelfDeletedBoxItem.PtsCount
    //    };

    //    await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.RequestInfo,
    //        r,
    //        domainEvent.AggregateEvent.RequestInfo.UserId,
    //        domainEvent.AggregateEvent.SelfDeletedBoxItem.Pts,
    //        domainEvent.AggregateEvent.ToPeerType);

    //    if (domainEvent.AggregateEvent.ToPeerType == PeerType.Channel)
    //    {
    //        channelPeer = new Peer(PeerType.Channel, domainEvent.AggregateEvent.SelfDeletedBoxItem.OwnerPeerId);
    //        var layeredChannelUpdates = _layeredUpdatesService.GetLayeredData(c =>
    //            c.ToDeleteMessagesUpdates(domainEvent.AggregateEvent.ToPeerType,
    //                domainEvent.AggregateEvent.SelfDeletedBoxItem,
    //                date));
    //        await PushUpdatesToChannelMemberAsync(
    //            new Peer(PeerType.User, domainEvent.AggregateEvent.RequestInfo.UserId),
    //            channelPeer,
    //            channelUpdates!,
    //            domainEvent.AggregateEvent.RequestInfo.AuthKeyId,
    //            skipSaveUpdates: true,
    //            layeredData: layeredChannelUpdates);
    //    }
    //    else
    //    {
    //        foreach (var deletedBoxItem in domainEvent.AggregateEvent.DeletedBoxItems)
    //        {
    //            var excludeAuthKeyId = 0L;
    //            if (deletedBoxItem.OwnerPeerId == domainEvent.AggregateEvent.SelfDeletedBoxItem.OwnerPeerId)
    //            {
    //                excludeAuthKeyId = domainEvent.AggregateEvent.RequestInfo.AuthKeyId;
    //            }

    //            var updates =
    //                _layeredUpdatesService.GetConverter(domainEvent.AggregateEvent.RequestInfo.Layer)
    //                    .ToDeleteMessagesUpdates(domainEvent.AggregateEvent.ToPeerType,
    //                        deletedBoxItem,
    //                        date);
    //            var layeredUpdates = _layeredUpdatesService.GetLayeredData(c =>
    //                c.ToDeleteMessagesUpdates(domainEvent.AggregateEvent.ToPeerType,
    //                    domainEvent.AggregateEvent.SelfDeletedBoxItem,
    //                    date));

    //            await PushUpdatesToPeerAsync(
    //                new Peer(PeerType.User, deletedBoxItem.OwnerPeerId),
    //                updates,
    //                excludeAuthKeyId,
    //                pts: deletedBoxItem.Pts,
    //                layeredData: layeredUpdates
    //            );
    //        }
    //    }
    //}

    //private async Task HandleClearHistoryCompletedAsync(DeleteMessagesCompletedEvent2 aggregateEvent)
    //{
    //    var r = new TAffectedHistory
    //    {
    //        Pts = aggregateEvent.SelfDeletedBoxItem.Pts,
    //        PtsCount = aggregateEvent.SelfDeletedBoxItem.PtsCount,
    //        Offset = aggregateEvent.SelfDeletedBoxItem.DeletedMessageIdList.Min()
    //    };
    //    await SendRpcMessageToClientAsync(aggregateEvent.RequestInfo, r);
    //    var date = DateTime.UtcNow.ToTimestamp();

    //    foreach (var deletedBoxItem in aggregateEvent.DeletedBoxItems)
    //    {
    //        var updates = _layeredUpdatesService.GetConverter(aggregateEvent.RequestInfo.Layer)
    //            .ToDeleteMessagesUpdates(aggregateEvent.ToPeerType, deletedBoxItem, date);
    //        var layeredData = _layeredUpdatesService.GetLayeredData(c =>
    //            c.ToDeleteMessagesUpdates(aggregateEvent.ToPeerType, deletedBoxItem, date));

    //        await PushUpdatesToPeerAsync(new Peer(PeerType.User, deletedBoxItem.OwnerPeerId),
    //            updates,
    //            layeredData: layeredData);
    //    }

    //    //var updates = _layeredUpdatesService.GetConverter(aggregateEvent.RequestInfo.Layer)
    //    //    .ToDeleteMessagesUpdates(aggregateEvent.ToPeerType, aggregateEvent.SelfDeletedBoxItem, date);
    //    //var layeredData= _layeredUpdatesService.GetLayeredData(c =>
    //    //    c.ToDeleteMessagesUpdates(PeerType.Channel,
    //    //        aggregateEvent.SelfDeletedBoxItem,
    //    //        date));
    //    //await PushUpdatesToPeerAsync(
    //    //    new Peer(PeerType.Channel, domainEvent.AggregateEvent.OwnerPeerId),
    //    //    updates,
    //    //    pts: domainEvent.AggregateEvent.Pts,
    //    //    layeredData: layeredData);
    //}

    //public async Task HandleAsync(IDomainEvent<DeleteMessageSaga2, DeleteMessageSaga2Id, DeleteMessagesCompletedEvent2> domainEvent,
    //    CancellationToken cancellationToken)
    //{
    //    if (domainEvent.AggregateEvent.IsClearHistory)
    //    {
    //        await HandleClearHistoryCompletedAsync(domainEvent.AggregateEvent);
    //        return;
    //    }

    //    var date = DateTime.UtcNow.ToTimestamp();

    //    var r = new TAffectedMessages
    //    {
    //        Pts = domainEvent.AggregateEvent.SelfDeletedBoxItem.Pts,
    //        PtsCount = domainEvent.AggregateEvent.SelfDeletedBoxItem.PtsCount
    //    };

    //    await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.RequestInfo,
    //        r,
    //        domainEvent.AggregateEvent.RequestInfo.UserId,
    //        domainEvent.AggregateEvent.SelfDeletedBoxItem.Pts,
    //        domainEvent.AggregateEvent.ToPeerType);

    //    foreach (var deletedBoxItem in domainEvent.AggregateEvent.DeletedBoxItems)
    //    {
    //        var excludeAuthKeyId = 0L;
    //        if (deletedBoxItem.OwnerPeerId == domainEvent.AggregateEvent.SelfDeletedBoxItem.OwnerPeerId)
    //        {
    //            excludeAuthKeyId = domainEvent.AggregateEvent.RequestInfo.AuthKeyId;
    //        }

    //        var updates =
    //            _layeredUpdatesService.GetConverter(domainEvent.AggregateEvent.RequestInfo.Layer)
    //                .ToDeleteMessagesUpdates(domainEvent.AggregateEvent.ToPeerType,
    //                    deletedBoxItem,
    //                    date);
    //        var layeredUpdates = _layeredUpdatesService.GetLayeredData(c =>
    //            c.ToDeleteMessagesUpdates(domainEvent.AggregateEvent.ToPeerType,
    //                domainEvent.AggregateEvent.SelfDeletedBoxItem,
    //                date));

    //        await PushUpdatesToPeerAsync(
    //            new Peer(PeerType.User, deletedBoxItem.OwnerPeerId),
    //            updates,
    //            excludeAuthKeyId,
    //            pts: deletedBoxItem.Pts,
    //            layeredData: layeredUpdates
    //        );
    //    }
    //}
    public async Task HandleAsync(IDomainEvent<SignInSaga, SignInSagaId, SignInSuccessEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        var tempAuthKeyId = domainEvent.AggregateEvent.TempAuthKeyId;
        await eventBus.PublishAsync(new UserSignInSuccessEvent(tempAuthKeyId,
                domainEvent.AggregateEvent.PermAuthKeyId,
                domainEvent.AggregateEvent.UserId,
                domainEvent.AggregateEvent.HasPassword ? PasswordState.WaitingForVerify : PasswordState.None))
     ;
        logger.LogDebug(
            "########################### User sign in success:{UserId} with tempAuthKeyId:{TempAuthKeyId} permAuthKeyId:{PermAuthKeyId} layer:{Layer}",
            domainEvent.AggregateEvent.UserId,
            domainEvent.AggregateEvent.TempAuthKeyId,
            domainEvent.AggregateEvent.PermAuthKeyId,
            domainEvent.AggregateEvent.RequestInfo.Layer
            );


        if (domainEvent.AggregateEvent.HasPassword)
        {
            //throw new BadRequestException("SESSION_PASSWORD_NEEDED");
            //ThrowHelper.ThrowUserFriendlyException("SESSION_PASSWORD_NEEDED");
            var rpcError = new TRpcError
            {
                ErrorCode = MyTelegramServerDomainConsts.BadRequestErrorCode,
                ErrorMessage = "SESSION_PASSWORD_NEEDED"
            };
            await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.RequestInfo, rpcError)
         ;
            return;
        }

        var user = layeredUserService.GetConverter(domainEvent.AggregateEvent.RequestInfo.Layer)
            .ToUser(domainEvent.AggregateEvent);

        var r = layeredAuthorizationService.GetConverter(domainEvent.AggregateEvent.RequestInfo.Layer)
            .CreateAuthorization(user);

        //await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.RequestInfo, r);
        await _objectMessageSender.SendRpcMessageToClientAsync(domainEvent.AggregateEvent.RequestInfo.ReqMsgId,
            r,
            domainEvent.AggregateEvent.RequestInfo.AuthKeyId,
            domainEvent.AggregateEvent.RequestInfo.PermAuthKeyId,
            user.Id);
    }

    public Task HandleAsync(IDomainEvent<SignInSaga, SignInSagaId, SignUpRequiredEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        return SendRpcMessageToClientAsync(domainEvent.AggregateEvent.RequestInfo,
            layeredAuthorizationService.GetConverter(domainEvent.AggregateEvent.RequestInfo.Layer)
                .CreateSignUpAuthorization());
    }

    public async Task HandleAsync(
        IDomainEvent<UpdatePinnedMessageSaga, UpdatePinnedMessageSagaId, UpdatePinnedMessageCompletedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        var r = layeredUpdatesService.GetConverter(domainEvent.AggregateEvent.RequestInfo.Layer)
            .ToSelfUpdatePinnedMessageUpdates(domainEvent.AggregateEvent);
        if (domainEvent.AggregateEvent.PmOneSide || domainEvent.AggregateEvent.ShouldReplyRpcResult)
        {
            await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.RequestInfo,
                r,
                domainEvent.AggregateEvent.SenderPeerId,
                domainEvent.AggregateEvent.Pts,
                //PtsType.OtherUpdates,
                domainEvent.AggregateEvent.ToPeer.PeerType
            );
            var layeredData =
                layeredUpdatesService.GetLayeredData(c =>
                    c.ToSelfUpdatePinnedMessageUpdates(domainEvent.AggregateEvent));
            await PushUpdatesToPeerAsync(
                new Peer(PeerType.User, domainEvent.AggregateEvent.OwnerPeerId),
                r,
                pts: domainEvent.AggregateEvent.Pts,
                layeredData: layeredData);
        }

        //else
        {
            await PushUpdatesToPeerAsync(
                domainEvent.AggregateEvent.ToPeer.PeerType == PeerType.Channel
                    ? new Peer(PeerType.Channel, domainEvent.AggregateEvent.OwnerPeerId)
                    : new Peer(PeerType.User, domainEvent.AggregateEvent.OwnerPeerId),
                layeredUpdatesService.Converter.ToUpdatePinnedMessageUpdates(domainEvent.AggregateEvent),
                excludeUserId: domainEvent.AggregateEvent.SenderPeerId,
                pts: domainEvent.AggregateEvent.Pts,
                layeredData: layeredUpdatesService.GetLayeredData(c =>
                    c.ToUpdatePinnedMessageUpdates(domainEvent.AggregateEvent))
            );
        }
    }

    //public async Task HandleAsync(IDomainEvent<DeleteParticipantHistorySaga, DeleteParticipantHistorySagaId, DeleteParticipantHistoryCompletedEvent> domainEvent,
    //    CancellationToken cancellationToken)
    //{
    //    var r = new TAffectedHistory
    //    {
    //        Pts = domainEvent.AggregateEvent.Pts,
    //        PtsCount = domainEvent.AggregateEvent.PtsCount,
    //        Offset = domainEvent.AggregateEvent.NextMaxId
    //    };
    //    await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.RequestInfo, r);
    //    var date = DateTime.UtcNow.ToTimestamp();
    //    var deletedBoxItem = new DeletedBoxItem(domainEvent.AggregateEvent.OwnerPeerId,
    //        domainEvent.AggregateEvent.Pts,
    //        domainEvent.AggregateEvent.PtsCount,
    //        domainEvent.AggregateEvent.MessageIds);
    //    var updates = _layeredUpdatesService.GetConverter(domainEvent.AggregateEvent.RequestInfo.Layer)
    //        .ToDeleteMessagesUpdates(PeerType.Channel,
    //            deletedBoxItem,
    //            date);
    //    var layeredData = _layeredUpdatesService.GetLayeredData(c =>
    //        c.ToDeleteMessagesUpdates(PeerType.Channel,
    //            deletedBoxItem,
    //            date));
    //    await PushUpdatesToPeerAsync(
    //        new Peer(PeerType.Channel, domainEvent.AggregateEvent.OwnerPeerId),
    //        updates,
    //        pts: domainEvent.AggregateEvent.Pts,
    //        layeredData: layeredData);
    //}

    public Task HandleAsync(IDomainEvent<PeerNotifySettingsAggregate, PeerNotifySettingsId, PeerNotifySettingsUpdatedEvent> domainEvent, CancellationToken cancellationToken)
    {
        var r = new TBoolTrue();

        return SendRpcMessageToClientAsync(domainEvent.AggregateEvent.RequestInfo, r);
    }

    public Task HandleAsync(IDomainEvent<UserAggregate, UserId, UserGlobalPrivacySettingsChangedEvent> domainEvent, CancellationToken cancellationToken)
    {
        return cacheManager.SetAsync(
            GlobalPrivacySettingsCacheItem.GetCacheKey(domainEvent.AggregateEvent.RequestInfo.UserId),
            new GlobalPrivacySettingsCacheItem(domainEvent.AggregateEvent.GlobalPrivacySettings.ArchiveAndMuteNewNoncontactPeers,
                domainEvent.AggregateEvent.GlobalPrivacySettings.KeepArchivedUnmuted,
                domainEvent.AggregateEvent.GlobalPrivacySettings.KeepArchivedFolders,
                domainEvent.AggregateEvent.GlobalPrivacySettings.HideReadMarks,
                domainEvent.AggregateEvent.GlobalPrivacySettings.NewNoncontactPeersRequirePremium
                )
        );
    }

    public async Task HandleAsync(IDomainEvent<DeleteMessagesSaga4, DeleteMessagesSaga4Id, DeleteSelfHistoryCompletedEvent4> domainEvent, CancellationToken cancellationToken)
    {
        var r = new TAffectedHistory
        {
            Pts = domainEvent.AggregateEvent.Pts,
            PtsCount = domainEvent.AggregateEvent.PtsCount,
            Offset = domainEvent.AggregateEvent.Offset
        };
        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.RequestInfo, r);
        var selfOtherDeviceUpdates = layeredUpdatesService.Converter.ToDeleteMessagesUpdates(PeerType.User,
            new DeletedBoxItem(domainEvent.AggregateEvent.RequestInfo.UserId, domainEvent.AggregateEvent.Pts,
                domainEvent.AggregateEvent.PtsCount, domainEvent.AggregateEvent.MessageIds),
            DateTime.UtcNow.ToTimestamp());

        await PushUpdatesToPeerAsync(domainEvent.AggregateEvent.RequestInfo.UserId.ToUserPeer(), selfOtherDeviceUpdates,
            domainEvent.AggregateEvent.RequestInfo.AuthKeyId);
    }

    public Task HandleAsync(IDomainEvent<DeleteMessagesSaga4, DeleteMessagesSaga4Id, DeleteOtherParticipantHistoryCompletedEvent4> domainEvent, CancellationToken cancellationToken)
    {
        var updates = layeredUpdatesService.Converter.ToDeleteMessagesUpdates(PeerType.User,
            new DeletedBoxItem(domainEvent.AggregateEvent.UserId, domainEvent.AggregateEvent.Pts,
                domainEvent.AggregateEvent.PtsCount, domainEvent.AggregateEvent.MessageIds),
            DateTime.UtcNow.ToTimestamp());
        var layeredUpdates = layeredUpdatesService.GetLayeredData(c => c.ToDeleteMessagesUpdates(PeerType.User,
            new DeletedBoxItem(domainEvent.AggregateEvent.UserId, domainEvent.AggregateEvent.Pts,
                domainEvent.AggregateEvent.PtsCount, domainEvent.AggregateEvent.MessageIds),
            DateTime.UtcNow.ToTimestamp()));

        return PushUpdatesToPeerAsync(domainEvent.AggregateEvent.UserId.ToUserPeer(), updates,
            pts: domainEvent.AggregateEvent.Pts, layeredData: layeredUpdates);
    }

    public Task HandleAsync(IDomainEvent<PinForwardedChannelMessageSaga, PinForwardedChannelMessageSagaId, PinChannelMessagePtsIncrementedEvent> domainEvent, CancellationToken cancellationToken)
    {
        var updateShort = new TUpdateShort
        {
            Update = new TUpdatePinnedChannelMessages
            {
                ChannelId = domainEvent.AggregateEvent.ChannelId,
                Messages = new TVector<int> { domainEvent.AggregateEvent.MessageId },
                Pinned = true,
                Pts = domainEvent.AggregateEvent.Pts,
                PtsCount = 1
            },
            Date = DateTime.UtcNow.ToTimestamp(),
        };

        return SendMessageToPeerAsync(domainEvent.AggregateEvent.ChannelId.ToChannelPeer(), updateShort);
    }
}
