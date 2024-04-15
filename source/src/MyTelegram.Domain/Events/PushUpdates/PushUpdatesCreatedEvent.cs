namespace MyTelegram.Domain.Events.PushUpdates;

public class PushUpdatesCreatedEvent : AggregateEvent<PushUpdatesAggregate, PushUpdatesId>
{
    public PushUpdatesCreatedEvent(long ownerPeerId, long? excludeAuthKeyId, long? excludeUserId, long? onlySendToThisAuthKeyId,
        /*PtsType ptsType,*/ int pts, int? messageId, int date, long seqNo,
        byte[] updates,
        List<long>? users, List<long>? chats)

    {
        OwnerPeerId = ownerPeerId;
        ExcludeAuthKeyId = excludeAuthKeyId;
        ExcludeUserId = excludeUserId;
        OnlySendToThisAuthKeyId = onlySendToThisAuthKeyId;
        //PtsType = ptsType;
        Pts = pts;
        MessageId = messageId;
        Date = date;
        SeqNo = seqNo;
        Updates = updates;
        Users = users;
        Chats = chats;
    }

    public long OwnerPeerId { get; }
    public long? ExcludeAuthKeyId { get; }
    public long? ExcludeUserId { get; }
    public long? OnlySendToThisAuthKeyId { get; }
    //public PtsType PtsType { get; }
    public int Pts { get; }
    public int? MessageId { get; }
    public int Date { get; }
    public long SeqNo { get; }
    public byte[] Updates { get; }
    public List<long>? Users { get; }
    public List<long>? Chats { get; }
}

//public class UpdatesCreatedEvent : AggregateEvent<UpdatesAggregate, UpdatesId>
//{
//    public UpdatesCreatedEvent(long ownerPeerId, long? excludeAuthKeyId, long? excludeUserId, long? onlySendToThisAuthKeyId,
//        PtsType ptsType, int pts, int? messageId, int date, long seqNo,
//        byte[] updates,
//        List<long>? users, List<long>? chats)

//    {
//        OwnerPeerId = ownerPeerId;
//        ExcludeAuthKeyId = excludeAuthKeyId;
//        ExcludeUserId = excludeUserId;
//        OnlySendToThisAuthKeyId = onlySendToThisAuthKeyId;
//        PtsType = ptsType;
//        Pts = pts;
//        MessageId = messageId;
//        Date = date;
//        SeqNo = seqNo;
//        Updates = updates;
//        Users = users;
//        Chats = chats;
//    }

//    public long OwnerPeerId { get; }
//    public long? ExcludeAuthKeyId { get; }
//    public long? ExcludeUserId { get; }
//    public long? OnlySendToThisAuthKeyId { get; }
//    public PtsType PtsType { get; }
//    public int Pts { get; }
//    public int? MessageId { get; }
//    public int Date { get; }
//    public long SeqNo { get; }
//    public byte[] Updates { get; }
//    public List<long>? Users { get; }
//    public List<long>? Chats { get; }
//}