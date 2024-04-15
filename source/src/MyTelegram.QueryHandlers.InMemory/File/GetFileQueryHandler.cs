namespace MyTelegram.QueryHandlers.InMemory.File;

public class GetFileQueryHandler(IQueryOnlyReadModelStore<FileReadModel> store) : IQueryHandler<GetFileQuery, IFileReadModel?>
{
    public async Task<IFileReadModel?> ExecuteQueryAsync(GetFileQuery query,
        CancellationToken cancellationToken)
    {
        return await store.FirstOrDefaultAsync(p =>
                p.ServerFileId == query.FileId || p.FileId == query.FileId ||
                p.FileReference == query.FileReference,
            cancellationToken: cancellationToken);
    }
}
