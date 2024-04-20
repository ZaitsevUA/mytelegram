namespace MyTelegram.QueryHandlers.InMemory.Privacy;

public class GetPrivacyListQueryHandler(IQueryOnlyReadModelStore<PrivacyReadModel> store) : IQueryHandler<GetPrivacyListQuery, IReadOnlyCollection<IPrivacyReadModel>>
{
    public async Task<IReadOnlyCollection<IPrivacyReadModel>> ExecuteQueryAsync(GetPrivacyListQuery query,
        CancellationToken cancellationToken)
    {
        return await store
            .FindAsync(p => query.UidList.Contains(p.UserId) && query.PrivacyTypes.Contains(p.PrivacyType),
                cancellationToken: cancellationToken);
    }
}
