namespace MyTelegram.Domain.Commands.Chat;

public class SetChatPinnedMsgIdCommand(
    ChatId aggregateId,
    long reqMsgId,
    int pinnedMsgId)
    : DistinctCommand<ChatAggregate, ChatId, IExecutionResult>(aggregateId)
{
    public int PinnedMsgId { get; } = pinnedMsgId;
    public long ReqMsgId { get; } = reqMsgId;

    protected override IEnumerable<byte[]> GetSourceIdComponents()
    {
        yield return BitConverter.GetBytes(ReqMsgId);
        yield return BitConverter.GetBytes(PinnedMsgId);
    }
}
