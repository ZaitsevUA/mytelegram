namespace MyTelegram.Domain.Sagas.Events;

public class ForwardMessageSagaStartedEvent(
    RequestInfo requestInfo,
    Peer fromPeer,
    Peer toPeer,
    IReadOnlyList<int> idList,
    IReadOnlyList<long> randomIdList,
    bool forwardFromLinkedChannel,
    bool post
    )
    : RequestAggregateEvent2<ForwardMessageSaga, ForwardMessageSagaId>(requestInfo)
{
    public Peer FromPeer { get; } = fromPeer;
    public IReadOnlyList<int> IdList { get; } = idList;
    public IReadOnlyList<long> RandomIdList { get; } = randomIdList;
    public bool ForwardFromLinkedChannel { get; } = forwardFromLinkedChannel;
    public bool Post { get; } = post;

    public Peer ToPeer { get; } = toPeer;
}
