namespace MyTelegram.Domain.Commands.Chat;

public class CheckChatStateCommand(ChatId aggregateId, RequestInfo requestInfo)
    : RequestCommand2<ChatAggregate, ChatId, IExecutionResult>(aggregateId, requestInfo)
{
    protected override IEnumerable<byte[]> GetSourceIdComponents()
    {
        yield return RequestInfo.RequestId.ToByteArray();
    }
}