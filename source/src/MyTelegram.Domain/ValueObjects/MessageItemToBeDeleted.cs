namespace MyTelegram.Domain.ValueObjects;

public record MessageItemToBeDeleted(long OwnerUserId, int MessageId, PeerType ToPeerType, long ToPeerId);