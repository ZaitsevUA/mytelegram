namespace MyTelegram.Domain.Sagas.Events;

public class SendOutboxMessageCompletedEvent(
    RequestInfo requestInfo,
    MessageItem messageItem,
    List<long>? mentionedUserIds,
    int pts,
    int groupItemCount,
    long? linkedChannelId,
    IReadOnlyCollection<long>? botUserIds)
    : RequestAggregateEvent2<SendMessageSaga, SendMessageSagaId>(requestInfo)
{
    public MessageItem MessageItem { get; } = messageItem;
    public List<long>? MentionedUserIds { get; } = mentionedUserIds;
    public int Pts { get; } = pts;
    public int GroupItemCount { get; } = groupItemCount;
    public long? LinkedChannelId { get; } = linkedChannelId;

    public IReadOnlyCollection<long>? BotUserIds { get; } = botUserIds;
    //public long GlobalSeqNo { get; }

    /*, long globalSeqNo*/
    //GlobalSeqNo = globalSeqNo;
}