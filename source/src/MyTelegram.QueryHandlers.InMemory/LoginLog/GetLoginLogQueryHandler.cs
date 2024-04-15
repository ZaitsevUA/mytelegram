namespace MyTelegram.QueryHandlers.InMemory.LoginLog;

public class GetLoginLogQueryHandler(IQueryOnlyReadModelStore<LoginLogReadModel> store) : IQueryHandler<GetLoginLogQuery, ILoginLogReadModel?>
{
    public async Task<ILoginLogReadModel?> ExecuteQueryAsync(GetLoginLogQuery query,
        CancellationToken cancellationToken)
    {
        return await store.FirstOrDefaultAsync(p => p.UserId == query.UserId, cancellationToken: cancellationToken);
    }
}