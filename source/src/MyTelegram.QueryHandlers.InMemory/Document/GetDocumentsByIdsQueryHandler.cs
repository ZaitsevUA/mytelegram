namespace MyTelegram.QueryHandlers.InMemory.Document;

public class
    GetDocumentsByIdsQueryHandler(IQueryOnlyReadModelStore<DocumentReadModel> store) : IQueryHandler<GetDocumentsByIdsQuery, IReadOnlyCollection<IDocumentReadModel>>
{
    public async Task<IReadOnlyCollection<IDocumentReadModel>> ExecuteQueryAsync(GetDocumentsByIdsQuery query,
        CancellationToken cancellationToken)
    {
        return   await store.FindAsync(p => query.Ids.Contains(p.DocumentId), cancellationToken: cancellationToken);
    }
}