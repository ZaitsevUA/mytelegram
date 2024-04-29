namespace MyTelegram.Core;

public record LayeredAuthKeyIdMessageCreatedIntegrationEvent(
    long AuthKeyId,
    byte[] Data,
    int Pts,
    long GlobalSeqNo,
    LayeredData<byte[]>? LayeredData) : ISessionMessage;