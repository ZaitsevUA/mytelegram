namespace MyTelegram.Domain.Commands.User;

public class UpdateBirthdayCommand : Command<UserAggregate, UserId, IExecutionResult>
{
    public Birthday? Birthday { get; }

    public UpdateBirthdayCommand(UserId aggregateId,Birthday? birthday) : base(aggregateId)
    {
        Birthday = birthday;
    }
}