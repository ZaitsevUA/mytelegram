namespace MyTelegram.Domain.Events.UserName;

public class UserNameChangedEvent(RequestInfo requestInfo, Peer peer, string? userName, string? oldUserName) : RequestAggregateEvent2<UserNameAggregate, UserNameId>(requestInfo)
{
    public Peer Peer { get; } = peer;
    public string? UserName { get; } = userName;
    public string? OldUserName { get; } = oldUserName;
}