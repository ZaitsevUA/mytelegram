//namespace MyTelegram.QueryHandlers.MongoDB.AuthKey;

//public class GetAuthKeyByTempAuthKeyIdQueryHandler : IQueryHandler<GetAuthKeyByTempAuthKeyIdQuery, IAuthKeyReadModel?>
//{
//    //private readonly IDbContextProvider<MyTelegramServerReadModelDbContext> _dbContextProvider;
//    private readonly IQueryOnlyReadModelStore<AuthKeyReadModel> _store;

//    public GetAuthKeyByTempAuthKeyIdQueryHandler(IQueryOnlyReadModelStore<AuthKeyReadModel> store)
//    {
//        _store = store;
//    }

//    public async Task<IAuthKeyReadModel?> ExecuteQueryAsync(GetAuthKeyByTempAuthKeyIdQuery query,
//        CancellationToken cancellationToken)
//    {
//        var cursor = await _store.FindAsync(p => p.TempAuthKeyId == query.TempAuthKeyId,
//                cancellationToken: cancellationToken)
//     ;
//        return await cursor.FirstOrDefaultAsync(cancellationToken);
//    }
//}
