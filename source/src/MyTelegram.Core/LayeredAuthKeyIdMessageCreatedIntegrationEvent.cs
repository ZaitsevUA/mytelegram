namespace MyTelegram.Core;

public record LayeredAuthKeyIdMessageCreatedIntegrationEvent(
    long AuthKeyId,
    byte[] Data,
    int Pts,
    int? Qts,
    long GlobalSeqNo,
    LayeredData<byte[]>? LayeredData) : ISessionMessage;