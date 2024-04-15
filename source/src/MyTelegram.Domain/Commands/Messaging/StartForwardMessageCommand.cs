//namespace MyTelegram.Domain.Commands.Messaging;

//public class StartForwardMessageCommand : RequestCommand2<MessageAggregate, MessageId, IExecutionResult>
//{
//    public StartForwardMessageCommand(MessageId aggregateId,
//        RequestInfo requestInfo,
//        Peer fromPeer,
//        Peer toPeer,
//        IReadOnlyList<int> idList,
//        IReadOnlyList<long> randomIdList,
//        bool forwardFromLinkedChannel) : base(aggregateId, requestInfo)
//    {
//        FromPeer = fromPeer;
//        ToPeer = toPeer;
//        IdList = idList;
//        RandomIdList = randomIdList;
//        ForwardFromLinkedChannel = forwardFromLinkedChannel;
//    }

//    public Peer FromPeer { get; }
//    public IReadOnlyList<int> IdList { get; }
//    public IReadOnlyList<long> RandomIdList { get; }
//    public bool ForwardFromLinkedChannel { get; }

//    public Peer ToPeer { get; }
//}