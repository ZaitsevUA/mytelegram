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
internal sealed class GetParticipantsHandler(
    IQueryProcessor queryProcessor,
    ILayeredService<IChatConverter> layeredService,
    IAccessHashHelper accessHashHelper,
    ILayeredService<IUserConverter> layeredUserService,
    IUserAppService userAppService,
    IChannelAppService channelAppService,
    IPhotoAppService photoAppService,
    IPrivacyAppService privacyAppService)
    : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestGetParticipants,
            MyTelegram.Schema.Channels.IChannelParticipants>,
        Channels.IGetParticipantsHandler
{
    //,

    protected override async Task<MyTelegram.Schema.Channels.IChannelParticipants> HandleCoreAsync(IRequestInput input,
        RequestGetParticipants obj)
    {
        if (obj.Channel is TInputChannel inputChannel)
        {
            await accessHashHelper.CheckAccessHashAsync(inputChannel.ChannelId, inputChannel.AccessHash);
            var channelReadModel = await channelAppService.GetAsync(inputChannel.ChannelId);

            channelReadModel.ThrowExceptionIfChannelDeleted();

            var joinedChannelIdList = await queryProcessor.ProcessAsync(new GetJoinedChannelIdListQuery(input.UserId,
                    new List<long> { inputChannel.ChannelId }));

            if (joinedChannelIdList.Count == 0 && channelReadModel!.Broadcast)
            {
                return new TChannelParticipants
                {
                    Chats = new TVector<IChat>(),
                    Count = 0,
                    Participants = new(),
                    Users = new TVector<IUser>()
                };
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
                    chatAdminReadModels = await queryProcessor.ProcessAsync(
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
                    CheckAdminPermission(channelReadModel!, input.UserId);
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

            if (joinedChannelIdList.Contains(channelReadModel!.ChannelId))
            {
                forceNotLeft = true;
            }

            var channelMemberReadModels = query == null ? Array.Empty<IChannelMemberReadModel>() : await queryProcessor
                .ProcessAsync(query);

            var userIdList = channelMemberReadModels.Select(p => p.UserId).ToList();
            var selfChannelMember = channelMemberReadModels.FirstOrDefault(p => p.UserId == input.UserId);
            if (selfChannelMember != null)
            {
                userIdList.Add(selfChannelMember.InviterId);
            }

            var userReadModels = await userAppService.GetListAsync(userIdList);
            var contactReadModels = await queryProcessor
                .ProcessAsync(new GetContactListQuery(input.UserId, userIdList));
            var privacies = await privacyAppService.GetPrivacyListAsync(userIdList);

            var photos = await photoAppService.GetPhotosAsync(userReadModels, contactReadModels);
            var users = layeredUserService.GetConverter(input.Layer)
                .ToUserList(input.UserId, userReadModels, photos, contactReadModels, privacies);
            var chatPhoto = await photoAppService.GetAsync(channelReadModel.PhotoId);

            var creatorId = channelReadModel.CreatorId;
            if (channelReadModel.Broadcast || (channelReadModel.HasLink && input.UserId != creatorId))
            {
                if (chatAdminReadModels?.Count > 0)
                {
                    var newAdminList = chatAdminReadModels.ToList();
                    newAdminList.RemoveAll(p => p.UserId == creatorId);
                    chatAdminReadModels = newAdminList;
                }

                if (channelMemberReadModels.Count > 0)
                {
                    var newChannelMemberReadModels = channelMemberReadModels.ToList();
                    newChannelMemberReadModels.RemoveAll(p => p.UserId == creatorId);
                    channelMemberReadModels = newChannelMemberReadModels;
                }

                if (users.Count > 0)
                {
                    var newUsers = users.ToList();
                    newUsers.RemoveAll(p => p.Id == creatorId);
                    users = newUsers;
                }
            }

            if ((chatAdminReadModels == null || chatAdminReadModels.Count == 0) && channelMemberReadModels.Count == 0)
            {
                return new TChannelParticipants
                {
                    Chats = [],
                    Participants = [],
                    Users = [],
                };
            }

            return layeredService.GetConverter(input.Layer).ToChannelParticipants(
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
