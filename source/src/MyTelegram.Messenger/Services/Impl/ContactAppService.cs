namespace MyTelegram.Messenger.Services.Impl;

public class ContactAppService(
    IQueryProcessor queryProcessor,
    IPhotoAppService photoAppService,
    IOptionsMonitor<MyTelegramMessengerServerOptions> options,
    IPrivacyAppService privacyAppService)
    : BaseAppService, IContactAppService
{
    public async Task<SearchContactOutput> SearchAsync(long selfUserId,
        string keyword)
    {
        if (keyword?.Length > 0)
        {
            var searchKeyword = keyword;
            if (searchKeyword.StartsWith("@"))
            {
                searchKeyword = keyword[1..];
            }

            var contactReadModels = await queryProcessor
                .ProcessAsync(new SearchContactQuery(selfUserId, searchKeyword));
            var userNameReadModels = await queryProcessor
                .ProcessAsync(new SearchUserNameQuery(searchKeyword));

            var channelIdList = userNameReadModels.Where(p => p.PeerType == PeerType.Channel).Select(p => p.PeerId)
                .ToList();

            var userIdList = contactReadModels.Select(p => p.TargetUserId).ToList();
            userIdList.AddRange(userNameReadModels.Where(p => p.PeerType == PeerType.User).Select(p => p.PeerId));

            var userReadModels = await queryProcessor
                .ProcessAsync(new GetUsersByUserIdListQuery(userIdList));
            var allUserReadModels = userReadModels.ToList();

            if (options.CurrentValue.EnableSearchNonContacts)
            {
                var userReadModels2 = await queryProcessor.ProcessAsync(new SearchUserByKeywordQuery(keyword, 20));
                allUserReadModels.AddRange(userReadModels2);
            }

            var channelReadModels = await queryProcessor
                .ProcessAsync(new GetChannelByChannelIdListQuery(channelIdList));

            var photos = await photoAppService.GetPhotosAsync(allUserReadModels, contactReadModels);

            var privacyReadModels = await queryProcessor.ProcessAsync(new GetPrivacyListQuery(allUserReadModels.Select(p => p.UserId).ToList(), new List<PrivacyType>
            {
                PrivacyType.PhoneNumber,
                PrivacyType.PhoneCall,
                PrivacyType.VoiceMessages,
                PrivacyType.ProfilePhoto,
                PrivacyType.StatusTimestamp,
                PrivacyType.Birthday,
                PrivacyType.About
            }));

            return new SearchContactOutput(selfUserId,
                allUserReadModels,
                photos,
                contactReadModels,
                [],
                channelReadModels,
                privacyReadModels,
                []
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