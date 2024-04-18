namespace MyTelegram.Domain.Commands.Chat;

public class EditChatAdminCommand(
    ChatId aggregateId,
    RequestInfo requestInfo,
    long promotedBy,
    bool canEdit,
    long userId,
    bool isBot,
    ChatAdminRights adminRights,
    string rank,
    int date)
    : RequestCommand2<ChatAggregate, ChatId, IExecutionResult>(aggregateId, requestInfo) //, IHasCorrelationId
{
    public ChatAdminRights AdminRights { get; } = adminRights;
    public bool CanEdit { get; } = canEdit;
    public long PromotedBy { get; } = promotedBy;
    public string Rank { get; } = rank;
    public int Date { get; } = date;
    public long UserId { get; } = userId;
    public bool IsBot { get; } = isBot;
}