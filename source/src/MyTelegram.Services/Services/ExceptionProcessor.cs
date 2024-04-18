using EventFlow.Exceptions;
using Microsoft.Extensions.Logging;
using MyTelegram.Core;
using MyTelegram.Schema;

namespace MyTelegram.Services.Services;

public class ExceptionProcessor(
    ILogger<ExceptionProcessor> logger,
    IObjectMessageSender objectMessageSender,
    IEventBus eventBus)
    : IExceptionProcessor
{
    //private const int BufferSize = 1024 * 4;

    public async Task HandleExceptionAsync(Exception ex,
        IObject requestData,
        long userId,
        string? handlerName,
        long reqMsgId,
        long authKeyId,
        bool isInMsgContainer)
    {
        logger.LogError(ex,
            "Process request {ReqMsgId} {IsInMsgContainer} failed,handler={HandlerName},userId={UserId},authKeyId={AuthKeyId:x2},requestData={@RequestData}",
            reqMsgId,
            isInMsgContainer ? "in msgContainer" : string.Empty,
            handlerName,
            userId,
            authKeyId,
            requestData
        );

        string errorMessage;
        int errorCode;
        switch (ex)
        {
            case DuplicateOperationException duplicateOperationException:
                //errorCode = MyTelegramServerDomainConsts.InternalErrorCode;
                //errorMessage = MyTelegramServerDomainConsts.InternalErrorMessage;
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
                //{
                //    InnerException: CommandException
                //    {
                //        InnerException: UserFriendlyException or DomainError
                //    } commandException
                //}:
                var innerException = sagaPublishException.InnerException;
                errorMessage = innerException switch
                {
                    CommandException { InnerException: UserFriendlyException subInnerException } => subInnerException
                        .Message,
                    _ => MyTelegramServerDomainConsts.InternalErrorMessage
                };
                errorCode = MyTelegramServerDomainConsts.BadRequestErrorCode;
                //errorMessage = commandException.InnerException?.Message ?? MyTelegramServerDomainConsts.InternalErrorMessage;
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