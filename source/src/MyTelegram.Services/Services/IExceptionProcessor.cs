using MyTelegram.Schema;

namespace MyTelegram.Services.Services;

public interface IExceptionProcessor
{
    Task HandleExceptionAsync(Exception ex,
        IObject requestData,
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
        bool isInMsgContainer);
}