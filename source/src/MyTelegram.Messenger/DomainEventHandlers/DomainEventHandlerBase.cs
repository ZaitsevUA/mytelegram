using MyTelegram.Domain.Aggregates.Updates;
using MyTelegram.Domain.Commands.Pts;
using MyTelegram.Domain.Commands.Updates;

namespace MyTelegram.Messenger.DomainEventHandlers;

public abstract class DomainEventHandlerBase(
    IObjectMessageSender objectMessageSender,
    ICommandBus commandBus,
    IIdGenerator idGenerator,
    IAckCacheService ackCacheService,
    IResponseCacheAppService responseCacheAppService)
{
    protected Task AddRpcGlobalSeqNoForAuthKeyIdAsync(long reqMsgId,
        long selfUserId,
        long globalSeqNo)
    {
        return ackCacheService.AddRpcPtsToCacheAsync(reqMsgId, 0, globalSeqNo, new Peer(PeerType.User, selfUserId));
    }

    protected async Task PushUpdatesToChannelMemberAsync(
        Peer senderPeer,
        Peer channelPeer,
        IUpdates updates,
        long? excludeAuthKeyId = null,
        long? excludeUserId = null,
        long? onlySendToUserId = null,
        long? onlySendToThisAuthKeyId = null,
        UpdatesType updatesType = UpdatesType.Updates,
        int pts = 0,
        bool skipSaveUpdates = false,
        LayeredData<IUpdates>? layeredData = null)
    {
        var globalSeqNo = 0L;
        if (!skipSaveUpdates)
        {
            globalSeqNo = await SavePushUpdatesAsync(
               channelPeer.PeerId,
               updates,
               pts,
               excludeAuthKeyId,
               excludeUserId,
               onlySendToUserId,
               onlySendToThisAuthKeyId,
               new List<long> { senderPeer.PeerId },
               updatesType: updatesType
                );
        }

        await objectMessageSender.PushMessageToPeerAsync(channelPeer,
            updates,
            excludeAuthKeyId,
            excludeUserId,
            onlySendToUserId,
            onlySendToThisAuthKeyId,
            pts,
           globalSeqNo: globalSeqNo,
           layeredData: layeredData);
    }

    protected async Task PushUpdatesToChannelSingleMemberAsync(
        long channelId,
        Peer channelMemberPeer,
        IUpdates updates,
        long? excludeAuthKeyId = null,
        long? excludeUserId = null,
        long? onlySendToUserId = null,
        long? onlySendToThisAuthKeyId = null,
        int pts = 0,
        UpdatesType updatesType = UpdatesType.Updates,
        LayeredData<IUpdates>? layeredData = null)
    {
        var globalSeqNo = await SavePushUpdatesAsync(
            channelId,
            updates,
            pts,
            excludeAuthKeyId,
            excludeUserId,
            onlySendToUserId,
            onlySendToThisAuthKeyId,
            updatesType: updatesType
            );
        await objectMessageSender.PushMessageToPeerAsync(channelMemberPeer,
            updates,
            excludeAuthKeyId,
            excludeUserId,
            onlySendToUserId,
            onlySendToThisAuthKeyId,
            pts,
            globalSeqNo: globalSeqNo,
            layeredData: layeredData);
    }

    protected async Task PushUpdatesToPeerAsync(Peer toPeer,
        IUpdates updates,
        long? excludeAuthKeyId = null,
        long? excludeUserId = null,
        long? onlySendToUserId = null,
        long? onlySendToThisAuthKeyId = null,
        int pts = 0,
        UpdatesType updatesType = UpdatesType.Updates,
        LayeredData<IUpdates>? layeredData = null,
        long? senderUserId = null,
        bool skipSaveUpdates = false,
        PushData? pushData = null
        )
    {
        long globalSeqNo = 0;
        List<long>? users = null;
        List<long>? chats = null;
        if (toPeer.PeerType == PeerType.User)
        {
            users = new List<long> { toPeer.PeerId };
            if (senderUserId.HasValue)
            {
                users.Add(senderUserId.Value);
            }
        }
        else if (toPeer.PeerType == PeerType.Channel || toPeer.PeerType == PeerType.Chat)
        {
            chats = new List<long> { toPeer.PeerId };
        }

        if (!skipSaveUpdates)
        {
            globalSeqNo = await SavePushUpdatesAsync(
                toPeer.PeerId,
                updates,
                pts,
                excludeAuthKeyId,
                excludeUserId,
                onlySendToUserId,
                onlySendToThisAuthKeyId,
                users,
                chats,
                updatesType
                );
        }

        await objectMessageSender.PushMessageToPeerAsync(toPeer,
            updates,
            excludeAuthKeyId,
            excludeUserId,
            onlySendToUserId,
            onlySendToThisAuthKeyId,
            pts,
            globalSeqNo: globalSeqNo,
            layeredData: layeredData,
            pushData: pushData
        );
    }

    protected async Task ReplyRpcResultToSenderAsync(
        RequestInfo requestInfo,
        Peer toPeer,
        IUpdates updates,
        //int groupItemCount,
        long selfUserId,
        int pts)
    {
        await SendRpcMessageToClientAsync(requestInfo,
            updates,
            selfUserId,
            pts,
            toPeer.PeerType);
        //if (groupItemCount > 1)
        //{
        //    await SendMultiMediaResultAsync(requestInfo,
        //        toPeer,
        //        updates,
        //        groupItemCount,
        //        selfUserId,
        //        pts);
        //}
        //else
        //{
        //    await SendRpcMessageToClientAsync(requestInfo,
        //        updates,
        //        selfUserId,
        //        pts,
        //        toPeer.PeerType);
        //}
    }

    protected async Task<long> SavePushUpdatesAsync(
        long ownerPeerId,
        //long? channelId,
        IUpdates updates,
        int pts,
        long? excludeAuthKeyId = null,
        long? excludeUserId = null,
        long? onlySendToUserId = null,
        long? onlySendToThisAuthKeyId = null,
        List<long>? users = null,
        List<long>? chats = null,
        UpdatesType updatesType = UpdatesType.Updates,
        int? messageId = null
    )
    {
        //UpdatesType updatesType = UpdatesType.Updates;
        var userIds = users;
        var chatIds = chats;
        //int? messageId = null;
        var date = DateTime.UtcNow.ToTimestamp();
        IUpdate[]? allUpdates = null;
        switch (updates)
        {
            case TUpdates updates1:
                allUpdates = updates1.Updates.ToArray();
                date = updates1.Date;
                if (updates1.Users.Count > 0)
                {
                    userIds ??= new();
                    userIds.AddRange(updates1.Users.Select(p => p.Id));
                }

                if (updates1.Chats.Count > 0)
                {
                    chatIds ??= new();
                    chatIds.AddRange(updates1.Chats.Select(p => p.Id));
                }

                break;
            case TUpdateShort updateShort:
                date = updateShort.Date;
                allUpdates = new[] { updateShort.Update };
                break;
            case TUpdateShortChatMessage updateShortChatMessage:
                date = updateShortChatMessage.Date;
                messageId = updateShortChatMessage.Id;
                updatesType = UpdatesType.NewMessages;

                break;
            case TUpdateShortMessage updateShortMessage:
                date = updateShortMessage.Date;
                messageId = updateShortMessage.Id;
                updatesType = UpdatesType.NewMessages;
                break;
            case TUpdateShortSentMessage updateShortSentMessage:
                date = updateShortSentMessage.Date;
                messageId = updateShortSentMessage.Id;
                updatesType = UpdatesType.NewMessages;
                break;
            case TUpdatesTooLong:
                return 0;
                //return Task.FromResult<long>(0);
        }


        var globalSeqNo = await idGenerator.NextLongIdAsync(IdType.GlobalSeqNo);

        _ = Task.Run(() =>
          {
              var command = new CreateUpdatesCommand(UpdatesId.New,
                  ownerPeerId,
                  excludeAuthKeyId,
                  excludeUserId,
                  onlySendToUserId,
                  onlySendToThisAuthKeyId,
                  updatesType,
                  pts,
                  messageId,
                  date,
                  globalSeqNo,
                  allUpdates,
                  userIds,
                  chatIds
              );
              commandBus.PublishAsync(command, default);
          });

        return globalSeqNo;
    }

    protected async Task PushMessageToPeerAsync<TData>(
        Peer toPeer,
        TData data,
        long? excludeAuthKeyId = null,
        long? excludeUserId = null,
        long? onlySendToUserId = null,
        long? onlySendToThisAuthKeyId = null,
        int pts = 0,
        UpdatesType updatesType = UpdatesType.Updates,
        LayeredData<TData>? layeredData = null,
        long channelId = 0,
        PushData? pushData = null
        ) where TData : IObject
    {
        var globalSeqNo = 0L;
        if (data is IUpdates updates)
        {
            globalSeqNo = await SavePushUpdatesAsync(
               toPeer.PeerId,
               //channelId,
               updates,
               pts,
               excludeAuthKeyId,
               excludeUserId,
               //onlySendToUserId,
               onlySendToThisAuthKeyId: onlySendToThisAuthKeyId,
               updatesType: updatesType
               );
        }
        await objectMessageSender.PushMessageToPeerAsync(toPeer,
            data,
            excludeAuthKeyId,
            excludeUserId,
            onlySendToUserId,
            onlySendToThisAuthKeyId,
            pts,
            globalSeqNo: globalSeqNo,
            layeredData: layeredData,
            pushData: pushData
            );
    }

    protected async Task PushMessageToPeerAsync<TData, TExtraData>(Peer toPeer,
        TData data,
        long? excludeAuthKeyId = null,
        long? excludeUserId = null,
        long? onlySendToUserId = null,
        long? onlySendToThisAuthKeyId = null,
        int pts = 0,
        UpdatesType updatesType = UpdatesType.Updates,
        LayeredData<TData>? layeredData = null,
        TExtraData? extraData = default,
        PushData? pushData = null
        ) where TData : IObject
    {
        var globalSeqNo = 0L;

        if (data is IUpdates updates)
        {
            globalSeqNo = await SavePushUpdatesAsync(
                toPeer.PeerId,
                //0,
                updates,
                pts,
                excludeAuthKeyId,
                excludeUserId,
                onlySendToUserId,
                onlySendToThisAuthKeyId,
                updatesType: updatesType
            );
        }

        await objectMessageSender.PushMessageToPeerAsync(toPeer,
            data,
            excludeAuthKeyId,
            excludeUserId,
            onlySendToUserId,
            onlySendToThisAuthKeyId,
            pts,
            globalSeqNo: globalSeqNo,
            layeredData: layeredData,
            extraData: extraData,
            pushData: pushData
            );
    }

    protected Task PushMessageToAuthKeyIdAsync<TData>(Peer toPeer, TData data, long authKeyId, int? qts = null, UpdatesType updatesType = UpdatesType.Updates)
        where TData : IObject
    {
        return objectMessageSender.PushMessageToPeerAsync(toPeer, data, onlySendToThisAuthKeyId: authKeyId, qts: qts);
    }

    protected async Task SendMultiMediaResultAsync(
        RequestInfo requestInfo,
        //long reqMsgId,
        Peer toPeer,
        IUpdates updates,
        int groupItemCount,
        long selfUserId,
        int pts)
    {
        var cachedCount = responseCacheAppService.AddToCache(requestInfo.ReqMsgId, updates);
        if (cachedCount == groupItemCount)
        {
            if (responseCacheAppService.TryRemoveResponseList(requestInfo.ReqMsgId, out var responseList))
            {
                var updatesAllInOne = new TUpdates
                {
                    Updates = new TVector<IUpdate>(),
                    Users = new TVector<IUser>(),
                    Chats = new TVector<IChat>(),
                    Date = DateTime.UtcNow.ToTimestamp()
                };
                foreach (var allUpdate in responseList)
                {
                    if (allUpdate is TUpdates updatesItem)
                    {
                        foreach (var update in updatesItem.Updates)
                        {
                            updatesAllInOne.Updates.Add(update);
                        }
                    }
                }

                await SendRpcMessageToClientAsync(requestInfo,
                    updatesAllInOne,
                    selfUserId,
                    pts,
                    toPeer.PeerType);
            }
        }
    }

    protected async Task SendRpcMessageToClientAsync<TData>(
        //long reqMsgId,
        RequestInfo requestInfo,
        TData data,
        long selfUserId = 0,
        int pts = 0,
        PeerType toPeerType = PeerType.User
    ) where TData : IObject
    {
        if (requestInfo.ReqMsgId == 0)
        {
            return;
        }

        if (pts > 0 && selfUserId != 0 && toPeerType != PeerType.Channel)
        {
            await ackCacheService.AddRpcPtsToCacheAsync(requestInfo.ReqMsgId, pts, 0, new Peer(PeerType.User, selfUserId))
         ;
        }

        await objectMessageSender.SendRpcMessageToClientAsync(requestInfo, data);
    }


    protected Task UpdateSelfGlobalSeqNoAfterSendChannelMessageAsync(long userId,
        long globalSeqNo)
    {
        Task.Run(() =>
        {
            var updateGlobalSeqNoCommand = new UpdateGlobalSeqNoCommand(PtsId.Create(userId), userId, 0, globalSeqNo);
            commandBus.PublishAsync(updateGlobalSeqNoCommand, default);
        });

        return Task.CompletedTask;
    }
}
