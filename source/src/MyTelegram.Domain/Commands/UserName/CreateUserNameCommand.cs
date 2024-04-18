using System.Text;

namespace MyTelegram.Domain.Commands.UserName;

public class CreateUserNameCommand(UserNameId aggregateId, long userId, string userName)
    : DistinctCommand<UserNameAggregate, UserNameId, IExecutionResult>(aggregateId)
{
    public long UserId { get; } = userId;
    public string UserName { get; } = userName;

    protected override IEnumerable<byte[]> GetSourceIdComponents()
    {
        yield return BitConverter.GetBytes(UserId);
        yield return Encoding.UTF8.GetBytes(UserName);
    }
}
