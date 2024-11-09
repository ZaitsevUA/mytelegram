// ReSharper disable All

namespace MyTelegram.Handlers.Contacts;

///<summary>
/// Returns the current user's contact list.
/// See <a href="https://corefork.telegram.org/method/contacts.getContacts" />
///</summary>
internal sealed class GetContactsHandler(
    IQueryProcessor queryProcessor,
    ILayeredService<IUserConverter> layeredUserService,
    IPhotoAppService photoAppService,
    IHashCalculator hashCalculator,
    IPrivacyAppService privacyAppService)
    : RpcResultObjectHandler<MyTelegram.Schema.Contacts.RequestGetContacts, MyTelegram.Schema.Contacts.IContacts>,
        Contacts.IGetContactsHandler
{
    protected override async Task<IContacts> HandleCoreAsync(IRequestInput input,
        RequestGetContacts obj)
    {
        var contactReadModels = await queryProcessor
            .ProcessAsync(new GetContactsByUserIdQuery(input.UserId), CancellationToken.None);
        var userIdList = contactReadModels.Select(p => p.TargetUserId).ToList();
        userIdList.Add(input.UserId);
        var userReadModels = await queryProcessor
            .ProcessAsync(new GetUsersByUserIdListQuery(userIdList));
        var privacies = await privacyAppService.GetPrivacyListAsync(userIdList);
        var photos = await photoAppService.GetPhotosAsync(userReadModels, contactReadModels);
        var userList = layeredUserService.GetConverter(input.Layer).ToUserList(input.UserId, userReadModels, photos, contactReadModels, privacies);

        var validUserIds = new List<long>();
        foreach (var user in userList)
        {
            user.Contact = true;
            validUserIds.Add(user.Id);
        }

        var hash = hashCalculator.GetHash(userIdList);

        if (obj.Hash != 0 && obj.Hash == hash)
        {
            return new TContactsNotModified();
        }

        var r = new TContacts
        {
            Contacts =
                new TVector<IContact>(contactReadModels.Where(p => validUserIds.Contains(p.TargetUserId)).Select(p =>
                    new TContact { UserId = p.TargetUserId, Mutual = false })),
            Users = new TVector<IUser>(userList),
            SavedCount = contactReadModels.Count,
        };

        return r;
        //return Task.FromResult<IContacts>(r);
    }
}
