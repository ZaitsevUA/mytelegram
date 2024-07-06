// ReSharper disable All

namespace MyTelegram.Handlers.Channels;

///<summary>
/// Get the participants of a <a href="https://corefork.telegram.org/api/channel">supergroup/channel</a>
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 406 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 403 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// See <a href="https://corefork.telegram.org/method/channels.getParticipants" />
///</summary>
internal sealed class GetParticipantsHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestGetParticipants, MyTelegram.Schema.Channels.IChannelParticipants>,
    Channels.IGetParticipantsHandler
{
    private readonly IQueryProcessor _queryProcessor;
    private readonly ILayeredService<IChatConverter> _layeredService;
    private readonly ILayeredService<IUserConverter> _layeredUserService;
    private readonly IAccessHashHelper _accessHashHelper;
    private readonly IPhotoAppService _photoAppService;
    private readonly IPrivacyAppService _privacyAppService;
    public GetParticipantsHandler(IQueryProcessor queryProcessor,
        ILayeredService<IChatConverter> layeredService,
        IAccessHashHelper accessHashHelper,
        ILayeredService<IUserConverter> layeredUserService,
        IPhotoAppService photoAppService,
        IPrivacyAppService privacyAppService //,
    )
    {
        _queryProcessor = queryProcessor;
        _layeredService = layeredService;
        _accessHashHelper = accessHashHelper;
        _layeredUserService = layeredUserService;
        _photoAppService = photoAppService;
        _privacyAppService = privacyAppService;
    }

    protected override async Task<MyTelegram.Schema.Channels.IChannelParticipants> HandleCoreAsync(IRequestInput input,
        RequestGetParticipants obj)
    {
        if (obj.Channel is TInputChannel inputChannel)
        {
            await _accessHashHelper.CheckAccessHashAsync(inputChannel.ChannelId, inputChannel.AccessHash);
            var channelReadModel = await _queryProcessor
                .ProcessAsync(new GetChannelByIdQuery(inputChannel.ChannelId));

            channelReadModel.ThrowExceptionIfChannelDeleted();

            var joinedChannelIdList = await _queryProcessor.ProcessAsync(new GetJoinedChannelIdListQuery(input.UserId,
                    new List<long> { inputChannel.ChannelId }));


            // Only channel creator or admin can access channel participants
            //if (channelReadModel.Broadcast)
            //{
            //    if (channelReadModel.CreatorId != input.UserId &&
            //        channelReadModel.AdminList.FirstOrDefault(p => p.UserId == input.UserId) == null)
            //    {
            //        _logger.LogWarning("None admin get channel participants,userId={UserId},channelId={ChannelId},filter={Filter}",
            //            input.UserId,
            //            inputChannel.ChannelId,
            //            obj.Filter
            //            );
            //        return new TChannelParticipants
            //        {
            //            Chats = new TVector<IChat>(),
            //            Count = 0,
            //            Participants = new TVector<IChannelParticipant>(),
            //            Users = new TVector<IUser>()
            //        };
            //    }
            //}

            if (joinedChannelIdList.Count == 0 && channelReadModel!.Broadcast)
            {
                return new TChannelParticipants
                {
                    Chats = new TVector<IChat>(),
                    Count = 0,
                    Participants = new(),
                    Users = new TVector<IUser>()
                };
                //ThrowHelper.ThrowUserFriendlyException("CHANNEL_PRIVATE");
            }

            void CheckAdminPermission(IChannelReadModel channel,
                long userId)
            {
                if (channelReadModel!.Broadcast)
                {
                    if (channel.CreatorId != userId &&
                        channel.AdminList?.FirstOrDefault(p => p.UserId == userId) == null)
                    {
                        RpcErrors.RpcErrors403.ChatAdminRequired.ThrowRpcError();
                    }
                }
            }

            var memberUidList = new List<long>();
            var forceNotLeft = false;
            IReadOnlyCollection<IChatAdminReadModel>? chatAdminReadModels = null;
            IQuery<IReadOnlyCollection<IChannelMemberReadModel>>? query = null;
            switch (obj.Filter)
            {
                case TChannelParticipantsAdmins channelParticipantsAdmins:
                    //CheckAdminPermission(channelReadModel, input.UserId);
                    chatAdminReadModels = await _queryProcessor.ProcessAsync(
                        new GetChatAdminListByChannelIdQuery(inputChannel.ChannelId, obj.Offset, obj.Limit));

                    break;
                case TChannelParticipantsBots:
                    return new TChannelParticipants
                    {
                        Participants = new(),
                        Users = new TVector<IUser>(),
                        Chats = new TVector<IChat>()
                    };
                case TChannelParticipantsKicked:
                    CheckAdminPermission(channelReadModel, input.UserId);
                    forceNotLeft = true;
                    query = new GetKickedChannelMembersQuery(inputChannel.ChannelId, obj.Offset, obj.Limit);
                    break;
                default:
                    query = new GetChannelMembersByChannelIdQuery(inputChannel.ChannelId,
                        new List<long>(),
                        false,
                        obj.Offset,
                        obj.Limit);
                    break;
            }

            if (joinedChannelIdList.Contains(channelReadModel.ChannelId))
            {
                forceNotLeft = true;
            }

            var channelMemberReadModels = query == null ? Array.Empty<IChannelMemberReadModel>() : await _queryProcessor
                .ProcessAsync(query);

            var userIdList = channelMemberReadModels.Select(p => p.UserId).ToList();
            var selfChannelMember = channelMemberReadModels.FirstOrDefault(p => p.UserId == input.UserId);
            if (selfChannelMember != null)
            {
                userIdList.Add(selfChannelMember.InviterId);
            }

            var userReadModels = await _queryProcessor
                .ProcessAsync(new GetUsersByUidListQuery(userIdList), default);
            var contactReadModels = await _queryProcessor
                .ProcessAsync(new GetContactListQuery(input.UserId, userIdList));
            var privacies = await _privacyAppService.GetPrivacyListAsync(userIdList);

            var photos = await _photoAppService.GetPhotosAsync(userReadModels, contactReadModels);
            var users = _layeredUserService.GetConverter(input.Layer)
                .ToUserList(input.UserId, userReadModels, photos, contactReadModels, privacies);
            var chatPhoto = await _photoAppService.GetPhotoAsync(channelReadModel.PhotoId);

            var creatorId = channelReadModel.CreatorId;
            if (channelReadModel.Broadcast || (channelReadModel.HasLink && input.UserId != creatorId))
            {
                if (chatAdminReadModels?.Any() ?? false)
                {
                    var newAdminList = chatAdminReadModels.ToList();
                    newAdminList.RemoveAll(p => p.UserId == creatorId);
                    chatAdminReadModels = newAdminList;
                }

                if (channelMemberReadModels.Any())
                {
                    var newChannelMemberReadModels = channelMemberReadModels.ToList();
                    newChannelMemberReadModels.RemoveAll(p => p.UserId == creatorId);
                    channelMemberReadModels = newChannelMemberReadModels;
                }

                if (users.Any())
                {
                    var newUsers = users.ToList();
                    newUsers.RemoveAll(p => p.Id == creatorId);
                    users = newUsers;
                }
            }

            if ((!chatAdminReadModels?.Any() ?? false) && !channelMemberReadModels.Any())
            {
                return new TChannelParticipants
                {
                    Chats = [],
                    Participants = [],
                    Users = [],
                };
            }

            return _layeredService.GetConverter(input.Layer).ToChannelParticipants(
                input.UserId,
                channelReadModel,
                chatPhoto,
                chatAdminReadModels,
                channelMemberReadModels,
                //userReadModels,
                users,
                DeviceType.Unknown,
                forceNotLeft);
        }

        throw new NotImplementedException();
    }
}
