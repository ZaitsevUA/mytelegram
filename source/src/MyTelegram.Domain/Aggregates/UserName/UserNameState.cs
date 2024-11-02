namespace MyTelegram.Domain.Aggregates.UserName;

public class UserNameState : AggregateState<UserNameAggregate, UserNameId, UserNameState>,
    IApply<SetUserNameSuccessEvent>,
    IApply<UserNameDeletedEvent>,
    IApply<UserNameCreatedEvent>,
    IApply<UserNameChangedEvent>
{
    public bool IsDeleted { get; private set; }
    public string? UserName { get; private set; }
    public Peer Peer { get; private set; } = default!;

    public void Apply(SetUserNameSuccessEvent aggregateEvent)
    {
        IsDeleted = false;
        UserName = aggregateEvent.UserName;
    }

    public void Apply(UserNameDeletedEvent aggregateEvent)
    {
        IsDeleted = true;
    }

    public void LoadSnapshot(UserNameSnapshot snapshot)
    {
        UserName = snapshot.UserName;
        IsDeleted = snapshot.IsDeleted;
        Peer = snapshot.Peer;
    }

    public void Apply(UserNameCreatedEvent aggregateEvent)
    {
        UserName = aggregateEvent.UserName;
        IsDeleted = false;
    }

    public void Apply(UserNameChangedEvent aggregateEvent)
    {
        Peer = aggregateEvent.Peer;
    }
}
