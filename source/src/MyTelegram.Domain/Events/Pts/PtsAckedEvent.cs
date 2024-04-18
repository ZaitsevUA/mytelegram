namespace MyTelegram.Domain.Events.Pts;

public class PtsAckedEvent(
    long peerId,
    long permAuthKeyId,
    long msgId,
    int pts,
    long globalSeqNo,
    Peer toPeer)
    : AggregateEvent<PtsAggregate, PtsId>
{
    public long GlobalSeqNo { get; } = globalSeqNo;
    public long MsgId { get; } = msgId;
    public long PeerId { get; } = peerId;
    public long PermAuthKeyId { get; } = permAuthKeyId;
    public int Pts { get; } = pts;
    public Peer ToPeer { get; } = toPeer;
}

//public class ChannelPtsForUserUpdatedEvent : AggregateEvent<ChannelPtsAggregate, ChannelPtsId>
//{
//    public long UserId { get; }
//    public long ChannelId { get; }
//    public int Pts { get; }
//    public long GlobalSeqNo { get; }

//    public ChannelPtsForUserUpdatedEvent(long userId,long channelId,int pts,long globalSeqNo)
//    {
//        UserId = userId;
//        ChannelId = channelId;
//        Pts = pts;
//        GlobalSeqNo = globalSeqNo;
//    }
//}