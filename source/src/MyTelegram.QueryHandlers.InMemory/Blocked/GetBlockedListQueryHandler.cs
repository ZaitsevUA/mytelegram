namespace MyTelegram.QueryHandlers.InMemory.Blocked;

public class GetBlockedListQueryHandler(IQueryOnlyReadModelStore<BlockedReadModel> store) : IQueryHandler<GetBlockedListQuery, IReadOnlyCollection<IBlockedReadModel>>
{
    public async Task<IReadOnlyCollection<IBlockedReadModel>> ExecuteQueryAsync(GetBlockedListQuery query,
        CancellationToken cancellationToken)
    {
        return await store.FindAsync(p => p.UserId == query.UserId, skip: query.Skip, limit: query.Limit, cancellationToken: cancellationToken);
    }
}
