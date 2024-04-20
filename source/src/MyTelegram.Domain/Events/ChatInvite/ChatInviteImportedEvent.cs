namespace MyTelegram.Domain.Events.ChatInvite;

public class ChatInviteImportedEvent(
    RequestInfo requestInfo,
    long channelId,
    long inviteId,
    ChatInviteRequestState chatInviteRequestState,
    int? requested,
    int? usage,
    string hash,
    int date)
    : RequestAggregateEvent2<ChatInviteAggregate, ChatInviteId>(requestInfo)
{
    public long ChannelId { get; } = channelId;
    public long InviteId { get; } = inviteId;
    public ChatInviteRequestState ChatInviteRequestState { get; } = chatInviteRequestState;
    public int? Requested { get; } = requested;
    public int? Usage { get; } = usage;
    public string Hash { get; } = hash;
    public int Date { get; } = date;
}