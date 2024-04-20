//namespace MyTelegram.QueryHandlers.MongoDB.Channel;

//public class
//    GetChannelIdListByMemberUserIdQueryHandler : IQueryHandler<GetChannelIdListByMemberUserIdQuery,
//        IReadOnlyCollection<long>>
//{
//    private readonly IMyMongoDbReadModelStore<ChannelMemberReadModel> _store;

//    public GetChannelIdListByMemberUserIdQueryHandler(IMyMongoDbReadModelStore<ChannelMemberReadModel> store)
//    {
//        _store = store;
//    }

//    public async Task<IReadOnlyCollection<long>> ExecuteQueryAsync(GetChannelIdListByMemberUserIdQuery query,
//        CancellationToken cancellationToken)
//    {
//        var findOptions = new FindOptions<ChannelMemberReadModel, long>
//        {
//            Projection = new ProjectionDefinitionBuilder<ChannelMemberReadModel>().Expression(p => p.ChannelId),
//            Limit = 100
//        };
//        var cursor = await _store.FindAsync(p => p.UserId == query.MemberUserId, findOptions, cancellationToken)
//            ;
//        return await cursor.ToListAsync(cancellationToken);
//    }
//}
