using MyTelegram.Schema;
using MyTelegram.Schema.Extensions;
using System.Diagnostics;

namespace MyTelegram.Services.Services;

public class DefaultDataProcessor<TData>(
    IHandlerHelper handlerHelper,
    IObjectMessageSender objectMessageSender,
    IRpcResultCacheAppService rpcResultCacheAppService,
    ILogger<DefaultDataProcessor<TData>> logger,
    IExceptionProcessor exceptionProcessor,
    IInvokeAfterMsgProcessor invokeAfterMsgProcessor)
    : IDataProcessor<TData>
    where TData : DataReceivedEvent
{
    public virtual Task ProcessAsync(TData obj)
    {
        Task.Run(async () =>
        {
            var sw = Stopwatch.StartNew();
            if (handlerHelper.TryGetHandler(obj.ObjectId, out var handler))
            {
                if (rpcResultCacheAppService.TryGetRpcResult(obj.UserId, obj.ReqMsgId, out var rpcResult))
                {
                    sw.Stop();
                    await SendMessageToPeerAsync(obj.ReqMsgId, rpcResult);
                    return;
                }

                IObject? data = null;
                var req = GetRequestInput(obj);
                try
                {
                    data = GetData(obj);
                    var handlerName = handler.GetType().Name;
                    if (data is IHasSubQuery)
                    {
                        handlerName = handlerHelper.GetHandlerFullName(data);
                    }
                    var r = await handler.HandleAsync(req, data);
                    sw.Stop();

                    logger.LogInformation(
                        "User {UserId} {Handler} {DeviceType}[{Layer}] {Timespan}ms",
                        obj.UserId,
                        handlerName,
                        obj.DeviceType,
                        obj.Layer,
                        sw.Elapsed.TotalMilliseconds
                    );

                    if (r != null!)
                    {
                        await SendMessageToPeerAsync(obj.ReqMsgId, r);
                    }

                    await invokeAfterMsgProcessor.AddCompletedReqMsgIdAsync(obj.ReqMsgId);
                }
                catch (Exception ex)
                {
                    await exceptionProcessor.HandleExceptionAsync(ex, req, data,
                        handler.GetType().Name);
                }
            }
        });

        return Task.CompletedTask;
    }

    protected virtual IObject GetData(TData obj)
    {
        return obj.Data.ToTObject<IObject>();
    }

    protected virtual IRequestInput GetRequestInput(TData obj)
    {
        var req = new RequestInput(
            obj.ConnectionId,
            obj.RequestId,
            obj.ObjectId,
            obj.ReqMsgId,
            obj.UserId,
            obj.AuthKeyId,
            obj.PermAuthKeyId,
            obj.Layer,
            obj.Date,
            obj.DeviceType,
            obj.ClientIp
        );

        return req;
    }

    protected virtual Task SendMessageToPeerAsync(long reqMsgId,
        IObject data)
    {
        return objectMessageSender.SendMessageToPeerAsync(reqMsgId, data);
    }
}