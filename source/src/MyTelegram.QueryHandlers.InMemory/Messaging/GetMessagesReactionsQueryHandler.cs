namespace MyTelegram.QueryHandlers.InMemory.Messaging;

public class
    GetMessagesReactionsQueryHandler(IQueryOnlyReadModelStore<MessageReadModel> store) : IQueryHandler<GetMessagesReactionsQuery, IReadOnlyCollection<IHasReactions>>
{
    public async Task<IReadOnlyCollection<IHasReactions>> ExecuteQueryAsync(GetMessagesReactionsQuery query,
        CancellationToken cancellationToken)
    {
        return await store.FindAsync(p =>
               query.MessageIds.Contains(p.MessageId) && p.OwnerPeerId == query.OwnerPeerId,
               p => new HasReactions(p.Reactions, p.RecentReactions, p.CanSeeList, p.MessageId, p.UserReactions), cancellationToken: cancellationToken);
    }
}