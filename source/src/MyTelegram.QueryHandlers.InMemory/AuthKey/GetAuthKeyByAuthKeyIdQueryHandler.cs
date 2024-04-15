namespace MyTelegram.QueryHandlers.InMemory.AuthKey;

public class GetAuthKeyByAuthKeyIdQueryHandler(IQueryOnlyReadModelStore<AuthKeyReadModel> store) : IQueryHandler<GetAuthKeyByAuthKeyIdQuery, IAuthKeyReadModel?>
{
    public async Task<IAuthKeyReadModel?> ExecuteQueryAsync(GetAuthKeyByAuthKeyIdQuery query,
        CancellationToken cancellationToken)
    {
        return await store.FirstOrDefaultAsync(p => p.AuthKeyId == query.AuthKeyId, cancellationToken);
    }
}
