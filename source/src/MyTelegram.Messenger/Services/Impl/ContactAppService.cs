namespace MyTelegram.Messenger.Services.Impl;

public class ContactAppService(
    IQueryProcessor queryProcessor,
    IPhotoAppService photoAppService,
    IPrivacyAppService privacyAppService)
    : BaseAppService, IContactAppService
{
    public async Task<SearchContactOutput> SearchAsync(long selfUserId,
        string keyword)
    {
        if (keyword?.Length > 0)
        {
            var searchKey = keyword;
            if (searchKey.StartsWith("@"))
            {
                searchKey = keyword[1..];
            }

            var userList =
                await queryProcessor.ProcessAsync(new SearchUserByKeywordQuery(keyword, 20),
                    CancellationToken.None);

            var contactReadModels = await queryProcessor
                .ProcessAsync(new SearchContactQuery(selfUserId, searchKey));

            var userNameReadModel = await queryProcessor
                .ProcessAsync(new SearchUserNameQuery(searchKey));

            var channelIdList = userNameReadModel.Where(p => p.PeerType == PeerType.Channel).Select(p => p.PeerId)
                .ToList();

            var userIdList = contactReadModels.Select(p => p.TargetUserId).ToList();

            var userList2 = await queryProcessor
                .ProcessAsync(new GetUsersByUidListQuery(userIdList));
            var allUserList = userList.ToList();
            allUserList.AddRange(userList2);
            var channelList = await queryProcessor
                .ProcessAsync(new GetChannelByChannelIdListQuery(channelIdList));

            var photos = await photoAppService.GetPhotosAsync(allUserList, contactReadModels);

            var privacyList = await privacyAppService.GetPrivacyListAsync(allUserList.Select(p => p.UserId).ToList());

            return new SearchContactOutput(selfUserId,
                allUserList,
                photos,
                contactReadModels,
                Array.Empty<IChannelReadModel>(),
                channelList,
                privacyList,
                Array.Empty<IChannelMemberReadModel>()
                );
        }

        return new SearchContactOutput(selfUserId,
            new List<IUserReadModel>(),
            new List<IPhotoReadModel>(),
            new List<IContactReadModel>(),
            new List<IChannelReadModel>(),
            new List<IChannelReadModel>(),
            new List<IPrivacyReadModel>(),
            new List<IChannelMemberReadModel>());
    }
}