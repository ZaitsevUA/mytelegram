using MyTelegram.Schema.Extensions;

namespace MyTelegram.Messenger.QueryServer.EventHandlers;

public class DuplicateOperationExceptionHandler(
    IQueryProcessor queryProcessor,
    IObjectMessageSender messageSender,
    ILogger<DuplicateOperationExceptionHandler> logger)
    : IEventHandler<DuplicateCommandEvent>
//, IDistributedEventHandler<DuplicateCommandEvent>//, ITransientDependency
{
    public async Task HandleEventAsync(DuplicateCommandEvent eventData)
    {
        logger.LogWarning("Duplicate command:{UserId},{ReqMsgId}", eventData.UserId, eventData.ReqMsgId);
        var rpcResult = await queryProcessor.ProcessAsync(new GetRpcResultQuery(eventData.UserId, eventData.ReqMsgId));
        if (rpcResult != null)
        {
            await messageSender.SendRpcMessageToClientAsync(eventData.ReqMsgId,
                rpcResult.RpcData.ToTObject<IObject>());
        }
        else
        {
            logger.LogWarning("Can not find rpc result,userId={UserId},reqMsgId={ReqMsgId}", eventData.UserId, eventData.ReqMsgId);
        }
    }
}