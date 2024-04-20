namespace MyTelegram.QueryHandlers.InMemory.Document;
public class GetDocumentByIdQueryHandler(IQueryOnlyReadModelStore<DocumentReadModel> store) : IQueryHandler<GetDocumentByIdQuery, IDocumentReadModel?>
{
    public async Task<IDocumentReadModel?> ExecuteQueryAsync(GetDocumentByIdQuery query,
        CancellationToken cancellationToken)
    {
        return await store.FirstOrDefaultAsync(p => p.DocumentId == query.Id, cancellationToken: cancellationToken);
    }
}