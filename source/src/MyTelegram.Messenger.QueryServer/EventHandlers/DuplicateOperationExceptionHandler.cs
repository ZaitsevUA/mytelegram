using MyTelegram.Schema.Extensions;

namespace MyTelegram.Messenger.QueryServer.EventHandlers;

public class DuplicateOperationExceptionHandler(
    IQueryProcessor queryProcessor,
    IObjectMessageSender messageSender,
    ILogger<DuplicateOperationExceptionHandler> logger)
    : IEventHandler<DuplicateCommandEvent>, ITransientDependency
{
    public async Task HandleEventAsync(DuplicateCommandEvent eventData)
    {
        logger.LogWarning("Duplicate command, userId: {UserId} reqMsgId: {ReqMsgId}", eventData.UserId, eventData.ReqMsgId);
        var rpcResult = await queryProcessor.ProcessAsync(new GetRpcResultQuery(eventData.UserId, eventData.ReqMsgId));
        if (rpcResult != null)
        {
            await messageSender.SendRpcMessageToClientAsync(eventData.ReqMsgId,
                rpcResult.RpcData.ToTObject<IObject>(), 0, eventData.PermAuthKeyId);
        }
        else
        {
            logger.LogWarning("Cannot find rpc result, userId: {UserId}, reqMsgId: {ReqMsgId}", eventData.UserId, eventData.ReqMsgId);
        }
    }
}