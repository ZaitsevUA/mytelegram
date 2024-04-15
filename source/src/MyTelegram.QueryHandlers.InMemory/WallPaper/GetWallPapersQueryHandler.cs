namespace MyTelegram.QueryHandlers.InMemory.WallPaper;

public class GetWallPapersQueryHandler(IQueryOnlyReadModelStore<WallPaperReadModel> store) : IQueryHandler<GetWallPapersQuery, IReadOnlyCollection<IWallPaperReadModel>>
{
    public async Task<IReadOnlyCollection<IWallPaperReadModel>> ExecuteQueryAsync(GetWallPapersQuery query, CancellationToken cancellationToken)
    {
        return await store.FindAsync(p => p.UserId == query.UserId, cancellationToken: cancellationToken);
    }
}