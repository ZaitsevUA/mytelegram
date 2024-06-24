namespace MyTelegram.AuthServer.Handlers;

public class ReqPqHandler(
    IStep1Helper step1ServerHelper,
    ILogger<ReqPqHandler> logger,
    ICacheManager<AuthCacheItem> cacheManager,
    IHashHelper hashHelper)
    : BaseObjectHandler<RequestReqPq, IResPQ>,
        IReqPqHandler
{
    private readonly IHashHelper _hashHelper = hashHelper;

    protected override async Task<IResPQ> HandleCoreAsync(IRequestInput input,
        RequestReqPq obj)
    {
        var dto = step1ServerHelper.GetResponse(obj.Nonce);
        var authCacheItem = new AuthCacheItem(obj.Nonce,
            dto.ServerNonce,
            dto.P,
            dto.Q,
            false);

        var key = AuthCacheItem.GetCacheKey(dto.ServerNonce);

        await cacheManager.SetAsync(key, authCacheItem, MyTelegramServerDomainConsts.AuthKeyExpireSeconds);
        logger.LogDebug("[Step1] ConnectionId={ConnectionId} Create resPq,reqMsgId={ReqMsgId}", input.ConnectionId, input.ReqMsgId);

        return dto.ResPq;
    }
}