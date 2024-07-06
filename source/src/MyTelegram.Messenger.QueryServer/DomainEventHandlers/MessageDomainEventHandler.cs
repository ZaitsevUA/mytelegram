using MyTelegram.Messenger.Services.Caching;
using MyTelegram.Messenger.Services.Interfaces;
using MyTelegram.Messenger.TLObjectConverters.Interfaces;
using MyTelegram.Services.TLObjectConverters;

namespace MyTelegram.Messenger.QueryServer.DomainEventHandlers;

public class MessageDomainEventHandler(
    IObjectMessageSender objectMessageSender,
    ICommandBus commandBus,
    IIdGenerator idGenerator,
    IAckCacheService ackCacheService,
    IResponseCacheAppService responseCacheAppService,
    ILayeredService<IMessageConverter> messageLayeredService,
    IChatEventCacheHelper chatEventCacheHelper,
    ILogger<MessageDomainEventHandler> logger,
    IQueryProcessor queryProcessor,
    ILayeredService<IUpdatesConverter> updatesLayeredService,
    ILayeredService<IChatConverter> chatLayeredService,
    ILayeredService<IUserConverter> userLayeredService,
    IPrivacyAppService privacyAppService,
    IPhotoAppService photoAppService)
    : DomainEventHandlerBase(objectMessageSender,
            commandBus,
            idGenerator,
            ackCacheService,
            responseCacheAppService),
        ISubscribeSynchronousTo<EditMessageSaga, EditMessageSagaId, OutboxMessageEditCompletedEvent>,
        ISubscribeSynchronousTo<EditMessageSaga, EditMessageSagaId, InboxMessageEditCompletedEvent>,
        ISubscribeSynchronousTo<SendMessageSaga, SendMessageSagaId, SendOutboxMessageCompletedEvent>,
        ISubscribeSynchronousTo<SendMessageSaga, SendMessageSagaId, ReceiveInboxMessageCompletedEvent>,
        ISubscribeSynchronousTo<MessageAggregate, MessageId, ChannelMessagePinnedEvent>,
        ISubscribeSynchronousTo<MessageAggregate, MessageId, MessageReplyUpdatedEvent>


{
    public Task HandleAsync(
        IDomainEvent<EditMessageSaga, EditMessageSagaId, InboxMessageEditCompletedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        var toPeer = new Peer(PeerType.User, domainEvent.AggregateEvent.OwnerPeerId);
        var updates = updatesLayeredService.Converter.ToEditUpdates(domainEvent.AggregateEvent);
        var layeredData = updatesLayeredService.GetLayeredData(c => c.ToEditUpdates(domainEvent.AggregateEvent));
        return PushUpdatesToPeerAsync(toPeer,
            updates,
            pts: domainEvent.AggregateEvent.Pts,
            updatesType: UpdatesType.Updates,
            layeredData: layeredData
        );
    }

    public async Task HandleAsync(
        IDomainEvent<EditMessageSaga, EditMessageSagaId, OutboxMessageEditCompletedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        var updates =
            updatesLayeredService.GetConverter(domainEvent.AggregateEvent.RequestInfo.Layer)
                .ToEditUpdates(domainEvent.AggregateEvent, domainEvent.AggregateEvent.SenderPeerId);
        var layeredData = updatesLayeredService.GetLayeredData(c =>
            c.ToEditUpdates(domainEvent.AggregateEvent, domainEvent.AggregateEvent.SenderPeerId));
        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.RequestInfo,
            updates,
            domainEvent.AggregateEvent.SenderPeerId,
            domainEvent.AggregateEvent.Pts,
            domainEvent.AggregateEvent.ToPeer.PeerType
        );
        await PushUpdatesToPeerAsync(
            new Peer(PeerType.User, domainEvent.AggregateEvent.SenderPeerId),
            updates,
            domainEvent.AggregateEvent.RequestInfo.AuthKeyId,
            pts: domainEvent.AggregateEvent.Pts,
            updatesType: UpdatesType.Updates,
            layeredData: layeredData);

        // Channel message shares the same message,edit out message should notify channel member
        if (domainEvent.AggregateEvent.ToPeer.PeerType == PeerType.Channel)
        {
            var channelEditUpdates = updatesLayeredService.Converter.ToEditUpdates(domainEvent.AggregateEvent, 0);
            var layeredChannelEditUpdates =
                updatesLayeredService.GetLayeredData(c => c.ToEditUpdates(domainEvent.AggregateEvent, 0));
            await PushUpdatesToPeerAsync(domainEvent.AggregateEvent.ToPeer,
                channelEditUpdates,
                pts: domainEvent.AggregateEvent.Pts,
                layeredData: layeredChannelEditUpdates);
        }
    }

    public Task HandleAsync(IDomainEvent<MessageAggregate, MessageId, ChannelMessagePinnedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public async Task HandleAsync(IDomainEvent<MessageAggregate, MessageId, MessageReplyUpdatedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        var messageReadModel = await queryProcessor.ProcessAsync(new GetMessageByIdQuery(MessageId
                .Create(domainEvent.AggregateEvent.OwnerChannelId, domainEvent.AggregateEvent.MessageId).Value),
            cancellationToken);

        if (messageReadModel != null)
        {
            var message = messageLayeredService.Converter.ToMessage(messageReadModel, null, null, 0);
            if (message is TMessage tMessage)
            {
                tMessage.EditDate = DateTime.UtcNow.ToTimestamp();
                tMessage.EditHide = true;
            }

            var update = new TUpdateEditChannelMessage
            {
                Message = message,
                Pts = domainEvent.AggregateEvent.Pts,
                PtsCount = 1
            };
            var updates = new TUpdates
            {
                Updates = new TVector<IUpdate>(update),
                Users = new TVector<IUser>(),
                Chats = new TVector<IChat>(),
                Date = DateTime.UtcNow.ToTimestamp()
            };

            await PushUpdatesToPeerAsync(domainEvent.AggregateEvent.OwnerChannelId.ToChannelPeer(), updates);
        }
    }

    public Task HandleAsync(
        IDomainEvent<SendMessageSaga, SendMessageSagaId, ReceiveInboxMessageCompletedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        return HandleReceiveMessageCompletedAsync(domainEvent.AggregateEvent);
    }

    public Task HandleAsync(
        IDomainEvent<SendMessageSaga, SendMessageSagaId, SendOutboxMessageCompletedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        return HandleSendOutboxMessageCompletedAsync(domainEvent.AggregateEvent);
    }

    private async Task<(IChannelReadModel channelReadModel, IPhotoReadModel? photoReadModel)> GetChannelAsync(
        long channelId)
    {
        var channelReadModel = await queryProcessor.ProcessAsync(new GetChannelByIdQuery(channelId));
        var photoReadModel = await photoAppService.GetPhotoAsync(channelReadModel!.PhotoId);

        return (channelReadModel, photoReadModel);
    }

    private async Task<IUser> GetUserAsync(long userId, long selfUserId)
    {
        var userReadModel = await queryProcessor.ProcessAsync(new GetUserByIdQuery(userId));
        var photos = await photoAppService.GetPhotosAsync(userReadModel);
        var privacyList = await privacyAppService.GetPrivacyListAsync(userId);
        return userLayeredService.Converter.ToUser(selfUserId, userReadModel!, photos, privacies: privacyList);
    }

    private async Task HandleCreateChannelAsync(SendOutboxMessageCompletedEvent aggregateEvent)
    {
        if (chatEventCacheHelper.TryRemoveChannelCreatedEvent(aggregateEvent.MessageItem.ToPeer.PeerId,
                out var eventData))
        {

            var channelId = eventData.ChannelId;
            var updates = updatesLayeredService.GetConverter(aggregateEvent.RequestInfo.Layer)
                .ToCreateChannelUpdates(eventData, aggregateEvent);
            var layeredData =
                updatesLayeredService.GetLayeredData(c => c.ToCreateChannelUpdates(eventData, aggregateEvent));

            IObject rpcData = updates;

            if (aggregateEvent.MessageItem.MessageSubType == MessageSubType.AutoCreateChannelFromChat)
            {
                var invitedUsers = new TInvitedUsers
                {
                    Updates = updates,
                    MissingInvitees = new TVector<IMissingInvitee>()
                };

                rpcData = invitedUsers;
            }

            await SendRpcMessageToClientAsync(aggregateEvent.RequestInfo,
                rpcData,
                aggregateEvent.MessageItem.SenderPeer.PeerId
            );

            await PushUpdatesToChannelSingleMemberAsync(channelId, aggregateEvent.MessageItem.SenderPeer,
                updates,
                aggregateEvent.RequestInfo.AuthKeyId,
                pts: aggregateEvent.Pts,
                layeredData: layeredData
            );
        }
        else
        {
            logger.LogWarning("Can not find create channel cache info,channelId={ChannelId}",
                aggregateEvent.MessageItem.ToPeer.PeerId);
        }
    }

    private async Task HandleCreateChatAsync(SendOutboxMessageCompletedEvent aggregateEvent)
    {
        if (chatEventCacheHelper.TryGetChatCreatedEvent(aggregateEvent.MessageItem.ToPeer.PeerId, out var eventData))
        {
            var chat = await queryProcessor.ProcessAsync(
                new GetChatByChatIdQuery(aggregateEvent.MessageItem.ToPeer.PeerId));
            var updates = updatesLayeredService.GetConverter(aggregateEvent.RequestInfo.Layer)
                .ToCreateChatUpdates(eventData, aggregateEvent, chat!);
            var invitedUsers = new TInvitedUsers
            {
                Updates = updates,
                MissingInvitees = new TVector<IMissingInvitee>()
            };
            var layeredData =
                updatesLayeredService.GetLayeredData(c => c.ToCreateChatUpdates(eventData, aggregateEvent, chat!));
            await SendRpcMessageToClientAsync(aggregateEvent.RequestInfo,
                invitedUsers,
                pts: aggregateEvent.Pts);
            await PushUpdatesToPeerAsync(aggregateEvent.MessageItem.SenderPeer,
                updates,
                aggregateEvent.RequestInfo.AuthKeyId,
                pts: aggregateEvent.Pts,
                layeredData: layeredData
            );
        }
        else
        {
            logger.LogWarning("Can not find cached chat info.{ToPeer}", aggregateEvent.MessageItem.ToPeer);
        }
    }

    private async Task HandleCreateChatAsync(ReceiveInboxMessageCompletedEvent aggregateEvent)
    {
        if (chatEventCacheHelper.TryGetChatCreatedEvent(aggregateEvent.MessageItem.ToPeer.PeerId, out var eventData))
        {
            var chat = await queryProcessor.ProcessAsync(
                new GetChatByChatIdQuery(aggregateEvent.MessageItem.ToPeer.PeerId));

            var updates = updatesLayeredService.Converter.ToCreateChatUpdates(eventData, aggregateEvent, chat!);
            var layeredData =
                updatesLayeredService.GetLayeredData(c => c.ToCreateChatUpdates(eventData, aggregateEvent, chat!));
            await PushUpdatesToPeerAsync(aggregateEvent.MessageItem.OwnerPeer,
                updates,
                pts: aggregateEvent.Pts,
                layeredData: layeredData
            );
        }
    }

    private Task HandleForwardMessageAsync(ReceiveInboxMessageCompletedEvent aggregateEvent)
    {
        var updates = updatesLayeredService.Converter.ToInboxForwardMessageUpdates(aggregateEvent);
        var layeredData = updatesLayeredService.GetLayeredData(c => c.ToInboxForwardMessageUpdates(aggregateEvent));
        return PushUpdatesToPeerAsync(aggregateEvent.MessageItem.OwnerPeer,
            updates,
            pts: aggregateEvent.Pts,
            layeredData: layeredData);
    }

    private async Task HandleInviteToChannelAsync(SendOutboxMessageCompletedEvent aggregateEvent)
    {
        if (chatEventCacheHelper.TryRemoveStartInviteToChannelEvent(aggregateEvent.MessageItem.ToPeer.PeerId,
                out var startInviteToChannelEvent))
        {
            var item = aggregateEvent.MessageItem;
            var channelReadModel = await queryProcessor
                .ProcessAsync(new GetChannelByIdQuery(item.ToPeer.PeerId));

            var updates = updatesLayeredService.GetConverter(aggregateEvent.RequestInfo.Layer)
                .ToInviteToChannelUpdates(
                    aggregateEvent,
                    startInviteToChannelEvent,
                    channelReadModel!,
                    true);
            var invitedUsers = new TInvitedUsers
            {
                Updates = updates,
                MissingInvitees = new TVector<IMissingInvitee>()
            };

            await SendRpcMessageToClientAsync(aggregateEvent.RequestInfo,
                invitedUsers,
                item.SenderPeer.PeerId);

            var updatesForChannelMember = updatesLayeredService.Converter.ToInviteToChannelUpdates(aggregateEvent,
                startInviteToChannelEvent,
                channelReadModel!,
                false
            );

            foreach (var memberUserId in startInviteToChannelEvent.MemberUidList)
            {
                await PushUpdatesToChannelSingleMemberAsync(item.ToPeer.PeerId, memberUserId.ToUserPeer(),
                    updatesForChannelMember);
            }

            await PushUpdatesToChannelMemberAsync(
                item.SenderPeer,
                item.ToPeer,
                updatesForChannelMember,
                excludeUserId: item.SenderPeer.PeerId,
                pts: aggregateEvent.Pts);

        }
    }

    private async Task HandleMigrateChatAsync(SendOutboxMessageCompletedEvent aggregateEvent)
    {
        var chatId = aggregateEvent.MessageItem.ToPeer.PeerId;
        if (chatEventCacheHelper.TryGetMigrateChannelId(chatId, out var channelId))
        {
            var chatReadModel = await queryProcessor.ProcessAsync(new GetChatByChatIdQuery(chatId));
            var channelReadModel = await queryProcessor.ProcessAsync(new GetChannelByIdQuery(channelId));
            var chatPhoto = await photoAppService.GetPhotoAsync(chatReadModel!.PhotoId);

            var updates = updatesLayeredService.GetConverter(aggregateEvent.RequestInfo.Layer)
                .ToMigrateChatUpdates(aggregateEvent, channelReadModel!, chatReadModel);

            var channel = chatLayeredService.GetConverter(aggregateEvent.RequestInfo.Layer)
                .ToChannel(aggregateEvent.RequestInfo.UserId, channelReadModel!, chatPhoto, null, false);
            var chat = chatLayeredService.GetConverter(aggregateEvent.RequestInfo.Layer)
                .ToChat(aggregateEvent.RequestInfo.UserId, chatReadModel, chatPhoto);
            if (updates is TUpdates tUpdates)
            {
                tUpdates.Chats.Add(channel);
                tUpdates.Chats.Add(chat);
            }

            var layeredData = updatesLayeredService.GetLayeredData(c =>
            {
                var selfOtherDeviceUpdates = c.ToMigrateChatUpdates(aggregateEvent, channelReadModel!, chatReadModel);
                if (selfOtherDeviceUpdates is TUpdates tSelfOtherDeviceUpdates)
                {
                    tSelfOtherDeviceUpdates.Chats.Add(channel);
                    tSelfOtherDeviceUpdates.Chats.Add(chat);
                }

                return selfOtherDeviceUpdates;
            });

            await SendRpcMessageToClientAsync(aggregateEvent.RequestInfo,
                updates,
                pts: aggregateEvent.Pts
            );

            await PushUpdatesToPeerAsync(aggregateEvent.MessageItem.SenderPeer,
                updates,
                aggregateEvent.RequestInfo.AuthKeyId,
                pts: aggregateEvent.Pts,
                layeredData: layeredData
            );
        }
    }

    private async Task HandleMigrateChatAsync(ReceiveInboxMessageCompletedEvent aggregateEvent)
    {
        if (chatEventCacheHelper.TryGetMigrateChannelId(aggregateEvent.MessageItem.ToPeer.PeerId, out var channelId))
        {
            var userId = aggregateEvent.MessageItem.OwnerPeer.PeerId;
            var chatReadModel =
                await queryProcessor.ProcessAsync(new GetChatByChatIdQuery(aggregateEvent.MessageItem.ToPeer.PeerId));
            var channelReadModel = await queryProcessor.ProcessAsync(new GetChannelByIdQuery(channelId));
            var chatPhoto = await photoAppService.GetPhotoAsync(chatReadModel!.PhotoId);

            var updates = updatesLayeredService.Converter.ToMigrateChatUpdates(aggregateEvent, channelId);
            var latestLayerChannel = chatLayeredService.Converter
                .ToChannel(userId, channelReadModel!, chatPhoto, null, false);
            var latestLayerChat = chatLayeredService.Converter
                .ToChat(userId, chatReadModel, chatPhoto);

            if (updates is TUpdates tUpdates)
            {
                tUpdates.Chats.Add(latestLayerChannel);
                tUpdates.Chats.Add(latestLayerChat);
            }

            var layeredData =
                updatesLayeredService.GetLayeredData(c =>
                {
                    var channel = chatLayeredService.GetConverter(c.RequestLayer)
                        .ToChannel(userId, channelReadModel!, chatPhoto, null, false);
                    var chat = chatLayeredService.GetConverter(c.RequestLayer)
                        .ToChat(userId, chatReadModel, chatPhoto);

                    var layeredUpdates = c.ToMigrateChatUpdates(aggregateEvent, channelId);
                    if (layeredUpdates is TUpdates tLayeredUpdates)
                    {
                        tLayeredUpdates.Chats.Add(channel);
                        tLayeredUpdates.Chats.Add(chat);
                    }

                    return layeredUpdates;
                });
            await PushUpdatesToPeerAsync(aggregateEvent.MessageItem.OwnerPeer,
                updates,
                pts: aggregateEvent.Pts,
                layeredData: layeredData
            );
        }
    }

    private async Task HandleReceiveMessageAsync(ReceiveInboxMessageCompletedEvent aggregateEvent)
    {
        var updates = updatesLayeredService.Converter.ToUpdates(aggregateEvent);

        var layeredData = updatesLayeredService.GetLayeredData(c => c.ToUpdates(aggregateEvent));
        await PushUpdatesToPeerAsync(aggregateEvent.MessageItem.OwnerPeer,
            updates,
            pts: aggregateEvent.Pts,
            layeredData: layeredData,
            senderUserId: aggregateEvent.MessageItem.SenderPeer.PeerId
        );
    }

    private Task HandleReceiveMessageCompletedAsync(ReceiveInboxMessageCompletedEvent aggregateEvent)
    {
        return aggregateEvent.MessageItem.MessageSubType switch
        {
            MessageSubType.CreateChat => HandleCreateChatAsync(aggregateEvent),
            MessageSubType.MigrateChat => HandleMigrateChatAsync(aggregateEvent),
            MessageSubType.UpdatePinnedMessage => HandleUpdatePinnedMessageAsync(aggregateEvent),
            MessageSubType.ForwardMessage => HandleForwardMessageAsync(aggregateEvent),
            _ => HandleReceiveMessageAsync(aggregateEvent)
        };
    }

    private async Task HandleSendMessageAsync(SendOutboxMessageCompletedEvent aggregateEvent)
    {
        var item = aggregateEvent.MessageItem;
        if (item.ToPeer.PeerType == PeerType.Channel)
        {
            await HandleSendMessageToChannelAsync(aggregateEvent);
            return;
        }

        var selfUpdates = updatesLayeredService.GetConverter(aggregateEvent.RequestInfo.Layer)
            .ToSelfUpdates(aggregateEvent);
        var updatesType = UpdatesType.Updates;
        if (item.MessageSubType == MessageSubType.Normal ||
            item.MessageSubType == MessageSubType.ForwardMessage)
        {
            updatesType = UpdatesType.NewMessages;
        }

        var selfLayeredData = updatesLayeredService.GetLayeredData(c => c.ToSelfUpdates(aggregateEvent));

        // when reqMsgId==0?
        // forward message/bot message
        if (aggregateEvent.RequestInfo.ReqMsgId == 0 || item.MessageSubType == MessageSubType.PhoneCall)
        {
            await PushUpdatesToPeerAsync(item.SenderPeer,
                selfUpdates,
                pts: aggregateEvent.Pts,
                //ptsType: ptsType,
                updatesType: updatesType,
                layeredData: selfLayeredData
            );
        }
        else
        {
            await ReplyRpcResultToSenderAsync(aggregateEvent.RequestInfo,
                item.SenderPeer,
                selfUpdates,
                aggregateEvent.GroupItemCount,
                item.SenderPeer.PeerId,
                aggregateEvent.Pts
            );
        }

        var selfOtherDeviceUpdates = updatesLayeredService.GetConverter(aggregateEvent.RequestInfo.Layer)
            .ToSelfOtherDeviceUpdates(aggregateEvent);
        var layeredSelfOtherDeviceUpdates =
            updatesLayeredService.GetLayeredData(c => c.ToSelfOtherDeviceUpdates(aggregateEvent));

        await PushUpdatesToPeerAsync(item.SenderPeer,
            selfOtherDeviceUpdates,
            aggregateEvent.RequestInfo.AuthKeyId,
            pts: aggregateEvent.Pts,
            updatesType: updatesType,
            layeredData: layeredSelfOtherDeviceUpdates
        );
    }

    private async Task HandleSendMessageToChannelAsync(SendOutboxMessageCompletedEvent aggregateEvent)
    {
        var item = aggregateEvent.MessageItem;
        var selfUpdates = updatesLayeredService.GetConverter(aggregateEvent.RequestInfo.Layer)
            .ToSelfUpdates(aggregateEvent);
        var selfOtherDeviceUpdates = updatesLayeredService.GetConverter(aggregateEvent.RequestInfo.Layer)
            .ToSelfOtherDeviceUpdates(aggregateEvent);
        var layeredSelfUpdates = updatesLayeredService.GetLayeredData(c => c.ToSelfUpdates(aggregateEvent));
        IChannelReadModel? channelReadModel = null;
        IPhotoReadModel? photoReadModel = null;
        IChat? chat = null;
        var isEditChannelPhoto = aggregateEvent.MessageItem.MessageSubType == MessageSubType.EditChannelPhoto;
        if (isEditChannelPhoto)
        {
            (channelReadModel, photoReadModel) = await GetChannelAsync(aggregateEvent.MessageItem.ToPeer.PeerId);
            chat = chatLayeredService.GetConverter(aggregateEvent.RequestInfo.Layer)
                .ToChannel(0, channelReadModel!, photoReadModel, null, false);
        }

        var channelUpdates = updatesLayeredService.GetConverter(aggregateEvent.RequestInfo.Layer)
            .ToChannelMessageUpdates(aggregateEvent);
        var layeredChannelUpdates =
            updatesLayeredService.GetLayeredData(c =>
            {
                var d = c.ToChannelMessageUpdates(aggregateEvent);
                if (chat != null)
                {
                    if (d is TUpdates tUpdates)
                    {
                        tUpdates.Chats.Add(chat);
                    }
                }

                //var ptsType = PtsType.OtherUpdates;
                return d;
            });
        var updatesType = UpdatesType.Updates;
        if (item.MessageSubType == MessageSubType.Normal || item.MessageSubType == MessageSubType.ForwardMessage)
        {
            updatesType = UpdatesType.NewMessages;
        }

        if (isEditChannelPhoto)
        {
            var selfChannel = chatLayeredService.GetConverter(aggregateEvent.RequestInfo.Layer)
                .ToChannel(aggregateEvent.RequestInfo.UserId, channelReadModel!, photoReadModel, null, false);

            if (selfUpdates is TUpdates tUpdates)
            {
                tUpdates.Chats.Add(selfChannel);
            }

            if (selfOtherDeviceUpdates is TUpdates tSelfOtherDeviceUpdates)
            {
                tSelfOtherDeviceUpdates.Chats.Add(selfChannel);
            }

            if (chat != null)
            {
                if (channelUpdates is TUpdates tUpdates1)
                {
                    tUpdates1.Chats.Add(chat);
                }
            }
        }

        var globalSeqNo = await SavePushUpdatesAsync(item.ToPeer.PeerId,
            channelUpdates,
            aggregateEvent.Pts,
            aggregateEvent.RequestInfo.AuthKeyId,
            aggregateEvent.RequestInfo.UserId,
            messageId: aggregateEvent.MessageItem.MessageId
        );
        await AddRpcGlobalSeqNoForAuthKeyIdAsync(aggregateEvent.RequestInfo.ReqMsgId, item.SenderPeer.PeerId,
                globalSeqNo)
            ;

        if (aggregateEvent.RequestInfo.ReqMsgId == 0 /*|| item.MessageSubType == MessageSubType.ForwardMessage*/)
        {
            await PushUpdatesToPeerAsync(new Peer(PeerType.User, aggregateEvent.RequestInfo.UserId),
                selfUpdates,
                pts: aggregateEvent.Pts,
                //ptsType: ptsType,
                updatesType: updatesType,
                layeredData: layeredSelfUpdates
            );
        }
        else
        {
            await ReplyRpcResultToSenderAsync(aggregateEvent.RequestInfo,
                aggregateEvent.MessageItem.SenderUserId.ToUserPeer(),
                selfUpdates,
                aggregateEvent.GroupItemCount,
                aggregateEvent.RequestInfo.UserId,
                aggregateEvent.Pts
            );

            await PushUpdatesToPeerAsync(item.SenderUserId.ToUserPeer(),
                selfOtherDeviceUpdates,
                aggregateEvent.RequestInfo.PermAuthKeyId,
                pts: aggregateEvent.Pts,
                updatesType: updatesType,
                //newMessage: newMessage,
                layeredData: updatesLayeredService.GetLayeredData(c => c.ToSelfOtherDeviceUpdates(aggregateEvent))
            );
        }

        // notify mentioned users
        if (aggregateEvent.MentionedUserIds?.Count > 0)
        {
            //var mentionedUpdates = updatesLayeredService.GetConverter(aggregateEvent.RequestInfo.Layer)
            //    .ToChannelMessageUpdates(aggregateEvent, true);
            //var layeredMentionUpdates =
            //    updatesLayeredService.GetLayeredData(c => c.ToChannelMessageUpdates(aggregateEvent, true));
            //foreach (var mentionedUserId in aggregateEvent.MentionedUserIds)
            //{
            //    if (mentionedUserId != aggregateEvent.RequestInfo.UserId)
            //    {
            //        await PushUpdatesToPeerAsync(new Peer(PeerType.User, mentionedUserId), mentionedUpdates,
            //            layeredData: layeredMentionUpdates);
            //    }
            //}
        }

        //Console.WriteLine($"Push message to channel:{item.ToPeer} pts:{aggregateEvent.Pts}");
        await PushUpdatesToPeerAsync(item.ToPeer,
                channelUpdates,
                aggregateEvent.RequestInfo.AuthKeyId,
                updatesType: updatesType,
                layeredData: layeredChannelUpdates,
                skipSaveUpdates: true
            )
            ;
    }

    private Task HandleSendOutboxMessageCompletedAsync(SendOutboxMessageCompletedEvent aggregateEvent)
    {
        return aggregateEvent.MessageItem.MessageSubType switch
        {
            MessageSubType.CreateChat => HandleCreateChatAsync(aggregateEvent),
            MessageSubType.CreateChannel => HandleCreateChannelAsync(aggregateEvent),
            MessageSubType.AutoCreateChannelFromChat => HandleCreateChannelAsync(aggregateEvent),
            MessageSubType.InviteToChannel => HandleInviteToChannelAsync(aggregateEvent),
            MessageSubType.UpdatePinnedMessage => HandleUpdatePinnedMessageAsync(aggregateEvent),
            MessageSubType.MigrateChat => HandleMigrateChatAsync(aggregateEvent),
            _ => HandleSendMessageAsync(aggregateEvent)
        };
    }
    private Task HandleUpdatePinnedMessageAsync(ReceiveInboxMessageCompletedEvent aggregateEvent)
    {
        var updates = updatesLayeredService.Converter.ToUpdatePinnedMessageUpdates(aggregateEvent);

        return PushUpdatesToPeerAsync(aggregateEvent.MessageItem.OwnerPeer,
            updates,
            pts: aggregateEvent.Pts);
    }

    private async Task HandleUpdatePinnedMessageAsync(SendOutboxMessageCompletedEvent aggregateEvent)
    {
        var updates = updatesLayeredService.GetConverter(aggregateEvent.RequestInfo.Layer)
            .ToUpdatePinnedMessageUpdates(aggregateEvent);

        await PushUpdatesToPeerAsync(aggregateEvent.MessageItem.SenderPeer,
            updates,
            pts: aggregateEvent.Pts);

        if (aggregateEvent.MessageItem.ToPeer.PeerType == PeerType.Channel)
        {
            var channelUpdates = updatesLayeredService.Converter.ToUpdatePinnedMessageServiceUpdates(aggregateEvent);
            if (channelUpdates is TUpdates tUpdates)
            {
                var user = await GetUserAsync(aggregateEvent.RequestInfo.UserId, 0);
                tUpdates.Users.Add(user);
            }

            var layeredChannelUpdates =
                updatesLayeredService.GetLayeredData(c => c.ToUpdatePinnedMessageServiceUpdates(aggregateEvent));
            await PushUpdatesToPeerAsync(aggregateEvent.MessageItem.ToPeer,
                channelUpdates,
                aggregateEvent.RequestInfo.AuthKeyId,
                pts: aggregateEvent.Pts,
                layeredData: layeredChannelUpdates);
        }
    }
}