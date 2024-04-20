namespace MyTelegram.Domain.Commands.Channel;

public class StartSendChannelMessageCommand(
    ChannelId aggregateId,
    RequestInfo requestInfo,
    long senderPeerId,
    bool senderIsBot,
    int messageId,
    MessageSubType messageSubType)
    : RequestCommand2<ChannelAggregate, ChannelId, IExecutionResult>(aggregateId, requestInfo) //, IHasCorrelationId
{
    //long reqMsgId,
    //ReqMsgId = reqMsgId;

    public int MessageId { get; } = messageId;

    public bool SenderIsBot { get; } = senderIsBot;

    //public long ReqMsgId { get; }
    public long SenderPeerId { get; } = senderPeerId;
    public MessageSubType SubType { get; } = messageSubType;
}