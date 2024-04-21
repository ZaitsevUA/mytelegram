namespace MyTelegram.QueryHandlers.InMemory.Messaging;

public class GetMessageItemListToBeDeletedQueryHandler(IQueryOnlyReadModelStore<MessageReadModel> store)
    : IQueryHandler<GetMessageItemListToBeDeletedQuery, IReadOnlyCollection<MessageItemToBeDeleted>>
{
    public async Task<IReadOnlyCollection<MessageItemToBeDeleted>> ExecuteQueryAsync(GetMessageItemListToBeDeletedQuery query, CancellationToken cancellationToken)
    {
        var batchIds =
            (await store.FindAsync(p => p.OwnerPeerId == query.OwnerPeerId && query.MessageIds.Contains(p.MessageId),
                p => p.BatchId, cancellationToken: cancellationToken));

        return await store.FindAsync(p => batchIds.Contains(p.BatchId),
            p => new MessageItemToBeDeleted(p.OwnerPeerId, p.MessageId, p.ToPeerType, p.ToPeerId), cancellationToken: cancellationToken);
    }
}

public class GetMessageItemListToBeDeletedQuery2Handler(IQueryOnlyReadModelStore<MessageReadModel> store)
    : IQueryHandler<GetMessageItemListToBeDeletedQuery2, IReadOnlyCollection<MessageItemToBeDeleted>>
{
    public async Task<IReadOnlyCollection<MessageItemToBeDeleted>> ExecuteQueryAsync(GetMessageItemListToBeDeletedQuery2 query, CancellationToken cancellationToken)
    {
        var maxId = query.MaxId;
        if (maxId == 0)
        {
            maxId = int.MaxValue;
        }
        var batchIds =
            (await store.FindAsync(p => p.OwnerPeerId == query.OwnerPeerId && p.ToPeerId == query.ToPeerId && p.MessageId < maxId,
                p => p.BatchId, limit: query.Limit, cancellationToken: cancellationToken));
        if (batchIds.Count == 0)
        {
            return [];
        }

        return await store.FindAsync(p => batchIds.Contains(p.BatchId),
            p => new MessageItemToBeDeleted(p.OwnerPeerId, p.MessageId, p.ToPeerType, p.ToPeerId), cancellationToken: cancellationToken);
    }
}