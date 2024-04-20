namespace MyTelegram.QueryHandlers.InMemory.Chat;

public class GetChatByChatIdQueryHandler(IQueryOnlyReadModelStore<ChatReadModel> store) : IQueryHandler<GetChatByChatIdQuery, IChatReadModel?>
{
    public async Task<IChatReadModel?> ExecuteQueryAsync(GetChatByChatIdQuery query,
        CancellationToken cancellationToken)
    {
        return await store.FirstOrDefaultAsync(p => p.ChatId == query.ChatId, cancellationToken);
    }
}
