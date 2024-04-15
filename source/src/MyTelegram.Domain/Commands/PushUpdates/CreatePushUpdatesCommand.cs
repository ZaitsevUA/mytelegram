namespace MyTelegram.Domain.Commands.PushUpdates;

public class CreatePushUpdatesCommand : Command<PushUpdatesAggregate, PushUpdatesId, IExecutionResult>
{
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

    public CreatePushUpdatesCommand(PushUpdatesId aggregateId,
        long ownerPeerId, long? excludeAuthKeyId,long? excludeUserId, long? onlySendToThisAuthKeyId,
        /*PtsType ptsType,*/ int pts, int? messageId, int date, long seqNo,
        byte[] updates,
        List<long>? users, List<long>? chats) : base(aggregateId)
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
}
