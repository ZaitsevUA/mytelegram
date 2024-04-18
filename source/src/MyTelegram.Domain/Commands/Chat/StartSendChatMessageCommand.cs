namespace MyTelegram.Domain.Commands.Chat;

public class StartSendChatMessageCommand(
    ChatId aggregateId,
    RequestInfo requestInfo,
    long senderPeerId,
    int senderMessageId,
    bool senderIsBot)
    : /*Request*/RequestCommand2<ChatAggregate, ChatId, IExecutionResult>(aggregateId, requestInfo) //,
//IHasCorrelationId
{
    //long reqMsgId,

    public bool SenderIsBot { get; } = senderIsBot;
    public int SenderMessageId { get; } = senderMessageId;

    public long SenderPeerId { get; } = senderPeerId;

    //protected override IEnumerable<byte[]> GetSourceIdComponents()
    //{
    //    yield return BitConverter.GetBytes(ReqMsgId);
    //    yield return Encoding.UTF8.GetBytes(AggregateId.Value);
    //}
}