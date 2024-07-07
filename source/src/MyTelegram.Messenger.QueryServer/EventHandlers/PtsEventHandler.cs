using MyTelegram.Schema.Extensions;

namespace MyTelegram.Messenger.QueryServer.EventHandlers;

public class PtsEventHandler(
    IAckCacheService ackCacheService,
    ICommandBus commandBus,
    ILogger<PtsEventHandler> logger)
    :
        IEventHandler<NewPtsMessageHasSentEvent>,
        IEventHandler<NewQtsMessageHasSentEvent>,
        IEventHandler<RpcMessageHasSentEvent>,
        IEventHandler<AcksDataReceivedEvent>
{
    public async Task HandleEventAsync(AcksDataReceivedEvent eventData)
    {
        var data = eventData.Data.ToTObject<TMsgsAck>();
        logger.LogTrace("Receive ack from {UserId}(authKeyId={AuthKeyId:x2}):{@MsgIds}",
            eventData.UserId,
            eventData.AuthKeyId,
            data.MsgIds);
        foreach (var msgId in data.MsgIds)
        {
            if (ackCacheService.TryGetPts(msgId, out var ackCacheItem))
            {
                logger.LogDebug("Ack pts,msgId:{MsgId},userId:{UserId},pts:{Pts},globalSeqNo:{GlobalSeqNo}  {Id}", msgId, eventData.UserId, ackCacheItem.Pts, ackCacheItem.GlobalSeqNo,PtsId.Create(eventData.UserId,eventData.PermAuthKeyId).Value);
                if (ackCacheItem.IsQts)
                {
                    var command = new QtsAckedCommand(PtsId.Create(eventData.UserId, eventData.PermAuthKeyId),
                        eventData.UserId,
                        eventData.PermAuthKeyId,
                        msgId,
                        ackCacheItem.Pts,
                        ackCacheItem.GlobalSeqNo,
                        ackCacheItem.ToPeer,
                        ackCacheItem.IsFromGetDifference
                    );
                    await commandBus.PublishAsync(command, default);
                }
                else
                {
                var command = new PtsAckedCommand(PtsId.Create(eventData.UserId, eventData.PermAuthKeyId),
                    eventData.UserId,
                    eventData.PermAuthKeyId,
                    msgId,
                    ackCacheItem.Pts,
                    ackCacheItem.GlobalSeqNo,
                        ackCacheItem.ToPeer,
                        ackCacheItem.IsFromGetDifference
                );
                await commandBus.PublishAsync(command, default);
                }
            }
            else
            {
                if (ackCacheService.TryGetRpcPtsCache(msgId, out ackCacheItem))
                {
                    logger.LogDebug("Ack rpc pts,msgId:{MsgId},userId:{UserId},pts:{Pts},globalSeqNo:{GlobalSeqNo}", msgId, eventData.UserId, ackCacheItem.Pts, ackCacheItem.GlobalSeqNo);

                    var command = new PtsAckedCommand(PtsId.Create(eventData.UserId, eventData.PermAuthKeyId),
                        eventData.UserId,
                        eventData.PermAuthKeyId,
                        msgId,
                        ackCacheItem.Pts,
                        ackCacheItem.GlobalSeqNo,
                        ackCacheItem.ToPeer,
                        ackCacheItem.IsFromGetDifference
                    );
                    await commandBus.PublishAsync(command, default);
                }
            }
        }
    }

    public Task HandleEventAsync(NewPtsMessageHasSentEvent eventData)
    {
        logger.LogDebug("New message has sent,msgId={MsgId},userId={UserId},pts={Pts}",
            eventData.MsgId,
            eventData.UserId,
            eventData.Pts);
        ackCacheService.AddMsgIdToCacheAsync(eventData.MsgId, eventData.Pts, eventData.GlobalSeqNo, eventData.ToPeer);
        return Task.CompletedTask;
    }

    public Task HandleEventAsync(RpcMessageHasSentEvent eventData)
    {
        logger.LogDebug("New rpc message has sent,reqMsgId={ReqMsgId},msgId={MsgId},userId={UserId},globalSeqNo={GlobalSeqNo}",
            eventData.ReqMsgId,
            eventData.MsgId,
            eventData.UserId,
            eventData.GlobalSeqNo
        );
        ackCacheService.AddRpcMsgIdToCache(eventData.MsgId, eventData.ReqMsgId);

        return Task.CompletedTask;
    }
    public Task HandleEventAsync(NewQtsMessageHasSentEvent eventData)
    {
        logger.LogDebug("New message has sent,msgId={MsgId},userId={UserId},qts={Qts}",
            eventData.MsgId,
            eventData.UserId,
            eventData.Qts);
        ackCacheService.AddMsgIdToCacheAsync(eventData.MsgId, eventData.Qts, eventData.GlobalSeqNo, eventData.ToPeer, true);
        return Task.CompletedTask;
    }
}
