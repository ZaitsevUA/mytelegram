namespace MyTelegram.QueryHandlers.MongoDB.Chat;

public class GetChatByChatIdListQueryHandler(IQueryOnlyReadModelStore<ChatReadModel> store) : IQueryHandler<GetChatByChatIdListQuery, IReadOnlyCollection<IChatReadModel>>
{
    public async Task<IReadOnlyCollection<IChatReadModel>> ExecuteQueryAsync(GetChatByChatIdListQuery query,
        CancellationToken cancellationToken)
    {
        return await store.FindAsync(p => query.ChatIdList.Contains(p.ChatId), cancellationToken: cancellationToken);
    }
}
