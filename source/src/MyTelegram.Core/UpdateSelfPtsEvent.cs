namespace MyTelegram.Core;

public record UpdateSelfPtsEvent(
    long SelfUserId,
    long SelfPermAuthKeyId,
    int Pts);