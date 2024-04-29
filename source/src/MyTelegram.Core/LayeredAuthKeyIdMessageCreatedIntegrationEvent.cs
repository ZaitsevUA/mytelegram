namespace MyTelegram.Core;

public partial record LayeredAuthKeyIdMessageCreatedIntegrationEvent(
    long AuthKeyId, byte[] Data, int Pts, long GlobalSeqNo, LayeredData<byte[]>? LayeredData) : ISessionMessage;