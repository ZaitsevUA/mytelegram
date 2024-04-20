//namespace MyTelegram.Domain.Commands.Messaging;

//public class ReplyToChannelMessageCommand : RequestCommand2<MessageAggregate, MessageId, IExecutionResult>
//{
//    public int RepliesPts { get; }
//    public int MaxMessageId { get; }

//    public ReplyToChannelMessageCommand(MessageId aggregateId, RequestInfo requestInfo, int repliesPts, int maxMessageId) : base(aggregateId, requestInfo)
//    {
//        RepliesPts = repliesPts;
//        MaxMessageId = maxMessageId;
//    }
//}