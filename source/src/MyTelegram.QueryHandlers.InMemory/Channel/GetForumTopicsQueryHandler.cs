namespace MyTelegram.QueryHandlers.InMemory.Channel;

public class GetForumTopicsQueryHandler(IQueryOnlyReadModelStore<ForumTopicReadModel> store) : IQueryHandler<GetForumTopicsQuery, IReadOnlyCollection<IForumTopicReadModel>>
{
    public async Task<IReadOnlyCollection<IForumTopicReadModel>> ExecuteQueryAsync(GetForumTopicsQuery query,
        CancellationToken cancellationToken)
    {
        return await store.FindAsync(p => p.ChannelId == query.ChannelId, limit: query.Limit, cancellationToken: cancellationToken);
    }
}