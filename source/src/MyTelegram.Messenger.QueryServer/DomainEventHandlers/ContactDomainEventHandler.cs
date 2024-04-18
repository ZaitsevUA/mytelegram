using MyTelegram.Messenger.DomainEventHandlers;
using MyTelegram.Messenger.Services.Caching;
using MyTelegram.Messenger.Services.Interfaces;
using MyTelegram.Messenger.TLObjectConverters.Interfaces;
using MyTelegram.Schema.Contacts;
using MyTelegram.Services.TLObjectConverters;
using TPeerSettings = MyTelegram.Schema.TPeerSettings;

namespace MyTelegram.Messenger.QueryServer.DomainEventHandlers;

public class ContactDomainEventHandler(
    IObjectMessageSender objectMessageSender,
    ICommandBus commandBus,
    IIdGenerator idGenerator,
    IAckCacheService ackCacheService,
    IResponseCacheAppService responseCacheAppService,
    IQueryProcessor queryProcessor,
    ILayeredService<IUserConverter> layeredUserService,
    IPhotoAppService photoAppService,
    ILayeredService<IPhotoConverter> layeredPhotoService,
    IPrivacyAppService privacyAppService,
    IPeerSettingsAppService peerSettingsAppService)
    : DomainEventHandlerBase(objectMessageSender,
            commandBus,
            idGenerator,
            ackCacheService,
            responseCacheAppService),
        ISubscribeSynchronousTo<ContactAggregate, ContactId, ContactAddedEvent>,
        ISubscribeSynchronousTo<ContactAggregate, ContactId, ContactDeletedEvent>,
        ISubscribeSynchronousTo<ImportContactsSaga, ImportContactsSagaId, ImportContactsCompletedEvent>,
        ISubscribeSynchronousTo<ContactAggregate, ContactId, ContactProfilePhotoChangedEvent>
{
    private readonly IPeerSettingsAppService _peerSettingsAppService = peerSettingsAppService;

    public async Task HandleAsync(IDomainEvent<ContactAggregate, ContactId, ContactAddedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        var targetUser =
            await queryProcessor.ProcessAsync(new GetUserByIdQuery(domainEvent.AggregateEvent.TargetUserId), default);
        var photos = await photoAppService.GetPhotosAsync(targetUser);
        var privacies = await privacyAppService.GetPrivacyListAsync(domainEvent.AggregateEvent.TargetUserId);
        var tUser = layeredUserService.GetConverter(domainEvent.AggregateEvent.RequestInfo.Layer)
            .ToUser(domainEvent.AggregateEvent.SelfUserId, targetUser!, photos, null, privacies);
        tUser.Contact = true;
        tUser.FirstName = domainEvent.AggregateEvent.FirstName;
        tUser.LastName = domainEvent.AggregateEvent.LastName;

        var r = new TUpdates
        {
            Chats = new TVector<IChat>(),
            Date = DateTime.UtcNow.ToTimestamp(),
            Seq = 0,
            Updates = new TVector<IUpdate>(new TUpdatePeerSettings
            {
                Peer = new TPeerUser { UserId = domainEvent.AggregateEvent.TargetUserId },
                Settings = new TPeerSettings { NeedContactsException = false }
            }),
            Users = new TVector<IUser>(tUser)
        };
        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.RequestInfo, r);
    }

    public async Task HandleAsync(IDomainEvent<ContactAggregate, ContactId, ContactDeletedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        var targetUser =
            await queryProcessor.ProcessAsync(new GetUserByIdQuery(domainEvent.AggregateEvent.TargetUid), default);
        var photos = await photoAppService.GetPhotosAsync(targetUser);
        var privacies = await privacyAppService.GetPrivacyListAsync(domainEvent.AggregateEvent.TargetUid);
        var tUser = layeredUserService.GetConverter(domainEvent.AggregateEvent.RequestInfo.Layer)
            .ToUser(domainEvent.AggregateEvent.RequestInfo.UserId, targetUser!, photos, null, privacies);

        var r = new TUpdates
        {
            Chats = new TVector<IChat>(),
            Date = DateTime.UtcNow.ToTimestamp(),
            Seq = 0,
            Updates = new TVector<IUpdate>(new TUpdatePeerSettings
            {
                Peer = new TPeerUser { UserId = domainEvent.AggregateEvent.TargetUid },
                Settings = new TPeerSettings()
            }),
            Users = new TVector<IUser>(tUser)
        };
        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.RequestInfo, r);
    }

    public async Task HandleAsync(
        IDomainEvent<ImportContactsSaga, ImportContactsSagaId, ImportContactsCompletedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        var importedContacts = domainEvent.AggregateEvent.PhoneContacts
            .Where(p => p.UserId > 0)
            .Select(p => new TImportedContact { ClientId = p.ClientId, UserId = p.UserId }).ToList();
        var userIds = importedContacts.Select(p => p.UserId).ToList();
        var userReadModels =
            await queryProcessor.ProcessAsync(
                new GetUsersByUidListQuery(userIds), cancellationToken);
        var photoReadModels = await photoAppService.GetPhotosAsync(userReadModels);
        var privacyReadModels = await privacyAppService.GetPrivacyListAsync(userIds);
        var contactReadModels =
            await queryProcessor.ProcessAsync(new GetContactListQuery(domainEvent.AggregateEvent.RequestInfo.UserId,
                userIds), cancellationToken);

        var userList = layeredUserService.GetConverter(domainEvent.AggregateEvent.RequestInfo.Layer).ToUserList(
            domainEvent.AggregateEvent.RequestInfo.UserId, userReadModels, photoReadModels, contactReadModels,
            privacyReadModels);
        foreach (var layeredUser in userList)
        {
            layeredUser.Contact = true;
        }

        var r = new TImportedContacts
        {
            Imported = new TVector<IImportedContact>(importedContacts),
            PopularInvites = new TVector<IPopularContact>(),
            RetryContacts = new TVector<long>(),
            Users = new TVector<IUser>(userList)
        };
        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.RequestInfo, r);
        var updates = new List<IUpdate>();
        foreach (var userId in userIds)
        {
            var updatePeerSettings = new TUpdatePeerSettings
            {
                Peer = new TPeerUser
                {
                    UserId = userId,
                },
                Settings = new TPeerSettings()
            };
            updates.Add(updatePeerSettings);
        }
        await SendMessageToPeerAsync(domainEvent.AggregateEvent.RequestInfo.UserId.ToUserPeer(), new TUpdates
        {
            Updates = new TVector<IUpdate>(updates),
            Users = new(userList),
            Chats = new(),
            Date = DateTime.Now.ToTimestamp()
        });
    }

    public async Task HandleAsync(IDomainEvent<ContactAggregate, ContactId, ContactProfilePhotoChangedEvent> domainEvent, CancellationToken cancellationToken)
    {
        var userReadModel =
            await queryProcessor.ProcessAsync(new GetUserByIdQuery(domainEvent.AggregateEvent.TargetUserId), cancellationToken);
        var privacyList = await privacyAppService.GetPrivacyListAsync(domainEvent.AggregateEvent.TargetUserId);
        var photoReadModels = await photoAppService.GetPhotosAsync(userReadModel);
        var photoReadModel = await photoAppService.GetPhotoAsync(domainEvent.AggregateEvent.PhotoId);
        var contactReadModel =
            await queryProcessor.ProcessAsync(new GetContactQuery(domainEvent.AggregateEvent.RequestInfo.UserId, domainEvent.AggregateEvent.TargetUserId), cancellationToken);

        var user = layeredUserService.GetConverter(domainEvent.AggregateEvent.RequestInfo.Layer)
            .ToUser(domainEvent.AggregateEvent.SelfUserId, userReadModel!, photoReadModels, contactReadModel, privacyList);
        var r = new MyTelegram.Schema.Photos.TPhoto
        {
            Users = new(user),
            Photo = layeredPhotoService.GetConverter(domainEvent.AggregateEvent.RequestInfo.Layer).ToPhoto(photoReadModel)
        };

        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.RequestInfo, r);
    }
}
