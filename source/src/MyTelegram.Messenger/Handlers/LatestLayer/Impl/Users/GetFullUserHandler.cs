// ReSharper disable All

namespace MyTelegram.Handlers.Users;

///<summary>
/// Returns extended user info by ID.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 400 USERNAME_OCCUPIED The provided username is already occupied.
/// 500 USERNAME_UNSYNCHRONIZED &nbsp;
/// 400 USER_ID_INVALID The provided user ID is invalid.
/// See <a href="https://corefork.telegram.org/method/users.getFullUser" />
///</summary>
internal sealed class GetFullUserHandler(
    IPeerHelper peerHelper,
    IQueryProcessor queryProcessor,
    IBlockCacheAppService blockCacheAppService,
    ILayeredService<IUserConverter> layeredUserService,
    IAccessHashHelper accessHashHelper,
    IPeerSettingsAppService peerSettingsAppService,
    IPhotoAppService photoAppService,
    IPrivacyAppService privacyAppService)
    : RpcResultObjectHandler<MyTelegram.Schema.Users.RequestGetFullUser,
            MyTelegram.Schema.Users.IUserFull>,
        Users.IGetFullUserHandler
{
    //private readonly ITlUserConverter _userConverter;

    protected override async Task<MyTelegram.Schema.Users.IUserFull> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Users.RequestGetFullUser obj)
    {
        await accessHashHelper.CheckAccessHashAsync(obj.Id);

        var userId = input.UserId;
        //var userId = await GetUserIdAsync(input);
        var targetPeer = peerHelper.GetPeer(obj.Id, userId);

        //var targetUserId = UserId.Create(targetPeer.PeerId);
        var user = await queryProcessor.ProcessAsync(new GetUserByIdQuery(targetPeer.PeerId));
        if (user == null)
        {
            //ThrowHelper.ThrowUserFriendlyException("USER_ID_INVALID");
            RpcErrors.RpcErrors400.UserIdInvalid.ThrowRpcError();
        }

        var contactReadModels =
            await queryProcessor.ProcessAsync(
                new GetContactListBySelfIdAndTargetUserIdQuery(input.UserId, targetPeer.PeerId));

        //var contactReadModel = await _queryProcessor
        //    .ProcessAsync(new GetContactQuery(input.UserId, targetPeer.PeerId));
        var contactType = ContactType.None;

        var contactReadModel = contactReadModels.FirstOrDefault(p =>
            p.SelfUserId == input.UserId && p.TargetUserId == targetPeer.PeerId);
        var contactReadModel2 =
            contactReadModels.FirstOrDefault(p =>
                p.SelfUserId == targetPeer.PeerId && p.TargetUserId == input.UserId);

        if (contactReadModel2 != null)
        {
            contactType = ContactType.Unilateral;
        }

        if (contactReadModel != null && contactReadModel2 != null)
        {
            contactType = ContactType.Mutual;
        }

        var privacies = await privacyAppService.GetPrivacyListAsync(user!.UserId);


        IBotReadModel? bot = null;
        IDocumentReadModel? botDescriptionDocumentReadModel = null;
        IPhotoReadModel? botDescriptionPhotoReadModel = null;

        var peerNotifySettingsId = PeerNotifySettingsId.Create(userId, targetPeer.PeerType, targetPeer.PeerId);

        var peerNotifySettings =
            await queryProcessor.ProcessAsync(new GetPeerNotifySettingsByIdQuery(peerNotifySettingsId));
        var peerSettings = await peerSettingsAppService.GetPeerSettingsAsync(input.UserId, targetPeer.PeerId);
        var photos = await photoAppService.GetPhotosAsync(user, contactReadModel);

        return await layeredUserService.GetConverter(input.Layer).ToUserFullAsync(
            userId,
            user,
            peerNotifySettings,
            peerSettings,
            photos,
            bot,
            botDescriptionDocumentReadModel,
            botDescriptionPhotoReadModel,
            contactReadModel,
            contactType,
            privacies);
    }
}
