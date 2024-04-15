namespace MyTelegram.Domain.Events.User;

public class BirthdayUpdatedEvent : AggregateEvent<UserAggregate, UserId>
{
    public Birthday? Birthday { get; }

    public BirthdayUpdatedEvent(Birthday? birthday)
    {
        Birthday = birthday;
    }
}