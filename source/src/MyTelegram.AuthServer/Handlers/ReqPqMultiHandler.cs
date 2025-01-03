﻿namespace MyTelegram.AuthServer.Handlers;

public class ReqPqMultiHandler(
    IStep1Helper step1ServerHelper,
    ILogger<ReqPqMultiHandler> logger,
    ICacheManager<AuthCacheItem> cacheManager
) : BaseObjectHandler<RequestReqPqMulti, IResPQ>, IReqPqMultiHandler
{
    protected override async Task<IResPQ> HandleCoreAsync(
        IRequestInput input,
        RequestReqPqMulti obj
    )
    {
        var dto = step1ServerHelper.GetResponse(obj.Nonce);

        var authCacheItem = new AuthCacheItem(obj.Nonce, dto.ServerNonce, dto.P, dto.Q, false);
        var key = AuthCacheItem.GetCacheKey(dto.ServerNonce);
        await cacheManager.SetAsync(
            key,
            authCacheItem,
            MyTelegramServerDomainConsts.AuthKeyExpireSeconds
        );
        logger.LogTrace(
            "[Step1] ReqPqMulti created, connectionId={ConnectionId}, reqMsgId: {ReqMsgId}, authKeyId: {AuthKeyId}",
            input.ConnectionId,
            input.ReqMsgId,
            input.AuthKeyId
        );

        return dto.ResPq;
    }
}