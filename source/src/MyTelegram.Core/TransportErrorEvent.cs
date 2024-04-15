namespace MyTelegram.Core;

public partial record TransportErrorEvent(long AuthKeyId,
    string ConnectionId, int TransportErrorCode);