namespace MyTelegram.QueryHandlers.InMemory.WallPaper;

public class GetWallPaperQueryHandler(IQueryOnlyReadModelStore<WallPaperReadModel> store) : IQueryHandler<GetWallPaperQuery, IWallPaperReadModel?>
{
    public async Task<IWallPaperReadModel?> ExecuteQueryAsync(GetWallPaperQuery query, CancellationToken cancellationToken)
    {
        return await store.FirstOrDefaultAsync(p => p.WallPaperId == query.WallPaperId, cancellationToken: cancellationToken);
    }
}