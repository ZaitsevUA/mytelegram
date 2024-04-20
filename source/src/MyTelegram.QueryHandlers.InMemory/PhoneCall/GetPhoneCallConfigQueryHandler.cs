namespace MyTelegram.QueryHandlers.InMemory.PhoneCall;

public class GetPhoneCallConfigQueryHandler(IQueryOnlyReadModelStore<PhoneCallConfigReadModel> store) : IQueryHandler<GetPhoneCallConfigQuery, IPhoneCallConfigReadModel?>
{
    public async Task<IPhoneCallConfigReadModel?> ExecuteQueryAsync(GetPhoneCallConfigQuery query,
        CancellationToken cancellationToken)
    {
        var id = PhoneCallConfigId.Create(query.UserId).Value;
        return await store.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }
}
