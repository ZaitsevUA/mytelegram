using MyTelegram.Messenger.Services.Caching;
using MyTelegram.Messenger.Services.Interfaces;
using MyTelegram.Messenger.TLObjectConverters.Interfaces;
using MyTelegram.Services.TLObjectConverters;

namespace MyTelegram.Messenger.QueryServer.DomainEventHandlers;

public class UserDomainEventHandler(
    IObjectMessageSender objectMessageSender,
    ICommandBus commandBus,
    IIdGenerator idGenerator,
    IAckCacheService ackCacheService,
    IResponseCacheAppService responseCacheAppService,
    IEventBus eventBus,
    IQueryProcessor queryProcessor,
    ILogger<UserDomainEventHandler> logger,
    ILayeredService<IUserConverter> layeredUserService,
    ILayeredService<IAuthorizationConverter> layeredAuthorizationService,
    ILayeredService<IPhotoConverter> layeredPhotoService,
    IPhotoAppService photoAppService)
    : DomainEventHandlerBase(objectMessageSender,
            commandBus,
            idGenerator,
            ackCacheService,
            responseCacheAppService),
        ISubscribeSynchronousTo<UserAggregate, UserId, UserCreatedEvent>,
        ISubscribeSynchronousTo<UserAggregate, UserId, UserProfileUpdatedEvent>,
        ISubscribeSynchronousTo<UserAggregate, UserId, UserNameUpdatedEvent>,
        ISubscribeSynchronousTo<UserAggregate, UserId, UserProfilePhotoChangedEvent>,
        ISubscribeSynchronousTo<UserAggregate, UserId, UserProfilePhotoUploadedEvent>
{
    //private readonly ITlAuthorizationConverter _authorizationConverter;
    //private readonly ITlUserConverter _userConverter;
    private readonly ILayeredService<IPhotoConverter> _layeredPhotoService = layeredPhotoService;


    public async Task HandleAsync(IDomainEvent<UserAggregate, UserId, UserCreatedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        logger.LogInformation(
            "User created successfully, userId: {UserId}  phoneNumber: {PhoneNumber} firstName: {FirstName} lastName: {LastName}",
            domainEvent.AggregateEvent.UserId,
            domainEvent.AggregateEvent.PhoneNumber,
            domainEvent.AggregateEvent.FirstName,
            domainEvent.AggregateEvent.LastName
        );

        await eventBus.PublishAsync(new UserSignUpSuccessIntegrationEvent(domainEvent.AggregateEvent.RequestInfo.AuthKeyId,
            domainEvent.AggregateEvent.RequestInfo.PermAuthKeyId,
            domainEvent.AggregateEvent.UserId));
        var user = layeredUserService.GetConverter(domainEvent.AggregateEvent.RequestInfo.Layer)
            .ToUser(domainEvent.AggregateEvent);
        var r = layeredAuthorizationService.GetConverter(domainEvent.AggregateEvent.RequestInfo.Layer).CreateAuthorization(user);
        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.RequestInfo,
            r,
            domainEvent.AggregateEvent.UserId);
    }

    public async Task HandleAsync(IDomainEvent<UserAggregate, UserId, UserNameUpdatedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        var user = await queryProcessor.ProcessAsync(
            new GetUserByIdQuery(domainEvent.AggregateEvent.RequestInfo.UserId), cancellationToken);
        var photos = await photoAppService.GetPhotosAsync(user);
        var r = layeredUserService.GetConverter(domainEvent.AggregateEvent.RequestInfo.Layer)
            .ToUser(domainEvent.AggregateEvent.RequestInfo.UserId, user!, photos);

        //var r = _layeredUserService.GetConverter(domainEvent.AggregateEvent.RequestInfo.Layer).ToUser(domainEvent.AggregateEvent);
        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.RequestInfo, r);
    }

    public async Task HandleAsync(IDomainEvent<UserAggregate, UserId, UserProfilePhotoChangedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        var photo = await photoAppService.GetPhotoAsync(domainEvent.AggregateEvent.PhotoId);
        var userReadModel = await queryProcessor.ProcessAsync(new GetUserByIdQuery(domainEvent.AggregateEvent.UserId), cancellationToken);
        var r = layeredUserService.GetConverter(domainEvent.AggregateEvent.RequestInfo.Layer).ToUserPhoto(userReadModel!, photo);

        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.RequestInfo, r);
    }

    public async Task HandleAsync(IDomainEvent<UserAggregate, UserId, UserProfilePhotoUploadedEvent> domainEvent, CancellationToken cancellationToken)
    {
        var photoReadModel = await photoAppService.GetPhotoAsync(domainEvent.AggregateEvent.PhotoId);
        var userReadModel =
            await queryProcessor.ProcessAsync(new GetUserByIdQuery(domainEvent.AggregateEvent.RequestInfo.UserId), cancellationToken);

        var r = layeredUserService.GetConverter(domainEvent.AggregateEvent.RequestInfo.Layer).ToUserPhoto(userReadModel!, photoReadModel);

        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.RequestInfo, r);
    }

    public async Task HandleAsync(IDomainEvent<UserAggregate, UserId, UserProfileUpdatedEvent> domainEvent,
            CancellationToken cancellationToken)
    {
        var userReadModel = await queryProcessor.ProcessAsync(new GetUserByIdQuery(domainEvent.AggregateEvent.UserId),
            cancellationToken);

        var photos = await photoAppService.GetPhotosAsync(userReadModel);

        var r = layeredUserService.GetConverter(domainEvent.AggregateEvent.RequestInfo.Layer).ToUser(userReadModel!.UserId, userReadModel, photos);

        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.RequestInfo,
            r,
            domainEvent.AggregateEvent.UserId);
    }
}

