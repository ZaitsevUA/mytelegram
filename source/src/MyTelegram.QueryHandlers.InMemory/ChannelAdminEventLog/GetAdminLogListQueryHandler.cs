namespace MyTelegram.QueryHandlers.InMemory.ChannelAdminEventLog;

public class GetAdminLogListQueryHandler(IQueryOnlyReadModelStore<ChannelAdminLogEventReadModel> store)
    : IQueryHandler<GetAdminLogListQuery, IReadOnlyCollection<IChannelAdminLogEventReadModel>>
{
    public async Task<IReadOnlyCollection<IChannelAdminLogEventReadModel>> ExecuteQueryAsync(GetAdminLogListQuery query, CancellationToken cancellationToken)
    {
        Expression<Func<ChannelAdminLogEventReadModel, bool>> filter = p => p.ChannelId == query.ChannelId;
        filter = filter.WhereIf(query.ActionTypes.Count > 0, p => query.ActionTypes.Contains(p.Action));

        return await store.FindAsync(filter, query.Skip, query.Limit, cancellationToken: cancellationToken);
    }
}
