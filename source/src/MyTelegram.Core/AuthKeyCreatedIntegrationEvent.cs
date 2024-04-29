namespace MyTelegram.Core;

public record AuthKeyCreatedIntegrationEvent(
    byte[] Data,
    long ServerSalt,
    bool IsPermanent);