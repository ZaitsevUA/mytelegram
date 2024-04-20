namespace MyTelegram.QueryHandlers.InMemory;

public static class MyQueryHandlerExtensions
{
    public static Task<T?> FirstOrDefaultAsync<T>(this IQueryable<T> query,
        Func<T, bool> predicate,
        CancellationToken cancellationToken)
    {
        return Task.FromResult(query.FirstOrDefault(predicate));
    }

    public static Task<T> SingleAsync<T>(this IQueryable<T> query,
        Func<T, bool> predicate,
        CancellationToken cancellationToken)
    {
        return Task.FromResult(query.Single(predicate));
    }

    public static Task<T?> SingleOrDefaultAsync<T>(this IQueryable<T> query,
        Func<T, bool> predicate,
        CancellationToken cancellationToken)
    {
        return Task.FromResult(query.SingleOrDefault(predicate));
    }

    public static Task<List<T>> ToListAsync<T>(this IQueryable<T> query,
        CancellationToken cancellationToken)
    {
        return Task.FromResult(query.ToList());
    }
}

public class GetAllDraftQueryHandler(IMyInMemoryReadStore<DraftReadModel> store)
    : MyQueryHandler<DraftReadModel>(store),
        IQueryHandler<GetAllDraftQuery, IReadOnlyCollection<IDraftReadModel>>
{
    public async Task<IReadOnlyCollection<IDraftReadModel>> ExecuteQueryAsync(GetAllDraftQuery query,
        CancellationToken cancellationToken)
    {
        return await (await CreateQueryAsync().ConfigureAwait(false))
                .Where(p => p.OwnerPeerId == query.OwnerPeerId)
                .ToListAsync(cancellationToken)
            ;
    }
}

public class GetAllUserNameQueryHandler(IMyInMemoryReadStore<UserNameReadModel> store)
    : MyQueryHandler<UserNameReadModel>(store),
        IQueryHandler<GetAllUserNameQuery, IReadOnlyCollection<string>>
{
    public async Task<IReadOnlyCollection<string>> ExecuteQueryAsync(GetAllUserNameQuery query,
        CancellationToken cancellationToken)
    {
        return (await CreateQueryAsync().ConfigureAwait(false))
            .OrderBy(p => p.Id)
            .Skip(query.Skip)
            .Take(query.Limit)
            .Select(p => p.UserName)
            .ToList();
    }
}

public class
    GetChannelByChannelIdListQueryHandler(IMyInMemoryReadStore<ChannelReadModel> store)
    : MyQueryHandler<ChannelReadModel>(store),
        IQueryHandler<GetChannelByChannelIdListQuery,
            IReadOnlyCollection<IChannelReadModel>>
{
    public async Task<IReadOnlyCollection<IChannelReadModel>> ExecuteQueryAsync(GetChannelByChannelIdListQuery query,
        CancellationToken cancellationToken)
    {
        if (query.ChannelIdList.Count == 0)
        {
            return new List<ChannelReadModel>();
        }

        // todo:pagination
        return await (await CreateQueryAsync().ConfigureAwait(false))
                .Where(p => query.ChannelIdList.Contains(p.ChannelId))
                .ToListAsync(cancellationToken)
            ;
    }
}

public class GetChannelByIdQueryHandler(IMyInMemoryReadStore<ChannelReadModel> store)
    : MyQueryHandler<ChannelReadModel>(store),
        IQueryHandler<GetChannelByIdQuery, IChannelReadModel>
{
    public async Task<IChannelReadModel> ExecuteQueryAsync(GetChannelByIdQuery query,
        CancellationToken cancellationToken)
    {
        var id = ChannelId.Create(query.ChannelId).Value;
        return await (await CreateQueryAsync().ConfigureAwait(false)).SingleAsync(p => p.Id == id, cancellationToken)
            ;
    }
}

public class GetChannelFullByIdQueryHandler(IMyInMemoryReadStore<ChannelFullReadModel> store)
    : MyQueryHandler<ChannelFullReadModel>(store),
        IQueryHandler<GetChannelFullByIdQuery, IChannelFullReadModel?>
{
    public async Task<IChannelFullReadModel?> ExecuteQueryAsync(GetChannelFullByIdQuery query,
        CancellationToken cancellationToken)
    {
        var id = ChannelId.Create(query.ChannelId).Value;
        return await (await CreateQueryAsync().ConfigureAwait(false))
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken)
            ;
    }
}

public class
    GetChannelIdListByMemberUidQueryHandler(IMyInMemoryReadStore<ChannelMemberReadModel> store)
    : MyQueryHandler<ChannelMemberReadModel>(store),
        IQueryHandler<GetChannelIdListByMemberUidQuery,
            IReadOnlyCollection<long>>
{
    public async Task<IReadOnlyCollection<long>> ExecuteQueryAsync(GetChannelIdListByMemberUidQuery query,
        CancellationToken cancellationToken)
    {
        // todo:pass page size parameter
        return await (await CreateQueryAsync().ConfigureAwait(false))
                .Where(p => p.UserId == query.MemberUid)
                .OrderBy(p => p.Id)
                .Take(100)
                .Select(p => p.ChannelId)
                .ToListAsync(cancellationToken)
            ;
    }
}

public class GetChannelIdListByUidQueryHandler(IMyInMemoryReadStore<ChannelMemberReadModel> store)
    : MyQueryHandler<ChannelMemberReadModel>(store),
        IQueryHandler<GetChannelIdListByUidQuery, IReadOnlyCollection<long>>
{
    public async Task<IReadOnlyCollection<long>> ExecuteQueryAsync(GetChannelIdListByUidQuery query,
        CancellationToken cancellationToken)
    {
        return await (await CreateQueryAsync().ConfigureAwait(false))
                .Where(p => p.UserId == query.UserId)
                .Select(p => p.ChannelId)
                .ToListAsync(cancellationToken)
            ;
    }
}

public class GetChannelMemberByUidQueryHandler(IMyInMemoryReadStore<ChannelMemberReadModel> store)
    : MyQueryHandler<ChannelMemberReadModel>(store),
        IQueryHandler<GetChannelMemberByUserIdQuery, IChannelMemberReadModel?>
{
    public async Task<IChannelMemberReadModel?> ExecuteQueryAsync(GetChannelMemberByUserIdQuery query,
        CancellationToken cancellationToken)
    {
        var id = ChannelMemberId.Create(query.ChannelId, query.UserId).Value;
        return await (await CreateQueryAsync().ConfigureAwait(false))
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken)
            ;
    }
}

public class GetChannelMemberListByChannelIdListQueryHandler(IMyInMemoryReadStore<ChannelMemberReadModel> store)
    : MyQueryHandler<ChannelMemberReadModel>(store), IQueryHandler<
        GetChannelMemberListByChannelIdListQuery, IReadOnlyCollection<IChannelMemberReadModel>>
{
    public async Task<IReadOnlyCollection<IChannelMemberReadModel>> ExecuteQueryAsync(
        GetChannelMemberListByChannelIdListQuery query,
        CancellationToken cancellationToken)
    {
        return await (await CreateQueryAsync().ConfigureAwait(false))
                .Where(p => p.UserId == query.MemberUid && query.ChannelIdList.Contains(p.ChannelId))
                .ToListAsync(cancellationToken)
            ;
    }
}

public class GetChannelMembersByChannelIdQueryHandler(IMyInMemoryReadStore<ChannelMemberReadModel> store)
    : MyQueryHandler<ChannelMemberReadModel>(store),
        IQueryHandler<GetChannelMembersByChannelIdQuery,
            IReadOnlyCollection<IChannelMemberReadModel>>
{
    public async Task<IReadOnlyCollection<IChannelMemberReadModel>> ExecuteQueryAsync(
        GetChannelMembersByChannelIdQuery query,
        CancellationToken cancellationToken)
    {
        return await (await CreateQueryAsync().ConfigureAwait(false))
                .Where(p => !p.Left && p.ChannelId == query.ChannelId)
                .WhereIf(query.MemberUidList.Count > 0, p => query.MemberUidList.Contains(p.UserId))
                .OrderBy(p => p.Id)
                .Skip(query.Offset)
                .Take(query.Limit)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false)
            ;
    }
}

public class GetChannelPushUpdatesBySeqNoQueryHandler(IMyInMemoryReadStore<PushUpdatesReadModel> store)
    : MyQueryHandler<PushUpdatesReadModel>(store),
        IQueryHandler<GetChannelPushUpdatesBySeqNoQuery,
            IReadOnlyCollection<IPushUpdatesReadModel>>
{
    public async Task<IReadOnlyCollection<IPushUpdatesReadModel>> ExecuteQueryAsync(
        GetChannelPushUpdatesBySeqNoQuery query,
        CancellationToken cancellationToken)
    {
        return await (await CreateQueryAsync().ConfigureAwait(false))
                .Where(p => p.SeqNo > query.SeqNo && query.ChannelIdList.Contains(p.PeerId))
                .Take(query.Limit)
                .ToListAsync(cancellationToken)
            ;
    }
}

public class GetChatByChatIdListQueryHandler(IMyInMemoryReadStore<ChatReadModel> store)
    : MyQueryHandler<ChatReadModel>(store),
        IQueryHandler<GetChatByChatIdListQuery, IReadOnlyList<IChatReadModel>>
{
    public async Task<IReadOnlyList<IChatReadModel>> ExecuteQueryAsync(GetChatByChatIdListQuery query,
        CancellationToken cancellationToken)
    {
        return await (await CreateQueryAsync().ConfigureAwait(false))
                .Where(p => query.ChatIdList.Contains(p.ChatId))
                .ToListAsync(cancellationToken)
            ;
    }
}

public class GetChatByChatIdQueryHandler(IMyInMemoryReadStore<ChatReadModel> store)
    : MyQueryHandler<ChatReadModel>(store),
        IQueryHandler<GetChatByChatIdQuery, IChatReadModel?>
{
    public async Task<IChatReadModel?> ExecuteQueryAsync(GetChatByChatIdQuery query,
        CancellationToken cancellationToken)
    {
        var id = ChatId.Create(query.ChatId).Value;
        return await (await CreateQueryAsync().ConfigureAwait(false))
                .SingleOrDefaultAsync(p => p.Id == id, cancellationToken)
            ;
    }
}

public class
    GetChatInvitesQueryHandler(IMyInMemoryReadStore<ChatInviteReadModel> store)
    : MyQueryHandler<ChatInviteReadModel>(store),
        IQueryHandler<GetChatInvitesQuery, IReadOnlyCollection<IChatInviteReadModel>>
{
    public async Task<IReadOnlyCollection<IChatInviteReadModel>> ExecuteQueryAsync(GetChatInvitesQuery query,
        CancellationToken cancellationToken)
    {
        return await (await CreateQueryAsync().ConfigureAwait(false))
                .Where(p =>
                    p.Revoked == query.Revoked && p.PeerId == query.PeerId && p.AdminId == query.AdminId)
                .ToListAsync(cancellationToken)
            ;
    }
}

public class GetDeviceByAuthKeyIdQueryHandler(IMyInMemoryReadStore<DeviceReadModel> store)
    : MyQueryHandler<DeviceReadModel>(store),
        IQueryHandler<GetDeviceByAuthKeyIdQuery, IDeviceReadModel?>
{
    public async Task<IDeviceReadModel?> ExecuteQueryAsync(GetDeviceByAuthKeyIdQuery query,
        CancellationToken cancellationToken)
    {
        var id = DeviceId.Create(query.AuthKeyId).Value;

        return await (await CreateQueryAsync().ConfigureAwait(false))
                .SingleOrDefaultAsync(p => p.Id == id, cancellationToken)
            ;
    }
}

public class GetDeviceByHashQueryHandler(IMyInMemoryReadStore<DeviceReadModel> store)
    : MyQueryHandler<DeviceReadModel>(store),
        IQueryHandler<GetDeviceByHashQuery, IDeviceReadModel?>
{
    public async Task<IDeviceReadModel?> ExecuteQueryAsync(GetDeviceByHashQuery query,
        CancellationToken cancellationToken)
    {
        return await (await CreateQueryAsync().ConfigureAwait(false))
                .FirstOrDefaultAsync(p => p.UserId == query.UserId && p.Hash == query.Hash, cancellationToken)
            ;
    }
}

public class GetDeviceByUidQueryHandler(IMyInMemoryReadStore<DeviceReadModel> store)
    : MyQueryHandler<DeviceReadModel>(store),
        IQueryHandler<GetDeviceByUidQuery, IReadOnlyCollection<IDeviceReadModel>>
{
    public async Task<IReadOnlyCollection<IDeviceReadModel>> ExecuteQueryAsync(GetDeviceByUidQuery query,
        CancellationToken cancellationToken)
    {
        return await (await CreateQueryAsync().ConfigureAwait(false))
                .Where(p => p.UserId == query.UserId && p.IsActive)
                .ToListAsync(cancellationToken)
            ;
    }
}

public class GetDialogByIdQueryHandler(IMyInMemoryReadStore<DialogReadModel> store)
    : MyQueryHandler<DialogReadModel>(store),
        IQueryHandler<GetDialogByIdQuery, IDialogReadModel?>
{
    public async Task<IDialogReadModel?> ExecuteQueryAsync(GetDialogByIdQuery query,
        CancellationToken cancellationToken)
    {
        return await (await CreateQueryAsync().ConfigureAwait(false))
                .SingleOrDefaultAsync(p => p.Id == query.Id.Value, cancellationToken)
            ;
    }
}

public class GetDialogsQueryHandler(IMyInMemoryReadStore<DialogReadModel> store)
    : MyQueryHandler<DialogReadModel>(store),
        IQueryHandler<GetDialogsQuery, IReadOnlyList<IDialogReadModel>>
{
    public async Task<IReadOnlyList<IDialogReadModel>> ExecuteQueryAsync(GetDialogsQuery query,
        CancellationToken cancellationToken)
    {
        // Fix native aot missing metadata issues
        var needOffsetDate = false;
        var offsetDate = DateTime.UtcNow;

        if (query.OffsetDate.HasValue)
        {
            needOffsetDate = true;
            offsetDate = query.OffsetDate.Value;
        }

        return await (await CreateQueryAsync().ConfigureAwait(false))
                .Where(p => p.OwnerId == query.OwnerId)
                .WhereIf(needOffsetDate, p => p.CreationTime > offsetDate)
                .WhereIf(query.Pinned.HasValue, p => p.Pinned == query.Pinned!.Value)
                .WhereIf(query.PeerIdList?.Count > 0, p => query.PeerIdList!.Contains(p.ToPeerId))
                .OrderBy(p => p.TopMessage)
                .Skip(0)
                .Take(query.Limit)
                .ToListAsync(cancellationToken)
            ;
    }
}

public class
    GetJoinedChannelIdListQueryHandler(IMyInMemoryReadStore<ChannelMemberReadModel> store)
    : MyQueryHandler<ChannelMemberReadModel>(store),
        IQueryHandler<GetJoinedChannelIdListQuery, IReadOnlyCollection<long>>
{
    public async Task<IReadOnlyCollection<long>> ExecuteQueryAsync(GetJoinedChannelIdListQuery query,
        CancellationToken cancellationToken)
    {
        return await (await CreateQueryAsync().ConfigureAwait(false))
                .Where(p => p.UserId == query.MemberUid && query.ChannelIdList.Contains(p.ChannelId))
                .Select(p => p.ChannelId)
                .ToListAsync(cancellationToken)
            ;
    }
}

public class GetKickedChannelMembersQueryHandler(IMyInMemoryReadStore<ChannelMemberReadModel> store)
    : MyQueryHandler<ChannelMemberReadModel>(store),
        IQueryHandler<GetKickedChannelMembersQuery,
            IReadOnlyCollection<IChannelMemberReadModel>>
{
    public async Task<IReadOnlyCollection<IChannelMemberReadModel>> ExecuteQueryAsync(
        GetKickedChannelMembersQuery query,
        CancellationToken cancellationToken)
    {
        return await (await CreateQueryAsync().ConfigureAwait(false))
                .Where(p => p.ChannelId == query.ChannelId && p.Kicked)
                .OrderBy(p => p.Id)
                .Skip(query.Offset)
                .Take(query.Limit)
                .ToListAsync(cancellationToken)
            ;
    }
}

public class
    GetMegaGroupByUidQueryHandler(IMyInMemoryReadStore<ChannelReadModel> store)
    : MyQueryHandler<ChannelReadModel>(store),
        IQueryHandler<GetMegaGroupByUidQuery, IReadOnlyCollection<IChannelReadModel>>
{
    public async Task<IReadOnlyCollection<IChannelReadModel>> ExecuteQueryAsync(GetMegaGroupByUidQuery query,
        CancellationToken cancellationToken)
    {
        return await (await CreateQueryAsync().ConfigureAwait(false))
                .Where(p => p.MegaGroup && p.CreatorId == query.UserId)
                .ToListAsync(cancellationToken)
            ;
    }
}

public class GetMessageByIdQueryHandler(IMyInMemoryReadStore<MessageReadModel> store)
    : MyQueryHandler<MessageReadModel>(store),
        IQueryHandler<GetMessageByIdQuery, IMessageReadModel?>
{
    public async Task<IMessageReadModel?> ExecuteQueryAsync(GetMessageByIdQuery query,
        CancellationToken cancellationToken)
    {
        return await (await CreateQueryAsync().ConfigureAwait(false))
            .FirstOrDefaultAsync(p => p.Id == query.Id, cancellationToken);
    }
}

public class GetMessageIdListQueryHandler(IMyInMemoryReadStore<MessageReadModel> store)
    : MyQueryHandler<MessageReadModel>(store),
        IQueryHandler<GetMessageIdListQuery, List<int>>
{
    public async Task<List<int>> ExecuteQueryAsync(GetMessageIdListQuery query,
        CancellationToken cancellationToken)
    {
        var maxId = query.MaxMessageId;
        if (maxId == 0)
        {
            maxId = int.MaxValue;
        }

        return await (await CreateQueryAsync().ConfigureAwait(false))
                .Where(p =>
                    p.OwnerPeerId == query.OwnerPeerId && p.ToPeerId == query.ToPeerId && p.MessageId < maxId)
                .Select(p => p.MessageId)
                .Take(query.Limit)
                .ToListAsync(cancellationToken)
            ;
    }
}

public class
    GetMessageReadHistoryQueryHandler(IMyInMemoryReadStore<ReadingHistoryReadModel> store)
    : MyQueryHandler<ReadingHistoryReadModel>(store),
        IQueryHandler<GetReadingHistoryQuery, IReadOnlyCollection<long>>
{
    public async Task<IReadOnlyCollection<long>> ExecuteQueryAsync(GetReadingHistoryQuery query,
        CancellationToken cancellationToken)
    {
        return await (await CreateQueryAsync().ConfigureAwait(false))
                .Where(p => p.TargetPeerId == query.TargetPeerId && p.MessageId == query.MessageId)
                .OrderBy(p => p.Id)
                // todo:set limit
                .Take(200)
                .Select(p => p.TargetPeerId)
                .ToListAsync(cancellationToken)
            ;
    }
}

public class GetMessagesByMessageIdListQueryHandler(IMyInMemoryReadStore<MessageReadModel> store)
    : MyQueryHandler<MessageReadModel>(store),
        IQueryHandler<GetMessagesByMessageIdListQuery,
            IReadOnlyCollection<IMessageReadModel>>
{
    public async Task<IReadOnlyCollection<IMessageReadModel>> ExecuteQueryAsync(
        GetMessagesByMessageIdListQuery query,
        CancellationToken cancellationToken)
    {
        return await (await CreateQueryAsync().ConfigureAwait(false))
                .Where(p => query.MessageIdList.Contains(p.MessageId))
                .ToListAsync(cancellationToken)
            ;
    }
}

public class
    GetMessagesByUserQueryHandler(IMyInMemoryReadStore<MessageReadModel> store)
    : MyQueryHandler<MessageReadModel>(store), IQueryHandler<GetMessagesByUserIdQuery,
        IReadOnlyCollection<IMessageReadModel>>
{
    public async Task<IReadOnlyCollection<IMessageReadModel>> ExecuteQueryAsync(GetMessagesByUserIdQuery query,
        CancellationToken cancellationToken)
    {
        return await (await CreateQueryAsync().ConfigureAwait(false))
                .Where(p => p.OwnerPeerId == query.OwnerPeerId && p.ToPeerId == query.ToPeerId)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false)
            ;
    }
}

public class
    GetMessagesQueryHandler(IMyInMemoryReadStore<MessageReadModel> store) : MyQueryHandler<MessageReadModel>(store),
    IQueryHandler<GetMessagesQuery, IReadOnlyCollection<IMessageReadModel>>
{
    public async Task<IReadOnlyCollection<IMessageReadModel>> ExecuteQueryAsync(GetMessagesQuery query,
        CancellationToken cancellationToken)
    {
        return await (await CreateQueryAsync().ConfigureAwait(false))
                .Where(p => p.OwnerPeerId == query.OwnerPeerId)
                .WhereIf(query.Q?.Length > 2, p => p.Message.Contains(query.Q!))
                .WhereIf(
                    query.MessageType != MessageType.Unknown && query.MessageType != MessageType.Pinned,
                    p => p.MessageType == query.MessageType)
                .WhereIf(query.MessageType == MessageType.Pinned, p => p.Pinned)
                .WhereIf(query.MessageIdList?.Count > 0, p => query.MessageIdList!.Contains(p.MessageId))
                .WhereIf(query.ChannelHistoryMinId > 0, p => p.MessageId > query.ChannelHistoryMinId)
                .WhereIf(query.Offset?.LoadType == LoadType.Forward, p => p.MessageId > query.Offset!.FromId)
                .WhereIf(query.Offset?.MaxId > 0, p => p.MessageId < query.Offset!.MaxId)
                .WhereIf(query.Pts > 0, p => p.Pts > query.Pts)
                .WhereIf(query.Peer != null,
                    p => p.ToPeerType == query.Peer!.PeerType && p.ToPeerId == query.Peer.PeerId)
                .OrderByDescending(p => p.MessageId)
                .Take(query.Limit)
                .ToListAsync(cancellationToken)
            ;
    }
}

public class GetMessageViewsQueryHandler(IMyInMemoryReadStore<MessageReadModel> store)
    : MyQueryHandler<MessageReadModel>(store),
        IQueryHandler<GetMessageViewsQuery, IReadOnlyCollection<MessageView>>
{
    public async Task<IReadOnlyCollection<MessageView>> ExecuteQueryAsync(GetMessageViewsQuery query,
        CancellationToken cancellationToken)
    {
        return await (await CreateQueryAsync().ConfigureAwait(false))
                .Where(p => p.OwnerPeerId == query.ChannelId && query.MessageIdList.Contains(p.MessageId))
                .Select(p => new MessageView { MessageId = p.MessageId, Views = p.Views ?? 0 })
                .ToListAsync(cancellationToken)
            ;
    }
}

public class
    GetPeerNotifySettingsByIdQueryHandler(IMyInMemoryReadStore<PeerNotifySettingsReadModel> store)
    : MyQueryHandler<PeerNotifySettingsReadModel>(store),
        IQueryHandler<GetPeerNotifySettingsByIdQuery,
            IPeerNotifySettingsReadModel>
{
    public async Task<IPeerNotifySettingsReadModel> ExecuteQueryAsync(GetPeerNotifySettingsByIdQuery query,
        CancellationToken cancellationToken)
    {
        return await (await CreateQueryAsync().ConfigureAwait(false))
            .FirstOrDefaultAsync(p => p.Id == query.Id.Value, cancellationToken)
            .ConfigureAwait(false) ?? new PeerNotifySettingsReadModel();
    }
}

public class GetPeerNotifySettingsListQueryHandler(IMyInMemoryReadStore<PeerNotifySettingsReadModel> store)
    : MyQueryHandler<PeerNotifySettingsReadModel>(store),
        IQueryHandler<GetPeerNotifySettingsListQuery,
            IReadOnlyCollection<IPeerNotifySettingsReadModel>>
{
    public async Task<IReadOnlyCollection<IPeerNotifySettingsReadModel>> ExecuteQueryAsync(
        GetPeerNotifySettingsListQuery query,
        CancellationToken cancellationToken)
    {
        var peerIdList = query.PeerNotifySettingsIdList.Select(p => p.Value).ToList();

        return await (await CreateQueryAsync().ConfigureAwait(false))
                .Where(p => peerIdList.Contains(p.Id))
                .ToListAsync(cancellationToken)
            ;
    }
}

public class GetPtsByPeerIdQueryHandler(IMyInMemoryReadStore<PtsReadModel> store) : MyQueryHandler<PtsReadModel>(store),
    IQueryHandler<GetPtsByPeerIdQuery, IPtsReadModel?>
{
    public async Task<IPtsReadModel?> ExecuteQueryAsync(GetPtsByPeerIdQuery query,
        CancellationToken cancellationToken)
    {
        var ptsId = PtsId.Create(query.PeerId).Value;
        return await (await CreateQueryAsync().ConfigureAwait(false))
            .FirstOrDefaultAsync(p => p.Id == ptsId, cancellationToken);
    }
}

public class GetPtsByPermAuthKeyIdQueryHandler(IMyInMemoryReadStore<PtsForAuthKeyIdReadModel> store)
    : MyQueryHandler<PtsForAuthKeyIdReadModel>(store),
        IQueryHandler<GetPtsByPermAuthKeyIdQuery, IPtsForAuthKeyIdReadModel?>
{
    public async Task<IPtsForAuthKeyIdReadModel?> ExecuteQueryAsync(GetPtsByPermAuthKeyIdQuery query,
        CancellationToken cancellationToken)
    {
        var id = PtsId.Create(query.PeerId, query.PermAuthKeyId);
        return await (await CreateQueryAsync().ConfigureAwait(false))
                .FirstOrDefaultAsync(p => p.Id == id.Value, cancellationToken)
            ;
    }
}

public class
    GetPushUpdatesByPtsQueryHandler(IMyInMemoryReadStore<PushUpdatesReadModel> store)
    : MyQueryHandler<PushUpdatesReadModel>(store), IQueryHandler<GetPushUpdatesByPtsQuery,
        IReadOnlyCollection<IPushUpdatesReadModel>>
{
    public async Task<IReadOnlyCollection<IPushUpdatesReadModel>> ExecuteQueryAsync(GetPushUpdatesByPtsQuery query,
        CancellationToken cancellationToken)
    {
        return await (await CreateQueryAsync().ConfigureAwait(false))
                .Where(p => p.PeerId == query.PeerId && p.Pts > query.Pts)
                .Take(query.Limit)
                .ToListAsync(cancellationToken)
            ;
    }
}

public class GetPushUpdatesQueryHandler(IMyInMemoryReadStore<PushUpdatesReadModel> store)
    : MyQueryHandler<PushUpdatesReadModel>(store),
        IQueryHandler<GetPushUpdatesQuery, IReadOnlyCollection<IPushUpdatesReadModel>>
{
    public async Task<IReadOnlyCollection<IPushUpdatesReadModel>> ExecuteQueryAsync(GetPushUpdatesQuery query,
        CancellationToken cancellationToken)
    {
        return (await CreateQueryAsync().ConfigureAwait(false))
            .Where(p => p.PeerId == query.PeerId && p.Pts > query.MinPts)
            .Take(query.Limit)
            .ToList();
    }
}

public class
    GetReadHistoryMessageQueryHandler(IMyInMemoryReadStore<MessageReadModel> store)
    : MyQueryHandler<MessageReadModel>(store),
        IQueryHandler<GetReadHistoryMessageQuery, IMessageReadModel?>
{
    public async Task<IMessageReadModel?> ExecuteQueryAsync(GetReadHistoryMessageQuery query,
        CancellationToken cancellationToken)
    {
        return await (await CreateQueryAsync().ConfigureAwait(false))
                .SingleOrDefaultAsync(p =>
                        p.OwnerPeerId == query.OwnerPeerId && p.MessageId == query.MessageId &&
                        p.ToPeerId == query.ToPeerId,
                    cancellationToken)
            ;
    }
}

public class GetRpcResultByIdQueryHandler(IMyInMemoryReadStore<RpcResultReadModel> store)
    : MyQueryHandler<RpcResultReadModel>(store),
        IQueryHandler<GetRpcResultByIdQuery, IRpcResultReadModel?>
{
    public async Task<IRpcResultReadModel?> ExecuteQueryAsync(GetRpcResultByIdQuery query,
        CancellationToken cancellationToken)
    {
        return await (await CreateQueryAsync().ConfigureAwait(false))
            .FirstOrDefaultAsync(p => p.Id == query.Id, cancellationToken);
    }
}

public class GetUserByIdQueryHandler(IMyInMemoryReadStore<UserReadModel> store) : MyQueryHandler<UserReadModel>(store),
    IQueryHandler<GetUserByIdQuery, IUserReadModel?>
{
    public async Task<IUserReadModel?> ExecuteQueryAsync(GetUserByIdQuery query,
        CancellationToken cancellationToken)
    {
        return (await CreateQueryAsync().ConfigureAwait(false))
            .FirstOrDefault(p => p.Id == UserId.Create(query.UserId).Value)
            ;
    }
}

public class GetUserByPhoneNumberQueryHandler(IMyInMemoryReadStore<UserReadModel> store)
    : MyQueryHandler<UserReadModel>(store),
        IQueryHandler<GetUserByPhoneNumberQuery, IUserReadModel?>
{
    public async Task<IUserReadModel?> ExecuteQueryAsync(GetUserByPhoneNumberQuery query,
        CancellationToken cancellationToken)
    {
        return (await CreateQueryAsync().ConfigureAwait(false))
            .FirstOrDefault(p => p.PhoneNumber == query.PhoneNumber)
            ;
    }
}

public class GetUserNameByIdQueryHandler(IMyInMemoryReadStore<UserNameReadModel> store)
    : MyQueryHandler<UserNameReadModel>(store),
        IQueryHandler<GetUserNameByIdQuery, IUserNameReadModel?>
{
    public async Task<IUserNameReadModel?> ExecuteQueryAsync(GetUserNameByIdQuery query,
        CancellationToken cancellationToken)
    {
        return (await CreateQueryAsync().ConfigureAwait(false))
            .FirstOrDefault(p => p.Id == UserNameId.Create(query.UserName).Value)
            ;
    }
}

public class
    GetUserNameByNameQueryHandler(IMyInMemoryReadStore<UserNameReadModel> store)
    : IQueryHandler<GetUserNameByNameQuery, IUserNameReadModel?>
{
    private readonly IMyInMemoryReadStore<UserNameReadModel> _store = store;

    public async Task<IUserNameReadModel?> ExecuteQueryAsync(GetUserNameByNameQuery query,
        CancellationToken cancellationToken)
    {
        var db = await _store.AsQueryable(cancellationToken);
        return db
                .FirstOrDefault(p => p.Id == UserNameId.Create(query.Name).Value)
            ;
    }
}

public class
    GetUsersByUidListQueryHandler(IMyInMemoryReadStore<UserReadModel> store) : MyQueryHandler<UserReadModel>(store),
    IQueryHandler<GetUsersByUidListQuery, IReadOnlyList<IUserReadModel>>
{
    public async Task<IReadOnlyList<IUserReadModel>> ExecuteQueryAsync(GetUsersByUidListQuery query,
        CancellationToken cancellationToken)
    {
        if (query.UidList.Count == 0)
        {
            return new List<UserReadModel>();
        }

        return (await CreateQueryAsync().ConfigureAwait(false))
            .Where(p => query.UidList.Contains(p.UserId))
            .ToList();
    }
}

public class MyQueryHandler<TReadModel>(IMyInMemoryReadStore<TReadModel> store)
    where TReadModel : class, IReadModel
{
    private readonly IMyInMemoryReadStore<TReadModel> _store = store;

    public Task<IQueryable<TReadModel>> CreateQueryAsync()
    {
        return _store.AsQueryable();
    }
}

public class
    SearchUserByKeywordQueryHandler(IMyInMemoryReadStore<UserReadModel> store) : MyQueryHandler<UserReadModel>(store),
    IQueryHandler<SearchUserByKeywordQuery, IReadOnlyCollection<IUserReadModel>>
{
    public async Task<IReadOnlyCollection<IUserReadModel>> ExecuteQueryAsync(SearchUserByKeywordQuery query,
        CancellationToken cancellationToken)
    {
        var q = query.Keyword;
        if (!string.IsNullOrEmpty(q) && q.StartsWith("@"))
        {
            q = query.Keyword[1..];
        }

        return (await CreateQueryAsync().ConfigureAwait(false))
            .WhereIf(!string.IsNullOrEmpty(q),
                p => (p.UserName != null && p.UserName.StartsWith(q)) || p.FirstName.Contains(q))
            .OrderBy(p => p.UserName)
            .Take(50)
            .ToList();
    }
}

public class SearchUserNameQueryHandler(IMyInMemoryReadStore<UserNameReadModel> store)
    : IQueryHandler<SearchUserNameQuery, IReadOnlyCollection<IUserNameReadModel>>
{
    private readonly IMyInMemoryReadStore<UserNameReadModel> _store = store;

    public async Task<IReadOnlyCollection<IUserNameReadModel>> ExecuteQueryAsync(SearchUserNameQuery query,
        CancellationToken cancellationToken)
    {
        var db = await _store.AsQueryable(cancellationToken);
        return db
                .Where(p => p.UserName.StartsWith(query.Keyword))
                //.OrderBy(p => p.Id)
                .Take(50)
                .ToList()
            ;
    }
}

public class GetLatestAppCodeQueryHandler(IMyInMemoryReadStore<AppCodeReadModel> store)
    : MyQueryHandler<AppCodeReadModel>(store),
        IQueryHandler<GetLatestAppCodeQuery, IAppCodeReadModel>
{
    public async Task<IAppCodeReadModel> ExecuteQueryAsync(GetLatestAppCodeQuery query,
        CancellationToken cancellationToken)
    {
        var item = await (await CreateQueryAsync().ConfigureAwait(false)).FirstOrDefaultAsync(p =>
                        p.PhoneNumber == query.PhoneNumber && p.PhoneCodeHash == query.PhoneCodeHash,
                    cancellationToken)
                .ConfigureAwait(false)
            ;
        return item ?? new AppCodeReadModel();
    }
}
