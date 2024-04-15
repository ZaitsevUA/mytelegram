namespace MyTelegram.QueryHandlers.MongoDB.ChatAdmin;

public class GetAdminedChannelIdsQueryHandler(IQueryOnlyReadModelStore<ChatAdminReadModel> store) : IQueryHandler<GetAdminedChannelIdsQuery, IReadOnlyCollection<long>>
{
    public async Task<IReadOnlyCollection<long>> ExecuteQueryAsync(GetAdminedChannelIdsQuery query, CancellationToken cancellationToken)
    {
        return await store.FindAsync(p => p.UserId == query.UserId, createResult: p => p.PeerId, cancellationToken: cancellationToken);
    }
}