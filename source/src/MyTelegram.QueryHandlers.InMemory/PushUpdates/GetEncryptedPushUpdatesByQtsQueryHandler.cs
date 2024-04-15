//namespace MyTelegram.QueryHandlers.MongoDB.PushUpdates;

//public class GetEncryptedPushUpdatesByQtsQueryHandler : IQueryHandler<GetEncryptedPushUpdatesByQtsQuery,
//    IReadOnlyCollection<IEncryptedPushUpdatesReadModel>>
//{
//    private readonly IQueryOnlyReadModelStore<EncryptedPushUpdatesReadModel> _store;

//    public GetEncryptedPushUpdatesByQtsQueryHandler(IQueryOnlyReadModelStore<EncryptedPushUpdatesReadModel> store)
//    {
//        _store = store;
//    }

//    public async Task<IReadOnlyCollection<IEncryptedPushUpdatesReadModel>> ExecuteQueryAsync(
//        GetEncryptedPushUpdatesByQtsQuery query,
//        CancellationToken cancellationToken)
//    {
//        var options =
//            new FindOptions<EncryptedPushUpdatesReadModel, EncryptedPushUpdatesReadModel> {
//                Limit = query.Limit, Skip = 0
//            };

//        var cursor = await _store.FindAsync(p =>
//                p.InboxOwnerPeerId== query.PeerId && p.Qts > query.Qts &&
//                p.InboxOwnerPermAuthKeyId == query.PermAuthKeyId,
//            options,
//            cancellationToken);
//        return await cursor.ToListAsync(cancellationToken);
//    }
//}
