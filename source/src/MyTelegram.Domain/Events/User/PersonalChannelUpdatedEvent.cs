namespace MyTelegram.Domain.Events.User;

public class PersonalChannelUpdatedEvent : AggregateEvent<UserAggregate, UserId>
{
    public long UserId { get; }
    public long? PersonalChannelId { get; }

    public PersonalChannelUpdatedEvent(long userId,long? personalChannelId)
    {
        UserId = userId;
        PersonalChannelId = personalChannelId;
    }
}