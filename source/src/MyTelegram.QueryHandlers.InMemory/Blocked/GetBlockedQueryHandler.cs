namespace MyTelegram.QueryHandlers.InMemory.Blocked;

public class GetBlockedQueryHandler(IQueryOnlyReadModelStore<BlockedReadModel> store) : IQueryHandler<GetBlockedQuery, IBlockedReadModel?>
{
    public async Task<IBlockedReadModel?> ExecuteQueryAsync(GetBlockedQuery query,
        CancellationToken cancellationToken)
    {
        return await store.FirstOrDefaultAsync(p => p.UserId == query.UserId && p.TargetPeerId == query.TargetPeerId, cancellationToken);

    }
}
