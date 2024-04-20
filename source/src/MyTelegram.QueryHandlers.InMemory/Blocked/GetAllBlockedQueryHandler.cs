namespace MyTelegram.QueryHandlers.InMemory.Blocked;

public class GetAllBlockedQueryHandler(IQueryOnlyReadModelStore<BlockedReadModel> store) : IQueryHandler<GetAllBlockedQuery, IReadOnlyCollection<IBlockedReadModel>>
{
    public async Task<IReadOnlyCollection<IBlockedReadModel>> ExecuteQueryAsync(GetAllBlockedQuery query,
        CancellationToken cancellationToken)
    {
        return await store.FindAsync(p => true, skip: query.Skip, limit: query.Limit, cancellationToken: cancellationToken);
    }
}
