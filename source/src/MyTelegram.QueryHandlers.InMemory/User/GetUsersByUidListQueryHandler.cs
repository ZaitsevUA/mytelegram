namespace MyTelegram.QueryHandlers.InMemory.User;

public class
    GetUsersByUidListQueryHandler(IQueryOnlyReadModelStore<UserReadModel> store) : IQueryHandler<GetUsersByUidListQuery, IReadOnlyCollection<IUserReadModel>>
{
    public async Task<IReadOnlyCollection<IUserReadModel>> ExecuteQueryAsync(GetUsersByUidListQuery query,
        CancellationToken cancellationToken)
    {
        if (query.UserIdList.Count == 0)
        {
            return new List<UserReadModel>();
        }

        return await store.FindAsync(p => query.UserIdList.Contains(p.UserId), cancellationToken: cancellationToken);
    }
}
