namespace MyTelegram.Domain.Commands.UserName;

public class DeleteUserNameCommand : Command<UserNameAggregate, UserNameId, IExecutionResult>
{
    public DeleteUserNameCommand(UserNameId aggregateId) : base(aggregateId)
    {
    }

    public DeleteUserNameCommand(UserNameId aggregateId,
        ISourceId sourceId) : base(aggregateId, sourceId)
    {
    }
}

//public class CheckUserNameCommand : RequestCommand<UserNameAggregate, UserNameId, IExecutionResult>
//{
//    public string UserName { get; }
//    public CheckUserNameCommand(UserNameId aggregateId,
//        long reqMsgId,
//        string userName) : base(aggregateId, reqMsgId)
//    {
//        UserName = userName;
//    }
//}
