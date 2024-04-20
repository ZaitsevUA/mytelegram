namespace MyTelegram.QueryHandlers.InMemory.User;

public class GetUserByPhoneNumberQueryHandler(IQueryOnlyReadModelStore<UserReadModel> store) : IQueryHandler<GetUserByPhoneNumberQuery, IUserReadModel?>
{
    public async Task<IUserReadModel?> ExecuteQueryAsync(GetUserByPhoneNumberQuery query,
        CancellationToken cancellationToken)
    {
        return await store
            .FirstOrDefaultAsync(p => p.PhoneNumber == query.PhoneNumber, cancellationToken: cancellationToken);
    }
}
