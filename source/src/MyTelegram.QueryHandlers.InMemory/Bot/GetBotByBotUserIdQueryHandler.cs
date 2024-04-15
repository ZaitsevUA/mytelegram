namespace MyTelegram.QueryHandlers.InMemory.Bot;

public class GetBotByBotUserIdQueryHandler(IQueryOnlyReadModelStore<BotReadModel> store) : IQueryHandler<GetBotByBotUserIdQuery, IBotReadModel?>
{
    public async Task<IBotReadModel?> ExecuteQueryAsync(GetBotByBotUserIdQuery query,
        CancellationToken cancellationToken)
    {
        return await store.FirstOrDefaultAsync(p => p.UserId == query.BotUserId, cancellationToken);
    }
}
