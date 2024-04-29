namespace MyTelegram.Core;

public record LayeredPushMessageCreatedIntegrationEvent(
    int PeerType,
    long PeerId,
    byte[] Data,
    long? ExcludeAuthKeyId,
    long? ExcludeUserId,
    long? OnlySendToUserId,
    long? OnlySendToThisAuthKeyId,
    int Pts,
    int? Qts,
    long GlobalSeqNo,
    LayeredData<byte[]>? LayeredData) : ISessionMessage;

public record LayeredPushMessageCreatedIntegrationEvent<TExtraData>(
    int PeerType,
    long PeerId,
    byte[] Data,
    long? ExcludeAuthKeyId,
    long? ExcludeUserId,
    long? OnlySendToUserId,
    long? OnlySendToThisAuthKeyId,
    int Pts,
    int? Qts,
    //PtsType PtsType,
    //UpdatesType UpdatesType,
    long GlobalSeqNo,
    LayeredData<byte[]>? LayeredData,
    TExtraData ExtraData) :
    LayeredPushMessageCreatedIntegrationEvent(PeerType, PeerId, Data, ExcludeAuthKeyId, ExcludeUserId,
        OnlySendToUserId,
        OnlySendToThisAuthKeyId, Pts, Qts, GlobalSeqNo, LayeredData);
