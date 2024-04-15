namespace MyTelegram.QueryHandlers.InMemory.Channel;

public class
    GetForumTopicsByIdsQueryHandler(IQueryOnlyReadModelStore<ForumTopicReadModel> store) : IQueryHandler<GetForumTopicsByIdsQuery, IReadOnlyCollection<IForumTopicReadModel>>
{
    public async Task<IReadOnlyCollection<IForumTopicReadModel>> ExecuteQueryAsync(GetForumTopicsByIdsQuery query,
        CancellationToken cancellationToken)
    {
        return await store.FindAsync(p => p.ChannelId == query.ChannelId && query.TopicIds.Contains(p.TopicId), cancellationToken: cancellationToken);
    }
}