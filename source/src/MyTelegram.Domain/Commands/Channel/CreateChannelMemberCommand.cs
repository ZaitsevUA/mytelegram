namespace MyTelegram.Domain.Commands.Channel;

public class CreateChannelMemberCommand : /*Request*/RequestCommand2<ChannelMemberAggregate, ChannelMemberId, IExecutionResult>
{
    public CreateChannelMemberCommand(ChannelMemberId aggregateId,
        //long reqMsgId,
        RequestInfo requestInfo,
        long channelId,
        long userId,
        long inviterId,
        int date,
        bool isBot,
        long? chatInviteId,
        ChatJoinType chatJoinType = ChatJoinType.InvitedByAdmin
        ) : base(aggregateId, requestInfo)
    {
        ChannelId = channelId;
        UserId = userId;
        InviterId = inviterId;
        Date = date;
        IsBot = isBot;
        ChatInviteId = chatInviteId;
        ChatJoinType = chatJoinType;
    }

    public long ChannelId { get; }
    public int Date { get; }
    public long InviterId { get; }
    public bool IsBot { get; }
    public long? ChatInviteId { get; }
    public ChatJoinType ChatJoinType { get; }
    public long UserId { get; }
}