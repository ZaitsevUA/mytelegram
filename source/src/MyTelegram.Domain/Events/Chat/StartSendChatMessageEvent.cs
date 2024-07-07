namespace MyTelegram.Domain.Events.Chat;

//public class SendChatMessageStartedEvent:AggregateEvent<chata>

public class StartSendChatMessageEvent(
    RequestInfo requestInfo,
    long chatId,
    string title,
    IReadOnlyList<long> memberUidList,
    long senderPeerId,
    int senderMessageId,
    bool senderIsBot,
    long lastDeletedMemberUid)
    : /*Request*/RequestAggregateEvent2<ChatAggregate, ChatId>(requestInfo)
{
    //long reqMsgId,

    public long ChatId { get; } = chatId;
    public long LastDeletedMemberUid { get; } = lastDeletedMemberUid;
    public IReadOnlyList<long> MemberUidList { get; } = memberUidList;
    public bool SenderIsBot { get; } = senderIsBot;
    public int SenderMessageId { get; } = senderMessageId;
    public long SenderPeerId { get; } = senderPeerId;
    public string Title { get; } = title;
}
