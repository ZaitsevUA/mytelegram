namespace MyTelegram.Domain.Events.User;

public class UserNameUpdatedEvent(
    RequestInfo requestInfo,
    UserItem userItem,
    string? oldUserName)
    : RequestAggregateEvent2<UserAggregate, UserId>(requestInfo)
{
    public string? OldUserName { get; } = oldUserName;

    public UserItem UserItem { get; } = userItem;
}