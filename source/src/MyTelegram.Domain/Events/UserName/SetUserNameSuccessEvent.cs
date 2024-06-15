namespace MyTelegram.Domain.Events.UserName;

public class SetUserNameSuccessEvent(
    RequestInfo requestInfo,
    long selfUserId,
    string userName,
    PeerType peerType,
    long peerId)
    : RequestAggregateEvent2<UserNameAggregate, UserNameId>(requestInfo)
{
    public long PeerId { get; } = peerId;
    public PeerType PeerType { get; } = peerType;
    public long SelfUserId { get; } = selfUserId;
    public string UserName { get; } = userName;
}
