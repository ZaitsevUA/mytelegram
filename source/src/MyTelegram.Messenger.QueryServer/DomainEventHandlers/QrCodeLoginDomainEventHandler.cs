using MyTelegram.Messenger.Services.Caching;
using MyTelegram.Messenger.TLObjectConverters.Interfaces;
using MyTelegram.Services.TLObjectConverters;

namespace MyTelegram.Messenger.QueryServer.DomainEventHandlers;

public class QrCodeLoginDomainEventHandler(
    IObjectMessageSender objectMessageSender,
    ICommandBus commandBus,
    IIdGenerator idGenerator,
    IAckCacheService ackCacheService,
    IResponseCacheAppService responseCacheAppService,
    IQueryProcessor queryProcessor,
    ILogger<QrCodeLoginDomainEventHandler> logger,
    ILayeredService<IAuthorizationConverter> layeredAuthorizationService)
    : DomainEventHandlerBase(objectMessageSender,
            commandBus,
            idGenerator,
            ackCacheService,
            responseCacheAppService),
        ISubscribeSynchronousTo<QrCodeAggregate, QrCodeId, QrCodeLoginTokenExportedEvent>,
        ISubscribeSynchronousTo<QrCodeAggregate, QrCodeId, LoginTokenAcceptedEvent>
{
    private readonly IObjectMessageSender _objectMessageSender = objectMessageSender;

    public async Task HandleAsync(IDomainEvent<QrCodeAggregate, QrCodeId, LoginTokenAcceptedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        var deviceReadModel = await queryProcessor
            .ProcessAsync(new GetDeviceByAuthKeyIdQuery(domainEvent.AggregateEvent.QrCodeLoginRequestPermAuthKeyId), cancellationToken);

        if (deviceReadModel == null)
        {
            logger.LogWarning(
                "Get device info failed.perm authKeyId={PermAuthKeyId:x2}",
                domainEvent.AggregateEvent.QrCodeLoginRequestPermAuthKeyId);
            return;
        }

        var authorization = layeredAuthorizationService.GetConverter(domainEvent.AggregateEvent.RequestInfo.Layer).ToAuthorization(deviceReadModel);
        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.RequestInfo, authorization);

        var updateShortForLoginWithTokenRequestOwner =
            new TUpdateShort { Date = DateTime.UtcNow.ToTimestamp(), Update = new TUpdateLoginToken() };

        await _objectMessageSender
            .PushSessionMessageToAuthKeyIdAsync(domainEvent.AggregateEvent.QrCodeLoginRequestTempAuthKeyId,
                updateShortForLoginWithTokenRequestOwner);

        logger.LogDebug("Accept qr code login token,userId={UserId},qr code client authKeyId={AuthKeyId}",
            domainEvent.AggregateEvent.UserId,
            domainEvent.AggregateEvent.QrCodeLoginRequestTempAuthKeyId);
    }

    public async Task HandleAsync(
        IDomainEvent<QrCodeAggregate, QrCodeId, QrCodeLoginTokenExportedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        var r = new TLoginToken
        {
            Token = domainEvent.AggregateEvent.Token,
            Expires = domainEvent.AggregateEvent.ExpireDate
        };

        await SendRpcMessageToClientAsync(domainEvent.AggregateEvent.RequestInfo, r);
    }
}
