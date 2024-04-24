using EventFlow.Exceptions;
using Microsoft.Extensions.Logging;
using MyTelegram.Schema;

namespace MyTelegram.Services.Services;

public class ExceptionProcessor(
    ILogger<ExceptionProcessor> logger,
    IObjectMessageSender objectMessageSender,
    IEventBus eventBus)
    : IExceptionProcessor //, ISingletonDependency
{
    //private const int BufferSize = 1024 * 4;

    public Task HandleExceptionAsync(Exception ex,
        //int errorCode,
        //string errorMessage,
        long userId,
        string? handlerName,
        long reqMsgId,
        //byte[] authKeyData,
        //byte[] serverSalt,
        //string connectionId,
        //int seqNumber,
        long authKeyId,
        //long sessionId, 
        bool isInMsgContainer,
        DeviceType deviceType
        )
    {
        logger.LogError(ex,
            "Process request {ReqMsgId} {IsInMsgContainer} failed,handler={HandlerName},userId={UserId},authKeyId={AuthKeyId:x2},deviceType={DeviceType}",
            reqMsgId,
            isInMsgContainer ? "in msgContainer" : string.Empty,
            handlerName,
            //connectionId,
            userId,
            authKeyId,
            deviceType
        );

        return ProcessExceptionCoreAsync(ex, userId, reqMsgId);
    }

    public Task HandleExceptionAsync(Exception ex, IRequestInput input, IObject? requestData, string? handlerName)
    {
        logger.LogError(ex,
            "Process request failed,handler={HandlerName},userId={UserId},requestInput={@RequestInput},requestData={@RequestData}",
            handlerName,
            input.UserId,
            input,
            requestData
        );
        return ProcessExceptionCoreAsync(ex, input.UserId, input.ReqMsgId);
    }

    private async Task ProcessExceptionCoreAsync(Exception ex, long userId, long reqMsgId)
    {
        string errorMessage;
        int errorCode;
        switch (ex)
        {
            case DuplicateOperationException:
                var eventData = new DuplicateCommandEvent(userId, reqMsgId);
                await eventBus.PublishAsync(eventData);
                return;
            //break;
            case UserFriendlyException userFriendlyException:
                errorCode = userFriendlyException.ErrorCode;
                errorMessage = userFriendlyException.Message;
                break;

            case NotImplementedException:
                errorCode = MyTelegramServerDomainConsts.InternalErrorCode;
                errorMessage = "API NotImplemented";
                break;

            case RpcException rpcException:
                errorCode = rpcException.RpcError.ErrorCode;
                errorMessage = rpcException.RpcError.Message;
                break;

            case DomainError domainError:
                errorCode = MyTelegramServerDomainConsts.InternalErrorCode;
                errorMessage = domainError.Message;
                break;

            case SagaPublishException sagaPublishException:
                var innerException = sagaPublishException.InnerException;
                errorMessage = innerException switch
                {
                    CommandException { InnerException: UserFriendlyException subInnerException } => subInnerException
                        .Message,
                    _ => MyTelegramServerDomainConsts.InternalErrorMessage
                };
                errorCode = MyTelegramServerDomainConsts.BadRequestErrorCode;
                break;

            default:
                errorCode = MyTelegramServerDomainConsts.InternalErrorCode;
                errorMessage = MyTelegramServerDomainConsts.InternalErrorMessage;
                break;
        }

        var rpcError = new TRpcError { ErrorCode = errorCode, ErrorMessage = errorMessage };
        var rpcResult = new TRpcResult { ReqMsgId = reqMsgId, Result = rpcError };
        await objectMessageSender.SendMessageToPeerAsync(reqMsgId, rpcResult);
    }
}