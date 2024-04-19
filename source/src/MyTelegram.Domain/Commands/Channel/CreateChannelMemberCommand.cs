namespace MyTelegram.Domain.Commands.Channel;

public class CreateChannelMemberCommand(
    ChannelMemberId aggregateId,
    RequestInfo requestInfo,
    long channelId,
    long userId,
    long inviterId,
    int date,
    bool isBot,
    long? chatInviteId,
    ChatJoinType chatJoinType = ChatJoinType.InvitedByAdmin)
    : /*Request*/Command<ChannelMemberAggregate, ChannelMemberId, IExecutionResult>(aggregateId),IHasRequestInfo
{
    //long reqMsgId,

    public long ChannelId { get; } = channelId;
    public int Date { get; } = date;
    public long InviterId { get; } = inviterId;
    public bool IsBot { get; } = isBot;
    public long? ChatInviteId { get; } = chatInviteId;
    public ChatJoinType ChatJoinType { get; } = chatJoinType;
    public long UserId { get; } = userId;
    public RequestInfo RequestInfo { get; } = requestInfo;
}