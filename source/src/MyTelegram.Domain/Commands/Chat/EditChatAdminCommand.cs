namespace MyTelegram.Domain.Commands.Chat;

public class EditChatAdminCommand : RequestCommand2<ChatAggregate, ChatId, IExecutionResult>//, IHasCorrelationId
{
    public EditChatAdminCommand(ChatId aggregateId,
        RequestInfo requestInfo,
        long promotedBy,
        bool canEdit,
        long userId,
        bool isBot,
        ChatAdminRights adminRights,
        string rank,
        int date) : base(aggregateId, requestInfo)
    {
        PromotedBy = promotedBy;
        CanEdit = canEdit;
        UserId = userId;
        IsBot = isBot;
        AdminRights = adminRights;
        Rank = rank;
        Date = date;
    }

    public ChatAdminRights AdminRights { get; }
    public bool CanEdit { get; }
    public long PromotedBy { get; }
    public string Rank { get; }
    public int Date { get; }
    public long UserId { get; }
    public bool IsBot { get; }
}