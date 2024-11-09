using MyTelegram.Schema;

namespace MyTelegram.Services.Services;

public class QueuedObjectMessageSender(
    IMessageQueueProcessor<ISessionMessage> sessionMessageQueueProcessor,
    IGZipHelper gzipHelper)
    : IObjectMessageSender, ITransientDependency
{
    private readonly int _maxQueueCount = 10;

    public Task PushSessionMessageToAuthKeyIdAsync<TData>(long authKeyId,
        TData data,
        int pts = 0,
        int? qts = null,
        long globalSeqNo = 0, LayeredData<TData>? layeredData = null) where TData : IObject
    {
        var layeredByteData = layeredData?.DataWithLayer?.ToDictionary(k => k.Key, v => v.Value.ToBytes());

        sessionMessageQueueProcessor.Enqueue(new LayeredAuthKeyIdMessageCreatedIntegrationEvent(authKeyId,
                data.ToBytes(),
                pts,
                qts,
                globalSeqNo, new LayeredData<byte[]>(layeredByteData)),
            authKeyId);
        return Task.CompletedTask;
    }

    //public Task PushSessionMessageToPeerAsync<TData>(Peer peer,
    //    TData data,
    //    long? excludeAuthKeyId = null,
    //    long? excludeUserId = null,
    //    long? onlySendToUserId = null,
    //    long? onlySendToThisAuthKeyId = null,
    //    int pts = 0,
    //    int? qts = null,
    //    long globalSeqNo = 0,
    //    LayeredData<TData>? layeredData = null) where TData : IObject
    //{
    //    sessionMessageQueueProcessor.Enqueue(new LayeredPushMessageCreatedIntegrationEvent((int)peer.PeerType,
    //            peer.PeerId,
    //            data.ToBytes(),
    //            excludeAuthKeyId,
    //            excludeUserId,
    //            onlySendToUserId,
    //            onlySendToThisAuthKeyId,
    //            pts,
    //            qts,
    //            globalSeqNo,
    //            new LayeredData<byte[]>(layeredData?.DataWithLayer?.ToDictionary(k => k.Key, v => v.Value.ToBytes()))),
    //        peer.PeerId);
    //    return Task.CompletedTask;
    //}

    public Task PushMessageToPeerAsync<TData>(Peer peer,
        TData data,
        long? excludeAuthKeyId = null,
        long? excludeUserId = null,
        long? onlySendToUserId = null,
        long? onlySendToThisAuthKeyId = null,
        int pts = 0,
        int? qts = null,
        long globalSeqNo = 0,
        LayeredData<TData>? layeredData = null,
        PushData? pushData = null
        ) where TData : IObject
    {
        sessionMessageQueueProcessor.Enqueue(new LayeredPushMessageCreatedIntegrationEvent((int)peer.PeerType,
                peer.PeerId,
                data.ToBytes(),
                excludeAuthKeyId,
                excludeUserId,
                onlySendToUserId,
                onlySendToThisAuthKeyId,
                pts,
                qts,
                globalSeqNo,
                new LayeredData<byte[]>(layeredData?.DataWithLayer?.ToDictionary(k => k.Key, v => v.Value.ToBytes())),
                PushData: pushData
            ),
            peer.PeerId);
        return Task.CompletedTask;
    }

    public Task PushMessageToPeerAsync<TData, TExtraData>(Peer peer,
        TData data,
        long? excludeAuthKeyId = null,
        long? excludeUserId = null,
        long? onlySendToUserId = null,
        long? onlySendToThisAuthKeyId = null,
        int pts = 0,
        int? qts = null,
        long globalSeqNo = 0,
        LayeredData<TData>? layeredData = null,
        TExtraData? extraData = default,
        PushData? pushData = null
        ) where TData : IObject
    {
        if (extraData == null)
        {
            return PushMessageToPeerAsync(peer,
                data,
                excludeAuthKeyId,
                excludeUserId,
                onlySendToUserId,
                onlySendToThisAuthKeyId,
                pts,
                qts,
                globalSeqNo,
                layeredData,
                pushData
                );
        }

        sessionMessageQueueProcessor.Enqueue(new LayeredPushMessageCreatedIntegrationEvent<TExtraData>(
                (int)peer.PeerType,
                peer.PeerId,
                data.ToBytes(),
                excludeAuthKeyId,
                excludeUserId,
                onlySendToUserId,
                onlySendToThisAuthKeyId,
                pts,
                qts,
                globalSeqNo,
                new LayeredData<byte[]>(layeredData?.DataWithLayer?.ToDictionary(k => k.Key, v => v.Value.ToBytes())),
                extraData,
                pushData
            ),
            peer.PeerId);
        return Task.CompletedTask;
    }

    public Task SendMessageToPeerAsync<TData>(long reqMsgId,
        TData data) where TData : IObject
    {
        sessionMessageQueueProcessor.Enqueue(new DataResultResponseReceivedEvent(reqMsgId, data.ToBytes()),
            reqMsgId % _maxQueueCount);

        return Task.CompletedTask;
    }

    public Task SendFileDataToPeerAsync<TData>(long reqMsgId,
        TData data) where TData : IObject
    {
        sessionMessageQueueProcessor.Enqueue(new FileDataResultResponseReceivedEvent(reqMsgId, data.ToBytes()),
            reqMsgId % _maxQueueCount);
        return Task.CompletedTask;
    }

    public Task SendRpcMessageToClientAsync<TData>(long reqMsgId,
        TData data,
        int pts = 0) where TData : IObject
    {
        var rpcResult = CreateRpcResult(reqMsgId, data);

        sessionMessageQueueProcessor.Enqueue(new DataResultResponseReceivedEvent(reqMsgId, rpcResult.ToBytes()),
            reqMsgId % _maxQueueCount);
        return Task.CompletedTask;
    }

    public Task SendRpcMessageToClientAsync<TData>(long reqMsgId, TData data,
        long authKeyId, long permAuthKeyId, long userId,
        int pts = 0) where TData : IObject
    {
        var rpcResult = CreateRpcResult(reqMsgId, data);
        sessionMessageQueueProcessor.Enqueue(
            new DataResultResponseWithUserIdReceivedEvent(reqMsgId, rpcResult.ToBytes(), userId, authKeyId,
                permAuthKeyId),
            reqMsgId % _maxQueueCount);
        return Task.CompletedTask;
    }

    private TRpcResult CreateRpcResult<TData>(long reqMsgId, TData data) where TData : IObject
    {
        var newData = data;
        var rpcResult = new TRpcResult { ReqMsgId = reqMsgId, Result = data };

        var length = data.GetLength();
        if (length > 500)
        {
            var gzipPacked = new TGzipPacked
            {
                PackedData = gzipHelper.Compress(newData.ToBytes())
            };
            rpcResult.Result = gzipPacked;
        }

        return rpcResult;
    }
}