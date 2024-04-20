namespace MyTelegram.QueryHandlers.InMemory.UserPassword;

public class GetUserPasswordQueryHandler(IQueryOnlyReadModelStore<UserPasswordReadModel> store) : IQueryHandler<GetUserPasswordQuery, IUserPasswordReadModel?>
{
    public async Task<IUserPasswordReadModel?> ExecuteQueryAsync(GetUserPasswordQuery query,
        CancellationToken cancellationToken)
    {
        return await store
            .FirstOrDefaultAsync(p => p.Id == UserPasswordId.Create(query.UserId).Value && p.HasPassword,
                cancellationToken: cancellationToken);
    }
}
