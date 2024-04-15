namespace MyTelegram.QueryHandlers.InMemory.Mention;
public class GetUnreadMentionedMessageIdListQueryHandler
    (IQueryOnlyReadModelStore<MentionsReadModel> store) : IQueryHandler<GetUnreadMentionedMessageIdListQuery, IReadOnlyCollection<int>>
{
    public async Task<IReadOnlyCollection<int>> ExecuteQueryAsync(GetUnreadMentionedMessageIdListQuery query, CancellationToken cancellationToken)
    {
        Expression<Func<MentionsReadModel, bool>> predicate = x => x.OwnerUserId == query.OwnerUserId && x.ToPeerId == query.ToPeerId;
        predicate = predicate
                .WhereIf(query.Offset?.MaxId > 0, p => p.MessageId < query.Offset!.MaxId)
                .WhereIf(query.Offset?.LoadType == LoadType.Forward, p => p.MessageId > query.Offset!.FromId)
            ;

        return await store.FindAsync(predicate, p => p.MessageId, 0, query.Limit, new SortOptions<MentionsReadModel>(p => p.MessageId, SortType.Descending), cancellationToken);
    }
}
