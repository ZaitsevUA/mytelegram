using System.Text;

namespace MyTelegram.Domain.Commands.UserName;

public class CreateUserNameCommand : DistinctCommand<UserNameAggregate, UserNameId, IExecutionResult>
{
    public long UserId { get; }
    public string UserName { get; }

    public CreateUserNameCommand(UserNameId aggregateId, long userId, string userName) : base(aggregateId)
    {
        UserId = userId;
        UserName = userName;
    }

    protected override IEnumerable<byte[]> GetSourceIdComponents()
    {
        yield return BitConverter.GetBytes(UserId);
        yield return Encoding.UTF8.GetBytes(UserName);
    }
}
