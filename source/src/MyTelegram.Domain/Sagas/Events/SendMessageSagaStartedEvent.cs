namespace MyTelegram.Domain.Sagas.Events;

public class SendMessageSagaStartedEvent(
    RequestInfo requestInfo,
    MessageItem messageItem,
    List<long>? mentionedUserIds,
    List<ReplyToMsgItem>? replyToMsgItems,
    bool clearDraft,
    int groupItemCount,
    long? linkedChannelId,
    List<long>? chatMembers)
    : RequestAggregateEvent2<SendMessageSaga, SendMessageSagaId>(requestInfo)
{
    public MessageItem MessageItem { get; } = messageItem;
    public List<long>? MentionedUserIds { get; } = mentionedUserIds;
    public List<ReplyToMsgItem>? ReplyToMsgItems { get; } = replyToMsgItems;
    public bool ClearDraft { get; } = clearDraft;
    public int GroupItemCount { get; } = groupItemCount;
    public long? LinkedChannelId { get; } = linkedChannelId;

    public List<long>? ChatMembers { get; } = chatMembers;
    //public bool ForwardFromLinkedChannel { get; }

    //ForwardFromLinkedChannel = forwardFromLinkedChannel;
}