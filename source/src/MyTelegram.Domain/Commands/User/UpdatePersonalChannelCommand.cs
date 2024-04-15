namespace MyTelegram.Domain.Commands.User;

public class UpdatePersonalChannelCommand : RequestCommand2<UserAggregate, UserId, IExecutionResult>
{
    public long? PersonalChannelId { get; }

    public UpdatePersonalChannelCommand(UserId aggregateId, RequestInfo requestInfo,long? personalChannelId) : base(aggregateId, requestInfo)
    {
        PersonalChannelId = personalChannelId;
    }
}