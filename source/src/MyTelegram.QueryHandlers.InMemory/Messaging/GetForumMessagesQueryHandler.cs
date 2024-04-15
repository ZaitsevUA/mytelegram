namespace MyTelegram.QueryHandlers.InMemory.Messaging;

public class GetForumMessagesQueryHandler(IQueryOnlyReadModelStore<MessageReadModel> store) : IQueryHandler<GetForumMessagesQuery, IReadOnlyCollection<IMessageReadModel>>
{
    public async Task<IReadOnlyCollection<IMessageReadModel>> ExecuteQueryAsync(GetForumMessagesQuery query,
        CancellationToken cancellationToken)
    {
        return await store
            .FindAsync(p => p.OwnerPeerId == query.ChannelId && query.MessageIds.Contains(p.MessageId),
                cancellationToken: default);
    }
}