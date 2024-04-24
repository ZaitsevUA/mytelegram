using MyTelegram.Schema;

namespace MyTelegram.Services.Services;

public interface IExceptionProcessor
{
    Task HandleExceptionAsync(Exception ex,
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
        );

    Task HandleExceptionAsync(Exception ex, IRequestInput input, IObject? requestData, string? handlerName);
}