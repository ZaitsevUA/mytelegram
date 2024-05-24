namespace MyTelegram.Domain.ValueObjects;

public record MessageItemToBeDeleted(long OwnerUserId, int MessageId, PeerType ToPeerType, long ToPeerId);
public record SimpleMessageItem(long OwnerPeerId, int MessageId, PeerType ToPeerType, long ToPeerId);