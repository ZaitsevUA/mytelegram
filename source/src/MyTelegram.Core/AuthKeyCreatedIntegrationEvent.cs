namespace MyTelegram.Core;

////[MemoryPackable]
public record AuthKeyCreatedIntegrationEvent(
    string ConnectionId,
    long ReqMsgId,
    byte[] Data,
    //byte[] ServerSalt,
    long ServerSalt,
    bool IsPermanent,
    byte[] SetClientDhParamsAnswer,
    int? DcId
);