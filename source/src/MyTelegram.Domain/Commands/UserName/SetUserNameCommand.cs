namespace MyTelegram.Domain.Commands.UserName;

public class SetUserNameCommand(
    UserNameId aggregateId,
    RequestInfo requestInfo,
    long selfUserId,
    PeerType peerType,
    long peerId,
    string userName)
    : RequestCommand2<UserNameAggregate, UserNameId, IExecutionResult>(aggregateId, requestInfo)
{
    public long PeerId { get; } = peerId;
    public PeerType PeerType { get; } = peerType;
    public long SelfUserId { get; } = selfUserId;
    public string UserName { get; } = userName;
}
