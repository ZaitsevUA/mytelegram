namespace MyTelegram.QueryHandlers.InMemory.WallPaper;

public class GetWallPaperBySlugQueryHandler(IQueryOnlyReadModelStore<WallPaperReadModel> store) : IQueryHandler<GetWallPaperBySlugQuery, IWallPaperReadModel?>
{
    public async Task<IWallPaperReadModel?> ExecuteQueryAsync(GetWallPaperBySlugQuery query, CancellationToken cancellationToken)
    {
        return await store.FirstOrDefaultAsync(p => p.Slug == query.Slug, cancellationToken: cancellationToken);
    }
}