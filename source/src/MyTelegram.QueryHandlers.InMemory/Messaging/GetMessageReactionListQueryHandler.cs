namespace MyTelegram.QueryHandlers.InMemory.Messaging;

public class
    GetMessageReactionListQueryHandler(IQueryOnlyReadModelStore<UserReactionReadModel> store) : IQueryHandler<GetMessageReactionListQuery,
        IReadOnlyCollection<IUserReactionReadModel>>
{
    public async Task<IReadOnlyCollection<IUserReactionReadModel>> ExecuteQueryAsync(GetMessageReactionListQuery query,
        CancellationToken cancellationToken)
    {
        Expression<Func<UserReactionReadModel, bool>> predicate = p =>
            p.PeerId == query.ToPeerId && p.MessageId == query.MessageId;
        predicate = predicate.WhereIf(query.ReactionId.HasValue, x => x.ReactionId == query.ReactionId!.Value);

        return await store
            .FindAsync(
                predicate,
                cancellationToken: cancellationToken);
    }
}