namespace MyTelegram.Domain.Events.UserName;

public class UserNameCreatedEvent(long userId, string userName) : AggregateEvent<UserNameAggregate, UserNameId>
{
    public long UserId { get; } = userId;
    public string UserName { get; } = userName;
}
